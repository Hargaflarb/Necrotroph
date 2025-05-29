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
using Necrotroph_Eksamensprojekt.Menu;
using Necrotroph_Eksamensprojekt.ObjectPools;
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt
{
    public class GameWorld : Game, IListener
    {
        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;
        private static GameWorld instance;
        
        private static GameState gameState;
        private static GameState gameStateToChangeTo;


        private bool gameWon = false;
        private string connectionString;
        private SqlConnection connection;

        #endregion
        #region Properties
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

        public static GameTime Time { get; private set; }

        public string ConnectionString { get => connectionString; set => connectionString = value; }
        public SqlConnection Connection { get => connection; set => connection = value; }

        public static Vector2 ScreenSize { get => screenSize; set => screenSize = value; }
        public GraphicsDeviceManager Graphics { get => _graphics; private set => _graphics = value; }
        public SpriteBatch SpriteBatch { get => _spriteBatch; private set => _spriteBatch = value; }
        public static GameState GameState { get => gameState; set => gameState = value; }
        public static GameState GameStateToChangeTo { get => gameStateToChangeTo; set => gameStateToChangeTo = value; }

        #endregion
        #region Constructors
        private GameWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            GameState = MainMenu.Instance;
        }

        #endregion
        #region Methods

        protected override void Initialize()
        {
            Graphics.PreferredBackBufferHeight = 1080;
            Graphics.PreferredBackBufferWidth = 1920;
            //Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
            ScreenSize = new Vector2(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);
            
            ConnectionString =
                "Server = localhost\\SQLEXPRESS; Database = GhostGame; Trusted_Connection = True; TrustServerCertificate = True";
            Connection = new SqlConnection(ConnectionString);
            
            GameState.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            GameObject.Pixel = Content.Load<Texture2D>("resd");
            EnemyFactory.LoadContent(Content);
            TreeFactory.LoadContent(Content);
            MemorabiliaFactory.LoadContent(Content);
            TextFactory.LoadContent(Content);

            DataBaseTest();

            SaveManager.Execute();
            
            GameState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Time = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            SeekerEnemyManager.Update();
            GameState.Update(gameTime);

            ChangeGameState(GameStateToChangeTo);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameState.Draw(gameTime);
            base.Draw(gameTime);
        }

        private void ChangeGameState(GameState gameState)
        {
            if (gameState is not null & gameState != GameState)
            {
                GameState.Exit();
                GameState = gameState;
                GameState.Enter();
            }
            gameObjectsToRemove.Clear();
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


