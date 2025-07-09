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
        private static int progressNum;

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
        public static int ProgressNum { get => progressNum; set => progressNum = value; }


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


            //Sound things - Emma
            SoundManager.Instance.AddSFX("PlayerWalk1", Content.Load<SoundEffect>("SFX/Player/rustling-grass-3-101284"), 300, true);
            SoundManager.Instance.AddSFX("PlayerWalk2", Content.Load<SoundEffect>("SFX/Player/bushmovement-6986"), 300, true);
            SoundManager.Instance.AddSFX("PlayerDamaged1", Content.Load<SoundEffect>("SFX/Player/glass-breaking-99389"), 400, false);
            SoundManager.Instance.AddSFX("PlayerDamaged2", Content.Load<SoundEffect>("SFX/Player/break06-36414"), 400, false);
            SoundManager.Instance.AddSFX("PlayerDeath", Content.Load<SoundEffect>("SFX/Player/breaking-glass-83809"), 400, false);
            SoundManager.Instance.AddSFX("PlayerLightToggle", Content.Load<SoundEffect>("SFX/Player/light-switch-81967"), 300, false);
            SoundManager.Instance.AddSFX("PlayerPickUpLight", Content.Load<SoundEffect>("SFX/Player/sfx9-fwoosh-324525"), 100, false);
            SoundManager.Instance.AddSFX("PlayerLightBurst", Content.Load<SoundEffect>("SFX/Player/magic-spell-333896"), 100, false);
            SoundManager.Instance.AddSFX("SeekerActivate", Content.Load<SoundEffect>("SFX/Seeker/very-loud-eviscerating-2-89000"), 400, false);
            SoundManager.Instance.AddSFX("SeekerDeactivate", Content.Load<SoundEffect>("SFX/Seeker/hugecrack-86690"), 300, false);
            SoundManager.Instance.AddSFX("HunterMove1", Content.Load<SoundEffect>("SFX/Hunter/dragging-84771"), 300, true);
            SoundManager.Instance.AddSFX("HunterMove2", Content.Load<SoundEffect>("SFX/Hunter/branch-drag-329004"), 300, true);
            SoundManager.Instance.AddSFX("HunterGrowl", Content.Load<SoundEffect>("SFX/Hunter/small-dog-growling-65105-Edited"), 200, true);
            SoundManager.Instance.AddSFX("LightEaterBuzzing", Content.Load<SoundEffect>("SFX/LightEater/bee_wasp-97053"), 400, true);

            SoundManager.Instance.AddAmbience("SpookyAmbience1", Content.Load<Song>("Ambience/darker-ambient-in-scandinavian-forest-190400"), 0.5f);
            SoundManager.Instance.AddAmbience("Wind", Content.Load<Song>("Ambience/smooth-cold-wind-looped-135538"), 0.1f);
            SoundManager.Instance.PlayAmbience("SpookyAmbience1");


            GameObject.Pixel = Content.Load<Texture2D>("resd");
            InGame.TileSprite = Content.Load<Texture2D>("grass2");
            EnemyFactory.LoadContent(Content);
            LightRefillFactory.LoadContent(Content);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)// || Keyboard.GetState().IsKeyDown(Keys.Escape))
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


