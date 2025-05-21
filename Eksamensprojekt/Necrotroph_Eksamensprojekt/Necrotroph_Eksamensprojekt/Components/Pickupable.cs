using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.Components
{
    public class Pickupable : Component
    {
        #region Fields

        #endregion
        #region Properties
        #endregion
        #region Constructor
        public Pickupable(GameObject gameObject) : base(gameObject)
        {
            this.gameObject = gameObject;
        }
        #endregion
        #region Methods
        public override void OnCollision(GameObject otherObject)
        {
            GameWorld.Instance.RemoveObject(this.gameObject);
        }
        #endregion
    }
}
    