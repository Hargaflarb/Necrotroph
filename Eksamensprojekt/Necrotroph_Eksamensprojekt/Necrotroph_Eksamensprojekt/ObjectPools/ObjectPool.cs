using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Necrotroph_Eksamensprojekt.GameObjects;

namespace Necrotroph_Eksamensprojekt.ObjectPools
{
    public abstract class ObjectPool
    {
        #region Fields
        protected static List<GameObject> active = new List<GameObject>();
        protected static List<GameObject> inactive = new List<GameObject>();
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public abstract GameObject GetObject(Vector2 position);

        public void ReleaseObject(GameObject obj)
        {
            if (active.Contains(obj))
            {
                active.Remove(obj);
                inactive.Add(obj);
                obj.Active = false;
            }
        }

        protected abstract GameObject Create(Vector2 position);

        protected abstract void CleanUp(GameObject obj);
        #endregion
    }
}
