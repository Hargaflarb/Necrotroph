using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt
{
    public class Player : GameObject
    {
        #region Fields
        private int life;
        private float speed;
        private static Player instance;
        #endregion
        #region Properties
        public float Speed { get => speed; set => speed = value; }
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player(new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y / 2));
                }
                return instance;
            }
        }
        #endregion
        #region Constructors
        private Player(Vector2 position) : base(position) 
        {
            speed = 300;
        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        #endregion
    }
}
