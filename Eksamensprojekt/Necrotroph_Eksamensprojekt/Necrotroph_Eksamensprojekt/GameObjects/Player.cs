using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class Player : GameObject, ISubject
    {
        #region Fields
        private int maxLife = 100;
        private float life;
        private static Player instance;
        private bool lightOn = true;
        private DeathObserver observer;
        private float invincibilityTime = 1f;
        private bool invincible = false;
        private int walkSFX1;
        private int walkSFX2;
        private int damageSFX1;
        private int damageSFX2;
        private int deathSFX;
        private int lightToggleSFX;
        private bool oneSoundPlayed = false;
        #endregion
        #region Properties
        public float Life { get => life; set => life = value; }
        public DeathObserver Observer { get => observer; set => observer = value; }
        public bool LightOn
        {
            get => lightOn;
            set
            {
                lightOn = value;
                LightOnOff();
            }
        }
        public bool IsMoving
        {
            get
            {
                return (GetComponent<Movable>().Direction != Vector2.Zero);
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
            life = maxLife;

        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            if (walkSFX1 == 0)
            {
                walkSFX1 = SoundManager.Instance.PlaySFX("PlayerWalk1", Transform.ScreenPosition);
                walkSFX2 = SoundManager.Instance.PlaySFX("PlayerWalk2", Transform.ScreenPosition);
                damageSFX1 = SoundManager.Instance.PlaySFX("PlayerDamaged1", Transform.ScreenPosition);
                damageSFX2 = SoundManager.Instance.PlaySFX("PlayerDamaged2", Transform.ScreenPosition);
                deathSFX = SoundManager.Instance.PlaySFX("PlayerDeath", Transform.ScreenPosition);
                lightToggleSFX = SoundManager.Instance.PlaySFX("PlayerLightToggle", Transform.ScreenPosition);
                SoundManager.Instance.PauseSFX(walkSFX1);
                SoundManager.Instance.PauseSFX(walkSFX2);
                SoundManager.Instance.PauseSFX(damageSFX1);
                SoundManager.Instance.PauseSFX(damageSFX2);
                SoundManager.Instance.PauseSFX(deathSFX);
                SoundManager.Instance.PauseSFX(deathSFX);
                SoundManager.Instance.PauseSFX(lightToggleSFX);
            }
            if (IsMoving)
            {
                SoundManager.Instance.ResumeSFX(walkSFX1);
                //SoundManager.Instance.ResumeSFX(walkSFX2);
            }
            else
            {
                SoundManager.Instance.PauseSFX(walkSFX1);
                //SoundManager.Instance.PauseSFX(walkSFX2);
            }
            if (lightOn)
            {
                life -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (life > 100)
                {
                    GetComponent<LightEmitter>().LightRadius = (100 / 500f) + 0.01f;
                }
                else
                {
                    GetComponent<LightEmitter>().LightRadius = ((float)life / 500f) + 0.01f;
                }
                if (life <= 0)
                {
                    lightOn = false;
                }
            }
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
                else
                {
                    SoundManager.Instance.PauseSFX(damageSFX1);
                    SoundManager.Instance.PauseSFX(damageSFX2);
                    switch (oneSoundPlayed)
                    {
                        case true:
                            SoundManager.Instance.ResumeSFX(damageSFX2);
                            break;
                        case false:
                            SoundManager.Instance.ResumeSFX(damageSFX1);
                            break;
                    }
                    oneSoundPlayed = !oneSoundPlayed;
                }
                    life -= damage;

                GetComponent<LightEmitter>().LightRadius = ((float)life / 500f) + 0.01f;
                TimeLineManager.AddEvent(invincibilityTime * 1000, UndoInvincibility);
                if (life <= 0 && lightOn)
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
            lightOn = false;
            SoundManager.Instance.ResumeSFX(deathSFX);
            GetComponent<LightEmitter>().LightRadius = 0f;
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

        private void LightOnOff()
        {
            SoundManager.Instance.PauseSFX(lightToggleSFX);
            SoundManager.Instance.ResumeSFX(lightToggleSFX);
            if (lightOn)
            {
                GetComponent<LightEmitter>().LightRadius = ((float)life / 500f) + 0.01f;
            }
            else
            {
                GetComponent<LightEmitter>().LightRadius = 0f;
            }
        }
        #endregion
    }
}
