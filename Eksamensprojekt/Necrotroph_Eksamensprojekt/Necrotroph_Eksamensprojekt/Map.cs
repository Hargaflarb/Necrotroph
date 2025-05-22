using Necrotroph_Eksamensprojekt.ObjectPools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt
{
    public static class Map
    {
        private static Random random;
        private static readonly Vector2 size;
        private static readonly Vector2 loadedSize;

        static Map()
        {
            random = new Random();
            size = new Vector2(10000, 10000);
            loadedSize = new Vector2(3000, 3000);
        }

        public static void CheckForObejctsToLoad(Vector2 playerWorldPos)
        {
            // trees
            foreach (Tree tree in TreePool.Instance.Inactive)
            {
                


            }
        }
    }
}
