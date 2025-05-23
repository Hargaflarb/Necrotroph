using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt
{
    public class HunterEnemy : GameObject
    {
        #region Fields
        private float speed = 50;
        private bool facingLeft = true;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public HunterEnemy(Vector2 position) : base(position)
        {

        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            //finds player position & moves toward it
            Vector2 direction = new Vector2(Player.Instance.Transform.ScreenPosition.X - Transform.ScreenPosition.X, Player.Instance.Transform.ScreenPosition.Y - Transform.ScreenPosition.Y);
            if (direction.X > 0 && facingLeft)
            {
                GetComponent<SpriteRenderer>().Flipped = true;
                facingLeft = false;
            }
            else if (direction.X < 0 && !facingLeft)
            {
                GetComponent<SpriteRenderer>().Flipped = false;
                facingLeft = true;
            }
            double remap = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(remap);
            float YDirection = (float)Math.Sin(remap);
            direction = new Vector2(XDirection, YDirection);
            direction.Normalize();
            ((Movable)GetComponent<Movable>()).Move(direction, speed);
            base.Update(gameTime);
        }
        #endregion
    }
}
