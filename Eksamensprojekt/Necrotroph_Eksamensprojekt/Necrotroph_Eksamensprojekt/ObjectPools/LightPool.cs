using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.ObjectPools
{
    public class LightPool : ObjectPool
    {
        #region Fields
        private static LightPool instance;
        #endregion
        #region Properties
        public static LightPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LightPool();
                }
                return instance;
            }
        }
        #endregion
        #region Constructors
        private LightPool()
        {

        }
        #endregion
        #region Methods
        public override GameObject GetObject(Vector2 position, params object[] consistencyData)
        {
            if (Inactive.OfType<LightRefill>().Any())
            {
                LightRefill selected = Inactive.OfType<LightRefill>().FirstOrDefault();
                Inactive.Remove(selected);
                Active.Add(selected);
                selected.Transform.WorldPosition = position;
                selected.Active = true;
                return selected;
            }
            else
            {
                return Create(position);
            }
        }

        protected override void CleanUp(GameObject obj)
        {
            if (Active.Contains(obj))
            {
                Active.Remove(obj);
                Inactive.Add(obj);
                obj.Active = false;
            }
        }

        protected override GameObject Create(Vector2 position, params object[] consistencyData)
        {
            LightRefill newLight = LightRefillFactory.Create(position);
            Active.Add(newLight);

            return newLight;
        }
        #endregion
    }
}
