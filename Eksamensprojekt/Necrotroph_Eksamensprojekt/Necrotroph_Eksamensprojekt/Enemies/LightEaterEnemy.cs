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
using Necrotroph_Eksamensprojekt.Observer;

namespace Necrotroph_Eksamensprojekt.Enemies
{
    //emma
    public class LightEaterEnemy : GameObject, IListener
    {
        #region Fields
        private float speed = 50;
        private int damage = 10;
        private bool facingLeft = true;
        private int wanderDistance = 200;
        private Vector2 wanderLocation;
        private Random rnd = new Random();
        private bool wandering = false;
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
            Vector2 direction;
            //if the player has light on
            if (Player.Instance.LightOn)
            {
                //finds player position & moves toward it
                direction = new Vector2(Player.Instance.Transform.ScreenPosition.X - Transform.ScreenPosition.X, Player.Instance.Transform.ScreenPosition.Y - Transform.ScreenPosition.Y);
            }
            //if the player has light off
            else
            {
                if (wanderLocation == Vector2.Zero)
                {
                    wanderLocation = new Vector2(rnd.Next(0, wanderDistance), rnd.Next(0, wanderDistance));
                }
                direction = new Vector2(wanderLocation.X- Transform.ScreenPosition.X, wanderLocation.Y - Transform.ScreenPosition.Y);
                if (Transform.ScreenPosition == wanderLocation)
                {
                    wandering = false;
                }
            }
            //picks a random lolcation, moves toward it, stands around a bit, repeat
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
            GetComponent<Movable>().Move(direction, speed);
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject otherObject)
        {
            if (otherObject == Player.Instance)
            {
                Player.Instance.TakeDamage(damage, EnemyType.LightEater);
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


        #endregion
    }
}
