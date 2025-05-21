using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt
{
    public class GameWorld : Game
    {
        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjectsToAdd;
        private List<GameObject> activeGameObjects;
        private List<GameObject> gameObjectsToRemove;
        private static Vector2 screenSize;

        private static GameWorld instance;

        #endregion
        #region Properties
        //TEMP
        public static Player Player { get; private set; }
        public static GameTime Time { get; private set; }
        //no longer temp
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }

        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }

        #endregion
        #region Constructors
        private GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        #endregion
        #region Methods
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();
            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            activeGameObjects = new List<GameObject>();

            AddPlayer(new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2));
            AddObject(new Tree(new Vector2(ScreenSize.X / 2 + 200, ScreenSize.Y / 2)));


            InputHandler.AddHeldKeyCommand(Keys.D, new WalkCommand(Player.Instance, new Vector2(1, 0)));
            InputHandler.AddHeldKeyCommand(Keys.A, new WalkCommand(Player.Instance, new Vector2(-1, 0)));
            InputHandler.AddHeldKeyCommand(Keys.W, new WalkCommand(Player.Instance, new Vector2(0, -1)));
            InputHandler.AddHeldKeyCommand(Keys.S, new WalkCommand(Player.Instance, new Vector2(0, 1)));

            InputHandler.AddPressedKeyCommand(Keys.Space, new SprintCommand(Player.Instance));
            InputHandler.AddUnclickedCommand(Keys.Space, new SprintCommand(Player.Instance));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GameObject.Pixel = Content.Load<Texture2D>("resd");
            EnemyFactory.LoadContent(Content);
            MemorabiliaFactory.LoadContent(Content);

            AddObject(EnemyFactory.CreateEnemy(new Vector2(300, 300), EnemyType.Hunter));
            AddObject(MemorabiliaFactory.CreateMemorabilia());

            ShaderManager.SetSprite();
        }

        protected override void Update(GameTime gameTime)
        {
            Time = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.HandleInput();

            foreach (GameObject gameObject in activeGameObjects)
            {
                if (gameObject.Active)
                {
                    gameObject.Update(gameTime);
                }
            }
            TimeLineManager.Update(gameTime);
            CheckCollision();


            AddAndRemoveGameObjects();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);

            //Higher layer numbers are closer, lower are further away
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            foreach (GameObject gameObject in activeGameObjects)
            {
                if (gameObject.GetComponent<SpriteRenderer>() != null && gameObject.Active)
                {
                    gameObject.GetComponent<SpriteRenderer>().Draw(_spriteBatch);
                    gameObject.Draw(_spriteBatch);
                }
            }
            ShaderManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddAndRemoveGameObjects()
        {
            foreach (GameObject gameObject in gameObjectsToAdd)
            {
                gameObject.Start();
                activeGameObjects.Add(gameObject);
            }
            gameObjectsToAdd.Clear();
            foreach (GameObject gameObject in gameObjectsToRemove)
            {
                if (!activeGameObjects.Contains(gameObject))
                {
                    activeGameObjects.Remove(gameObject);
                }
            }
            gameObjectsToRemove.Clear();
        }

        public void CheckCollision()
        {
            foreach (GameObject gameObject1 in activeGameObjects)
            {
                if (gameObject1.Active)
                {
                    foreach (GameObject gameObject2 in activeGameObjects)
                    {
                        if (gameObject1 == gameObject2)
                        {
                            continue;
                        }

                        if (gameObject1.CheckCollision(gameObject2) && gameObject2.Active)
                        {
                            gameObject1.OnCollision(gameObject2);
                            gameObject2.OnCollision(gameObject1);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// Adds object to the gameworld during next update
        /// </summary>
        /// <param name="gameObject"></param>
        public void AddObject(GameObject gameObject)
        {
            gameObject.Awake();
            gameObjectsToAdd.Add(gameObject);
        }
        /// <summary>
        /// Removes object from the gameworld during next update
        /// </summary>
        /// <param name="gameObject"></param>
        public void RemoveObject(GameObject gameObject)
        {
            gameObjectsToRemove.Add(gameObject);
        }
        /// <summary>
        /// Method to create player
        /// </summary>
        /// <param name="position"></param>
        private void AddPlayer(Vector2 position)
        {
            Player newPlayer = Player.Instance;
            newPlayer.AddComponent<SpriteRenderer>(Content.Load<Texture2D>("noImageFound"), 1f);
            newPlayer.AddComponent<LightEmitter>(0.3f);
            //newPlayer.AddComponent<Movable>();
            newPlayer.Transform.Scale = 10f;
            AddObject(newPlayer);
            Player = newPlayer;
        }
        
        public void MoveMap(Vector2 direction, float speed)
        {
            foreach (GameObject gameObject in activeGameObjects)
            {
                if (gameObject != Player && gameObject.Active)
                {
                    gameObject.Transform.Position -= ((direction * speed) * (float)Time.ElapsedGameTime.TotalSeconds);
                }
            }
        }

        public (List<LightEmitter> lightEmitters, List<ShadowInterval> shadowIntervals) GetShaderData()
        {
            List<LightEmitter> lightEmitters = new List<LightEmitter>();
            List<ShadowInterval> shadowCasters = new List<ShadowInterval>();
            foreach (GameObject gameObject in activeGameObjects)
            {
                Component shaderComponent;
                if ((shaderComponent = gameObject.GetComponent<LightEmitter>()) is not null)
                {
                    lightEmitters.Add((LightEmitter)shaderComponent);
                }
                else if ((shaderComponent = gameObject.GetComponent<ShadowCaster>()) is not null)
                {

                    //shadowCasters.Add((ShadowInterval)shaderComponent);
                }
            }
            return (lightEmitters, shadowCasters);
        }

        #endregion
    }
}
