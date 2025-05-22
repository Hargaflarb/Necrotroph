using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.ObjectPools
{
    public abstract class ObjectPool
    {
        #region Fields
        protected List<GameObject> active = new List<GameObject>();
        protected List<GameObject> inactive = new List<GameObject>();

        #endregion
        #region Properties
        public List<GameObject> Active { get => active; }
        public List<GameObject> Inactive { get => inactive; }
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public abstract GameObject GetObject(Vector2 position);

        public void ReleaseObject(GameObject obj)
        {
            if (Active.Contains(obj))
            {
                Active.Remove(obj);
                Inactive.Add(obj);
                GameWorld.Instance.RemoveObject(obj);
                Map.TryAddObjectToMap(obj);
                obj.Active = false;
            }
        }

        protected abstract GameObject Create(Vector2 position);

        protected abstract void CleanUp(GameObject obj);
        #endregion
    }
}
