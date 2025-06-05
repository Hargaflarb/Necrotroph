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
    public class GameWorld : Game
    {
        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;
        private static GameWorld instance;
        private static Random rnd;
        private static int seed;

        private static GameState gameState;
        private static GameState gameStateToChangeTo;


        private bool gameWon = false;
        private string connectionString;
        private SqlConnection connection;
        private static MemorabiliaProgress memProgress;

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
        public static Random Rnd { get => rnd; set => rnd = value; }
        public static int Seed { get => seed; set => seed = value; }
        public static MemorabiliaProgress MemProgress { get => memProgress; set => memProgress = value; }

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
            
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();

            Seed = (new Random()).Next();
            Rnd = new Random(Seed);

            ScreenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
                        
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            GameObject.Pixel = Content.Load<Texture2D>("resd");
            InGame.TileSprite = Content.Load<Texture2D>("grass2");
            EnemyFactory.LoadContent(Content);
            TreeFactory.LoadContent(Content);
            MemorabeliaFactory.LoadContent(Content);
            TextFactory.LoadContent(Content);
            LightEmitter.ShaderShadowEffect = Content.Load<Effect>("ShadowShader");

            ShaderManager.SetSpritesAndShaders();
            
            GameState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Time = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputHandler.CurrentMouseState = Mouse.GetState();
            GameState.Update(gameTime);

            ChangeGameState(GameStateToChangeTo);
            InputHandler.LastMouseState = InputHandler.CurrentMouseState;
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
        }
        #endregion
    }
}


