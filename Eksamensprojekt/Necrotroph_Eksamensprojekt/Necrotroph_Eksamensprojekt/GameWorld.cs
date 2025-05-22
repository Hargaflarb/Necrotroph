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
using Necrotroph_Eksamensprojekt.ObjectPools;

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
        private Vector2 previousPlayerPosition;
        private static Vector2 screenSize;
        private int itemsCollected;
        private static GameWorld instance;

        #endregion
        #region Properties
        public static GameTime Time { get; private set; }
        public int ItemsCollected
        {
            get => itemsCollected;
            set
            {
                if (value <= 0)
                {
                    itemsCollected = 0;
                }
                else if (value >= 5)
                {
                    itemsCollected = 5;
                }
                else
                {
                    itemsCollected = value;
                }
            }
        }
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

            AddPlayer(Vector2.Zero);
            AddObject(TreePool.Instance.GetObject(new Vector2(200, 0)));


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
            TextFactory.LoadContent(Content);

            AddObject(EnemyFactory.CreateEnemy(new Vector2(-300, -300), EnemyType.Hunter));
            AddObject(MemorabiliaFactory.CreateMemorabilia(new Vector2(-500, 0)));
            UIManager.Instance.AddUIObject(TextFactory.CreateTextObject(ItemsCollected + "/5", Color.White));

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

            foreach (UIObject uiObject in UIManager.Instance.ActiveUIObjects)
            {
                if (uiObject.Active)
                {
                    uiObject.Update(gameTime);
                }
            }
            TimeLineManager.Update(gameTime);
            CheckCollision();

            ItemsCollected++;
            UIManager.Instance.AddAndRemoveUIObjects();

            Map.CheckForObejctsToLoad();
            Map.CheckForObjectsToUnload();
            AddAndRemoveGameObjects();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ShaderManager.PrepareShadows(_spriteBatch);

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

            foreach (UIObject uiObject in UIManager.Instance.ActiveUIObjects)
            {
                if (uiObject.GetComponent<TextRenderer>() != null && uiObject.Active)
                {
                    uiObject.GetComponent<TextRenderer>().Draw(_spriteBatch);
                    uiObject.Draw(_spriteBatch);
                }
            }
#if !DEBUG
            ShaderManager.Draw(_spriteBatch);
#endif
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
                if (activeGameObjects.Contains(gameObject))
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
            newPlayer.AddComponent<Movable>();
            newPlayer.AddComponent<SpriteRenderer>(Content.Load<Texture2D>("noImageFound"), 1f);
            newPlayer.AddComponent<LightEmitter>(0.15f);
            newPlayer.Transform.Scale = 10f;
            AddObject(newPlayer);
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
                //else if ((shaderComponent = gameObject.GetComponent<ShadowCaster>()) is not null)
                {

                    //shadowCasters.Add((ShadowInterval)shaderComponent);
                }
            }
            return (lightEmitters, shadowCasters);
        }

        #endregion
    }
}


