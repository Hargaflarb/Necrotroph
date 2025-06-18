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
using Necrotroph_Eksamensprojekt.States;

namespace Necrotroph_Eksamensprojekt
{
    //emama
    public class HunterEnemy : GameObject, IListener, IChaceStateImplementation
    {
        #region Fields
        private bool facingLeft = true;
        private int damage = 30;
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

        public ChaceState CurrentChaceState { get; set; } //doesn't have get/set cus it's an interface implementation
        #endregion
        #region Constructors
        private HunterEnemy(Vector2 position) : base(position)
        {
            Player.Instance.Observer.AddListener(this);
        }
        #endregion
        #region Methods
        public override void Awake()
        {
            (CurrentChaceState = new PathfindState(this, Transform, GetComponent<Movable>())).Enter(Player.Instance);
            base.Awake();
        }

        public override void Update(GameTime gameTime)
        {
            CurrentChaceState.Execute();

            Vector2 direction = GetComponent<Movable>().Direction;
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

        public void ChangeChaceState(ChaceState state)
        {
            if (CurrentChaceState is not null)
            {
                CurrentChaceState.Exit();
                CurrentChaceState = state;
                CurrentChaceState.Enter(Player.Instance);
            }
        }

        #endregion
    }
}
