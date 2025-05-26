using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Components;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class Player : GameObject
    {
        #region Fields
        private int maxLife = 100;
        private int life;
        private float speed;
        private Vector2 direction;
        private static Player instance;
        #endregion
        #region Properties
        public float Speed { get => speed; set => speed = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
        public int Life { get => life;}
        public bool IsMoving
        {
            get
            {
                return (direction != Vector2.Zero);
            }
        }
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player(Vector2.Zero);
                }
                return instance;
            }
        }
        #endregion
        #region Constructors
        private Player(Vector2 position) : base(position)
        {
            speed = 300;
            life = maxLife;
        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void TakeDamage(int damage)
        {
            DeathObserver.CheckDeath();
            life -= damage;
        }
        #endregion
    }
}
