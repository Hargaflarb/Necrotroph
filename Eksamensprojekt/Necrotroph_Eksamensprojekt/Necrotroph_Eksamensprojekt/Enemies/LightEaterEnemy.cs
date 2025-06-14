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
using Necrotroph_Eksamensprojekt.Menu;
using Necrotroph_Eksamensprojekt.ObjectPools;
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt.Enemies
{
    //emma
    public class LightEaterEnemy : GameObject, IListener
    {
        #region Fields
        private float speed = 300;
        private float chaseSpeed = 300;
        private float wanderSpeed = 70;
        private int damage = 10;
        private bool facingLeft = false;
        private float standAroundTime = 5;
        private int wanderDistance = 500;
        private Vector2 wanderLocation;
        private bool wandering = false;
        private float sightDistance = 500;
        private bool hadLightOn = false;
        private Vector2 direction;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public LightEaterEnemy(Vector2 position) : base(position)
        {
            Player.Instance.Observer.AddListener(this);
        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            //despawns this enemy if it strays too far from the player
            Vector2 dif = Transform.WorldPosition - Player.Instance.Transform.WorldPosition;
            if (MathF.Abs(dif.X) > Map.UnloadBound.X | MathF.Abs(dif.Y) > Map.UnloadBound.Y)
            {
                InGame.Instance.RemoveObject(this);
            }

            //if the player has light on
            if (Player.Instance.LightOn && Vector2.Distance(Player.Instance.Transform.ScreenPosition, Transform.ScreenPosition) <= sightDistance)
            {
                //finds player position & moves toward it
                direction = new Vector2(Player.Instance.Transform.ScreenPosition.X - Transform.ScreenPosition.X, Player.Instance.Transform.ScreenPosition.Y - Transform.ScreenPosition.Y);
                wandering = false;
                hadLightOn = true;
            }
            //if the player has light off
            else
            {
                if (hadLightOn)
                {
                    wandering = false;
                    hadLightOn = false;
                    TimeLineManager.AddEvent(standAroundTime * 1000, Wander);
                }
                if (wandering)
                {
                    direction = new Vector2(wanderLocation.X - Transform.WorldPosition.X, wanderLocation.Y - Transform.WorldPosition.Y);
                }

                if (Vector2.Distance(Transform.WorldPosition, wanderLocation) < 40)
                {
                    wandering = false;
                    TimeLineManager.AddEvent(standAroundTime * 1000, Wander);
                }
            }
            //picks a random location, moves toward it, stands around a bit, repeat
            if (direction.X > 0 && facingLeft)
            {
                GetComponent<SpriteRenderer>().Flipped = false;
                facingLeft = false;
            }
            else if (direction.X <= 0 && !facingLeft)
            {
                GetComponent<SpriteRenderer>().Flipped = true;
                facingLeft = true;
            }
            if (wandering || Player.Instance.LightOn)
            {
                double remap = Math.Atan2(direction.Y, direction.X);
                float XDirection = (float)Math.Cos(remap);
                float YDirection = (float)Math.Sin(remap);
                direction = new Vector2(XDirection, YDirection);
                direction.Normalize();
                if (Player.Instance.LightOn)
                {
                    GetComponent<Movable>().Move(direction, speed);
                }
                else
                {
                    GetComponent<Movable>().Move(direction, wanderSpeed);
                }
            }
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject otherObject)
        {
            if (otherObject == Player.Instance)
            {
                Player.Instance.TakeDamage(damage, EnemyType.LightEater);
                //slow down for a bit to give the player a chance to escape
                speed = wanderSpeed;
                TimeLineManager.AddEvent(0.5f * 1000, SpeedUp);
            }
            base.OnCollision(otherObject);
        }
        public void HearFromObserver(IObserver observer)
        {
            if (observer is DeathObserver)
            {
                ((Movable)GetComponent<Movable>()).StandStill = true;
            }
        }

        public void SpeedUp()
        {
            speed = chaseSpeed;
        }

        public void Wander()
        {
            wandering = true;
            wanderLocation = new Vector2(GameWorld.Rnd.Next((int)Transform.WorldPosition.X - wanderDistance, (int)Transform.WorldPosition.X + wanderDistance), GameWorld.Rnd.Next((int)Transform.WorldPosition.Y - wanderDistance, (int)Transform.WorldPosition.Y + wanderDistance));
            direction = new Vector2(wanderLocation.X - Transform.WorldPosition.X, wanderLocation.Y - Transform.WorldPosition.Y);
        }
        #endregion
    }
}
