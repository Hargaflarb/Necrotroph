using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.Enemies
{
    public class LurkerEnemy : GameObject
    {
        #region Fields
        private bool hiding = true;
        private bool fleeing = false;
        private bool playerPassed = false;
        private byte stage;
        private float speed = 80;
        private float sightRange = 200;
        private int damage = 70;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public LurkerEnemy(Vector2 position) : base(position)
        {
        }
        #endregion
        #region Methods
        public override void Update(GameTime gameTime)
        {
            //probably better to do this with states
            if (hiding)
            {
                //once entering the screen, check if stage >4, if yes, switch 'attacking' on, set stage to 0 & stop the rest of this Update
                //check if player was nearby
                if (Vector2.Distance(Player.Instance.Transform.ScreenPosition, Transform.ScreenPosition) <= sightRange && !playerPassed)
                {
                    playerPassed = true;
                }
                //check what kind of tree it is & change sprite accordingly
                //when exiting the screen, 
                Vector2 dif = Transform.WorldPosition - Player.Instance.Transform.WorldPosition;
                if (MathF.Abs(dif.X) > Map.UnloadBound.X | MathF.Abs(dif.Y) > Map.UnloadBound.Y)
                {
                    //if outside of screen, change position to tree at edge of screen, in the direction the player is moving
                    if (playerPassed)
                    {
                        stage++;
                        playerPassed = false;
                    }
                }
            }
            else if (fleeing)
            {
                //run directly away from the player
                Vector2 direction;
                direction = new Vector2(Player.Instance.Transform.ScreenPosition.X + Transform.ScreenPosition.X, Player.Instance.Transform.ScreenPosition.Y + Transform.ScreenPosition.Y);
                double remap = Math.Atan2(direction.Y, direction.X);
                float XDirection = (float)Math.Cos(remap);
                float YDirection = (float)Math.Sin(remap);
                direction = new Vector2(XDirection, YDirection);
                direction.Normalize();
                GetComponent<Movable>().Move(direction, speed);
                //when exiting the screen, despawn
                Vector2 dif = Transform.WorldPosition - Player.Instance.Transform.WorldPosition;
                if (MathF.Abs(dif.X) > Map.UnloadBound.X | MathF.Abs(dif.Y) > Map.UnloadBound.Y)
                {
                    InGame.Instance.RemoveObject(this);
                }
            }
            else
            {
                //run at the player, directly
                Vector2 direction;
                direction = new Vector2(Player.Instance.Transform.ScreenPosition.X - Transform.ScreenPosition.X, Player.Instance.Transform.ScreenPosition.Y - Transform.ScreenPosition.Y);
                double remap = Math.Atan2(direction.Y, direction.X);
                float XDirection = (float)Math.Cos(remap);
                float YDirection = (float)Math.Sin(remap);
                direction = new Vector2(XDirection, YDirection);
                direction.Normalize();
                GetComponent<Movable>().Move(direction, speed);
            }
            base.Update(gameTime);
        }
        public override void OnCollision(GameObject otherObject)
        {
            if (otherObject == Player.Instance && !hiding)
            {
                Player.Instance.TakeDamage(damage, EnemyType.Stalker);
            }
            base.OnCollision(otherObject);
        }
        #endregion
    }
}
