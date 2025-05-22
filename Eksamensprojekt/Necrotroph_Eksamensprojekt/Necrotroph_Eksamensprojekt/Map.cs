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
        private static Vector2 size;

        static Map()
        {
            random = new Random();
            size = new Vector2(10000, 10000);
        }

        public static void CheckForObejctsToLoad(Vector2 playerWorldPos)
        {

        }
    }
}
