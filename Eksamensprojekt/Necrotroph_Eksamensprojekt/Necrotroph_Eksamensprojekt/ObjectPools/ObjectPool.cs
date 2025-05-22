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
        #endregion
        #region Constructors
        #endregion
        #region Methods
        public GameObject GetObject()
        {
            return null;
        }
        public void ReleaseObject(GameObject obj)
        {

        }

        protected abstract GameObject Create();

        protected abstract void CleanUp(GameObject obj);
        #endregion
    }
}
