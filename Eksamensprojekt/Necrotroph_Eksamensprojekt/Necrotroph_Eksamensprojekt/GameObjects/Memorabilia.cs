using Necrotroph_Eksamensprojekt.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class Memorabilia : GameObject
    {
        public Memorabilia(Vector2 position) : base(position)
        {
            AddComponent<LightEmitter>(0.05f);
            Transform.WorldPosition = position;
        }

        public override void OnCollision(GameObject otherObject)
        {
            if (otherObject is Player)
            {
                GameWorld.Instance.ItemsCollected++;
                this.GetComponent<Pickupable>().RemoveObject();
                base.OnCollision(otherObject);

            }
        }
    }
}
