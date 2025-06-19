using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    //Echo
    public class Memorabilia : GameObject
    {
        private int key;

        public Memorabilia(int key, Vector2 position) : base(position)
        {
            AddComponent<LightEmitter>(0.05f);
            this.key = key;
            Transform.WorldPosition = position;
        }
        /// <summary>
        /// Runs the collision and updates ItemsCollected
        /// </summary>
        /// <param name="otherObject"></param>
        public override void OnCollision(GameObject otherObject)
        {
            if (otherObject is Player)
            {
                InGame.Instance.ItemsCollected++;
                InGame.Instance.ActiveMemorabilia.Remove(key);
                
                this.GetComponent<Pickupable>().RemoveObject();
               
                base.OnCollision(otherObject);
            }
        }
    }
}
