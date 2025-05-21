using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt.Components
{
    /// <summary>
    /// Collider should be added after spriteRendere
    /// </summary>
    public class Collider : Component
    {
        #region Fields
        #endregion
        #region Properties
        public Rectangle Hitbox
        {
            get
            {
                return gameObject.Hitbox;
            }
        }
        #endregion
        #region Constructors
        public Collider(GameObject gameObject) : base(gameObject)
        {
        }
        #endregion
        #region Methods
        public override void OnCollision(GameObject otherObject)
        {
            float newX = otherObject.Transform.Position.X;
            float newY = otherObject.Transform.Position.Y;

            Hitbox.Deconstruct(out int x, out int y, out int w, out int h);
            otherObject.Hitbox.Deconstruct(out int x2, out int y2, out int w2, out int h2);

            int leftDif = (x) - (x2 + w2);
            int rightDif = (x + w) - (x2);
            int upperDif = (y) - (y2 + h2);
            int lowerDif = (y + h) - (y2);

            // findes the collision handle that has the smallest effect
            int xDif = GetLowerAbsoluteValue(leftDif, rightDif);
            int yDif = GetLowerAbsoluteValue(upperDif, lowerDif);
            if (MathF.Abs(xDif) < Math.Abs(yDif))
            {
                float targetDif = otherObject.Hitbox.Width / 2f + Hitbox.Width / 2f;

                //sets a new X, based on wether it colliding from the right or left.
                newX = otherObject.Transform.Position.X + (xDif > 0 ? -targetDif : targetDif);
            }
            else
            {
                float targetDif = otherObject.Hitbox.Height / 2f + Hitbox.Height / 2f;

                //sets a new Y, based on wether it colliding from above or bellow.
                newY = otherObject.Transform.Position.Y + (yDif > 0 ? -targetDif : targetDif);
            }

            otherObject.Transform.Position = new Vector2(newX, newY);
        }

        private int GetLowerAbsoluteValue(int value1, int value2)
        {
            if (MathF.Abs(value1) < MathF.Abs(value2))
            {
                return value1;
            }
            return value2;
        }

        #endregion
    }
}
