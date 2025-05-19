using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;

namespace Necrotroph_Eksamensprojekt
{
    public enum EnemyType
    {
        Hunter = 1,
        Seeker = 2,
        LightEater = 3,
        Stalker = 4,
    }

    public class GameWorld : Game
    {
        #region Fields
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjectsToAdd;
        private List<GameObject> activeGameObjects;
        private List<GameObject> gameObjectsToRemove;
        private static GameWorld instance;

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
            gameObjectsToAdd = new List<GameObject>();
            gameObjectsToRemove = new List<GameObject>();
            activeGameObjects = new List<GameObject>();
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject gameObject in activeGameObjects)
            {
                gameObject.Update(gameTime);
            }
            
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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Higher layer numbers are closer, lower are further away
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            foreach (GameObject gameObject in activeGameObjects)
            {
                if (gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    gameObject.GetComponent<SpriteRenderer>().Draw(_spriteBatch);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
