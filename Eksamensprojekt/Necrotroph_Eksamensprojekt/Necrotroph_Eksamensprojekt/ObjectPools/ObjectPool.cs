using Necrotroph_Eksamensprojekt.GameObjects;
using Necrotroph_Eksamensprojekt.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.ObjectPools
{
    //emma
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
        public abstract GameObject GetObject(Vector2 position, params object[] consistencyData);

        public void ReleaseObject(GameObject obj)
        {
            if (Active.Contains(obj))
            {
                Active.Remove(obj);
                Inactive.Add(obj);
                InGame.Instance.RemoveObject(obj);
                Map.TryAddObjectToMap(obj);
                obj.Active = false;
            }
        }

        protected abstract GameObject Create(Vector2 position, params object[] consistencyData);

        protected abstract void CleanUp(GameObject obj);

        public void Clear()
        {
            for (int i = active.Count - 1; i >= 0; i--)
            {
                GameObject gameObject = active[i];
                InGame.Instance.RemoveObject(gameObject);
                active.Remove(gameObject);
            }
        }
        #endregion
    }
}
