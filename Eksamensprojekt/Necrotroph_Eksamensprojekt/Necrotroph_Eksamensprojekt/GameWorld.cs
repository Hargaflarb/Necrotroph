using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Enemies;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.ObjectPools;
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt
{
    public class GameWorld : Game, IListener
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
        private bool gameWon = false;
        private string connectionString;
        private SqlConnection connection;
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
        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public SqlConnection Connection { get => connection; set => connection = value; }
        
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

            ConnectionString =
                "Server = localhost\\SQLEXPRESS; Database = GhostGame; Trusted_Connection = True; TrustServerCertificate = True";

            Connection = new SqlConnection(ConnectionString);

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            activeGameObjects = new List<GameObject>();

            AddPlayer(Vector2.Zero);
            Player.Instance.Observer = new DeathObserver();
            Player.Instance.Observer.AddListener(this);
            //AddObject(TreePool.Instance.GetObject(new Vector2(200, 0)));
            Map.GenerateMap();

            InputHandler.AddHeldKeyCommand(Keys.D, new WalkCommand(Player.Instance, new Vector2(1, 0)));
            InputHandler.AddHeldKeyCommand(Keys.A, new WalkCommand(Player.Instance, new Vector2(-1, 0)));
            InputHandler.AddHeldKeyCommand(Keys.W, new WalkCommand(Player.Instance, new Vector2(0, -1)));
            InputHandler.AddHeldKeyCommand(Keys.S, new WalkCommand(Player.Instance, new Vector2(0, 1)));

            InputHandler.AddUnclickedCommand(Keys.D, new WalkCommand(Player.Instance, new Vector2(1, 0)));
            InputHandler.AddUnclickedCommand(Keys.A, new WalkCommand(Player.Instance, new Vector2(-1, 0)));
            InputHandler.AddUnclickedCommand(Keys.W, new WalkCommand(Player.Instance, new Vector2(0, -1)));
            InputHandler.AddUnclickedCommand(Keys.S, new WalkCommand(Player.Instance, new Vector2(0, 1)));

            InputHandler.AddPressedKeyCommand(Keys.LeftShift, new SprintCommand(Player.Instance));
            InputHandler.AddUnclickedCommand(Keys.LeftShift, new SprintCommand(Player.Instance));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            GameObject.Pixel = Content.Load<Texture2D>("resd");
            EnemyFactory.LoadContent(Content);
            TreeFactory.LoadContent(Content);
            MemorabiliaFactory.LoadContent(Content);
            TextFactory.LoadContent(Content);

            AddObject(EnemyFactory.CreateEnemy(new Vector2(-300, -300), EnemyType.Hunter));
            AddObject(MemorabiliaFactory.CreateMemorabilia(new Vector2(500, 0)));
            AddObject(MemorabiliaFactory.CreateMemorabilia(new Vector2(-500, 0)));
            AddObject(MemorabiliaFactory.CreateMemorabilia(new Vector2(200, 0)));
            AddObject(MemorabiliaFactory.CreateMemorabilia(new Vector2(-200, 0)));
            AddObject(MemorabiliaFactory.CreateMemorabilia(new Vector2(600, 0)));

            DataBaseTest();

            SaveManager.Execute();

            UIManager.AddUIObject(TextFactory.CreateTextObject(() => { return ItemsCollected + "/5"; }, Color.White, new Vector2 (50, 50), 1f));

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

            foreach (UIObject uiObject in UIManager.ActiveUIObjects)
            {
                if (uiObject.Active)
                {
                    uiObject.Update(gameTime);
                }
            }
            TimeLineManager.Update(gameTime);
            CheckCollision();

            SeekerEnemyManager.Update();
            Map.CheckForObejctsToLoad();
            Map.CheckForObjectsToUnload();
            AddAndRemoveGameObjects();
            UIManager.AddAndRemoveUIObjects();
            CheckForWin();
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
                if (gameObject.Active)
                {
                    gameObject.Draw(_spriteBatch);
                }
            }

            foreach (UIObject uiObject in UIManager.ActiveUIObjects)
            {
                if (uiObject.Active)
                {
                    uiObject.Draw(_spriteBatch);
                }
            }
            //#if !DEBUG
            ShaderManager.Draw(_spriteBatch);
            //#endif
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
            for (int i = 0; i < activeGameObjects.Count; i++)
            {

                for (int j = i + 1; j < activeGameObjects.Count; j++)
                {
                    if (activeGameObjects[i].CheckCollision(activeGameObjects[j]) && activeGameObjects[j].Active)
                    {
                        activeGameObjects[i].OnCollision(activeGameObjects[j]);
                        activeGameObjects[j].OnCollision(activeGameObjects[i]);
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
            newPlayer.AddComponent<SpriteRenderer>(Content.Load<Texture2D>("PlayerSprites/playerIdleSouthLightOn"), 1f, new Vector2(0.6f, 0.3f), new Vector2(0.5f, 0.85f));
            newPlayer.AddComponent<Animator>();
            newPlayer.AddComponent<LightEmitter>(0.2f);
            //remember to add more animations
            (newPlayer.GetComponent<Animator>()).AddAnimation("IdleLeftLightOff", Content.Load<Texture2D>("PlayerSprites/playerIdleWestLightOff"));
            (newPlayer.GetComponent<Animator>()).AddAnimation("IdleLeftLightOn", Content.Load<Texture2D>("PlayerSprites/playerIdleWestLightOn"));
            (newPlayer.GetComponent<Animator>()).AddAnimation("IdleDownLightOff", Content.Load<Texture2D>("PlayerSprites/playerIdleSouthLightOff"));
            (newPlayer.GetComponent<Animator>()).AddAnimation("IdleDownLightOn", Content.Load<Texture2D>("PlayerSprites/playerIdleSouthLightOn"));
            (newPlayer.GetComponent<Animator>()).PlayAnimation("IdleDownLightOn");
            newPlayer.Transform.Scale = 0.25f;
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

        public void CheckForWin()
        {
            if (ItemsCollected == 5 && !gameWon)
            {
                activeGameObjects.Clear();
                UIManager.ActiveUIObjects.Clear();
                gameWon = true;
                string win = "You win";

                UIManager.AddUIObject(TextFactory.CreateTextObject(() => { return win; }, Color.White, new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2), 2f));
            }
        }

        public void HearFromObserver(IObserver observer)
        {
            //currently appears under the shader :/
            UIManager.AddUIObject(TextFactory.CreateTextObject(() => { return "Game Over"; }, Color.White, new Vector2(screenSize.X / 2, screenSize.Y / 2), 2f));
        }
        public void DataBaseTest()
        {
            try
            {
                Connection.Open();
                //string insertQuery = "INSERT INTO Saves (SaveID, Light, PlayerPosX, PlayerPosY) VALUES (1, 2, 10, 50)";
                //SqlCommand insertCommand = new SqlCommand(insertQuery, Connection);
                //insertCommand.ExecuteNonQuery();

                SqlCommand selectCommand = new SqlCommand("SELECT SaveID, Light FROM Saves", Connection);
                SqlDataReader reader = selectCommand.ExecuteReader();
                string test = "eh";

                while (reader.Read())
                {
                    int saveID = reader.GetInt32(1);
                    test = $"{saveID}";
                }
                reader.Close();

                UIManager.AddUIObject(TextFactory.CreateTextObject(() => { return test; }, Color.White, new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2), 1f));
                Connection.Close();
            }
            catch(Microsoft.Data.SqlClient.SqlException)
            {
                //in case the user does not have the database
            }
        }

        #endregion
    }
}


