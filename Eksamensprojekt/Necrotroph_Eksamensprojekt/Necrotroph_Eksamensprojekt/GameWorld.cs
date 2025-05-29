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
using Necrotroph_Eksamensprojekt.Menu;
using Necrotroph_Eksamensprojekt.ObjectPools;

namespace Necrotroph_Eksamensprojekt
{
    public class GameWorld : Game
    {
        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private static Vector2 screenSize;
        private static GameWorld instance;
        private static GameState gameState;
        private static GameState gameStateToChangeTo;


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

            GameState.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Time = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


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
        }

        #endregion
    }
}


