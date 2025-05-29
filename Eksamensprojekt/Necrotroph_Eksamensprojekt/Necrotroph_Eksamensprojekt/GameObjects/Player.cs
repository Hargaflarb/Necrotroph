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
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class Player : GameObject, ISubject
    {
        #region Fields
        private int maxLife = 100;
        private int life;
        private float speed;
        private Vector2 direction;
        private static Player instance;
        private static bool lightOn = true;
        private DeathObserver observer;
        private float invincibilityTime = 1f;
        private bool invincible = false;
        #endregion
        #region Properties
        public float Speed { get => speed; set => speed = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
        public int Life { get => life; }
        public DeathObserver Observer { get => observer; set => observer = value; }
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

        /// <summary>
        /// When the player takes damage, but doesn't necessarily die
        /// </summary>
        /// <param name="damage">The amount of damage taken</param>
        /// <param name="source">Which enemy caused the damage</param>
        public void TakeDamage(int damage, EnemyType source)
        {
            if (!invincible)
            {
                invincible = true;
                if (!lightOn)
                {
                    PlayerDeath(source);
                }
                life -= damage;
                GetComponent<LightEmitter>().LightRadius = ((float)life / 500f)+0.01f;
                TimeLineManager.AddEvent(invincibilityTime * 1000, UndoInvincibility);
                if (life <= 0)
                {
                    lightOn = false;
                    GetComponent<LightEmitter>().LightRadius = 0;
                }
            }
        }
        public void UndoInvincibility()
        {
            invincible = false;
        }
        /// <summary>
        /// When the player is outright killed by an enemy
        /// </summary>
        /// <param name="source">Which enemy caused the death</param>
        public void PlayerDeath(EnemyType source)
        {
            NotifyObserver();
            GetComponent<Movable>().StandStill = true;
        }

        public void AttachObserver(IObserver observer)
        {
            this.observer = (DeathObserver)observer;
        }

        public void DetachObserver(IObserver observer)
        {
            if (this.observer == observer)
            {
                this.observer = null;
            }
        }

        public void NotifyObserver()
        {
            observer.Update();
        }
        #endregion
    }
}
