using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.Components
{
    /// <summary>
    /// Component used for movable items. Contains the code for the Move function too, and the sprint function
    /// </summary>
    public class Movable : Component
    {
        #region Fields
        private float speed;
        private Vector2 direction;
        private bool standStill = false;

        #endregion
        #region Properties
        public Vector2 Direction { get => direction; set => direction = value; }
        public float Speed { get => speed; set => speed = value; }
        public bool StandStill { get => standStill; set => standStill = value; }
        #endregion
        #region Constructors
        public Movable(GameObject gameObject, float speed) : base(gameObject)
        {
            Speed = speed;
        }
        public Movable(GameObject gameObject) : base(gameObject)
        {

        }
        #endregion
        #region Methods
        public void Move(Vector2 direction, float speed)
        {
            if (!StandStill)
            {
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }

                float deltaTime = (float)GameWorld.Time.ElapsedGameTime.TotalSeconds;

                Vector2 change = ((direction * speed) * deltaTime);
                gameObject.Transform.WorldPosition += change;
            }
        }

        public override void Update(GameTime gameTime)
        {
            Move(Direction, Speed);
            Direction = Vector2.Zero;
            base.Update(gameTime);
        }

        public void Sprint(float speed)
        {
            //Player.Instance.Speed = speed;
        }

        #endregion
    }
}
