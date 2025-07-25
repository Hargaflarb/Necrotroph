﻿using System;
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

namespace Necrotroph_Eksamensprojekt
{
    //emama
    public class HunterEnemy : GameObject, IListener
    {
        #region Fields
        private bool facingLeft = true;
        private int damage = 30;
        private static Vector2 position;
        private Vector2 nextDestination;
        private static HunterEnemy instance;
        #endregion
        #region Properties
        public static Vector2 Position;
        public static HunterEnemy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HunterEnemy(Position);
                }
                return instance;
            }
        }
        #endregion
        #region Constructors
        private HunterEnemy(Vector2 position) : base(position)
        {
            Player.Instance.Observer.AddListener(this);
            nextDestination = Transform.WorldPosition;
        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            //finds player position & moves toward it
            nextDestination = Map.PathfoundDestination(Transform.WorldPosition, Player.Instance.Transform.WorldPosition, nextDestination);
            Vector2 direction = nextDestination - Transform.WorldPosition;
            //Vector2 direction = new Vector2(Player.Instance.Transform.ScreenPosition.X - Transform.ScreenPosition.X, Player.Instance.Transform.ScreenPosition.Y - Transform.ScreenPosition.Y);
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
            //double remap = Math.Atan2(direction.Y, direction.X);
            //float XDirection = (float)Math.Cos(remap);
            //float YDirection = (float)Math.Sin(remap);
            //direction = new Vector2(XDirection, YDirection);
            direction.Normalize();
            HunterEnemy.Instance.GetComponent<Movable>().Direction += direction;
            base.Update(gameTime);
        }

        public override void OnCollision(GameObject otherObject)
        {
            if (otherObject == Player.Instance)
            {
                Player.Instance.TakeDamage(damage, EnemyType.Hunter);
            }
            base.OnCollision(otherObject);
        }

        public void HearFromObserver(IObserver observer)
        {
            if (observer is DeathObserver)
            {
                GetComponent<Movable>().StandStill = true;
            }
        }


        #endregion
    }
}
