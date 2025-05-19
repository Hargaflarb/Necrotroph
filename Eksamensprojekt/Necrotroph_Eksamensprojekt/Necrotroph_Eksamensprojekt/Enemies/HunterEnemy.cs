using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Necrotroph_Eksamensprojekt.Components;

namespace Necrotroph_Eksamensprojekt
{
    public class HunterEnemy : GameObject
    {
        #region Fields
        private float speed = 50;
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
            Vector2 direction = new Vector2(GameWorld.Player.Transform.Position.X - Transform.Position.X, GameWorld.Player.Transform.Position.Y - Transform.Position.Y);

            double remap = Math.Atan2(direction.Y, direction.X);
            float XDirection = (float)Math.Cos(remap);
            float YDirection = (float)Math.Sin(remap);
            direction = new Vector2(XDirection, YDirection);
            direction.Normalize();
            ((Movable)GetComponent<Movable>()).Move(direction,speed);
            base.Update(gameTime);
        }
        #endregion
    }
}
