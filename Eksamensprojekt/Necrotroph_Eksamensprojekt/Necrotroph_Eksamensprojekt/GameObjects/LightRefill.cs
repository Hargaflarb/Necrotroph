using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.GameObjects
{
    public class LightRefill : GameObject
    {
        #region Fields
        private float refillValue=20;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        public LightRefill(Vector2 position) : base(position)
        {
            Transform.WorldPosition = position;
        }
        #endregion
        #region Methods
        public override void OnCollision(GameObject otherObject)
        {
            if (otherObject is Player)
            {
                Player.Instance.Life += refillValue;
                SoundManager.Instance.ResumeSFX("PlayerPickUpLight");
                this.GetComponent<Pickupable>().RemoveObject();

                base.OnCollision(otherObject);
            }
        }
        #endregion
    }
}
