using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Necrotroph_Eksamensprojekt.Components
{
    public class Movable : Component
    {
        #region Fields
        private float speed;
        private Vector2 direction;

        #endregion
        #region Properties
        #endregion
        #region Constructors
        public Movable(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
        #endregion
        #region Methods
        public void Move(Vector2 direction, float speed)
        {
            float deltaTime = (float)GameWorld.Time.ElapsedGameTime.TotalSeconds;

            Vector2 change = ((direction * speed) * deltaTime);
            gameObject.Transform.Position += change;
        }
        public override void Execute()
        {

        }
        #endregion
    }
}
