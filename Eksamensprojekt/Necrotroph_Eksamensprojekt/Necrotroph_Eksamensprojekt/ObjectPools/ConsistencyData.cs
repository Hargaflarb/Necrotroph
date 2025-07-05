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
    public class ConsistencyData
    {
        private ObjectPool poolType;
        private object[] consistencyData;
        private Vector2 position;

        public ConsistencyData(ObjectPool poolType, Vector2 position, params object[] consistencyData)
        {
            this.PoolType = poolType;
            this.ConsistencyData_ = consistencyData;
            this.Position = position;
        }

        public ObjectPool PoolType { get => poolType; set => poolType = value; }
        public object[] ConsistencyData_ { get => consistencyData; set => consistencyData = value; }
        public Vector2 Position { get => position; set => position = value; }
    }
}
