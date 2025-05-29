using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Menu
{
    public abstract class GameState
    {
        protected static GameTime Time { get => GameWorld.Time; }
        protected static GraphicsDeviceManager _graphics { get => GameWorld.Instance.Graphics; }
        protected static SpriteBatch _spriteBatch { get => GameWorld.Instance.SpriteBatch; }
        protected static GraphicsDevice GraphicsDevice { get =>  GameWorld.Instance.GraphicsDevice; }
        protected static ContentManager Content { get => GameWorld.Instance.Content; }


        public virtual void Initialize() { }

        public virtual void LoadContent() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(GameTime gameTime) { }

        public virtual void Enter()
        {
            Initialize();
            LoadContent();
        }

        public virtual void Exit() { }

    }
}
