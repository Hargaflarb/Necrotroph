using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.ObjectPools
{
    /// <summary>
    /// An interface that states what data to save, when the object is unloaded.
    /// </summary>
    public interface IConsistencyData
    {
        public ObjectPool PoolType { get; }

        public object[] GetConsistencyData();

        public Transform Transform { get; }
    }
}
