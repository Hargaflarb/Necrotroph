using Necrotroph_Eksamensprojekt.ObjectPools;
using Necrotroph_Eksamensprojekt.Commands;
using Necrotroph_Eksamensprojekt.Components;
using Necrotroph_Eksamensprojekt.Factories;
using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace Necrotroph_Eksamensprojekt
{
    public static class Map
    {
        private static Random random;
        private static readonly Vector2 size;
        private static readonly Vector2 loadBound;
        private static readonly Vector2 unloadBound;
        private static List<(Vector2 position, ObjectPool poolType)> unloadedMapObjects;

        static Map()
        {
            random = new Random();
            size = new Vector2(10000, 10000);
            loadBound = new Vector2(1500, 1500);
            unloadBound = new Vector2(1600, 1600);
            unloadedMapObjects = new List<(Vector2 position, ObjectPool poolType)>();
        }

        /// <summary>
        /// Checks the Objects pools to see if any 
        /// </summary>
        public static void CheckForObejctsToLoad()
        {
            // trees
            foreach ((Vector2 position, ObjectPool poolType) mapObject in unloadedMapObjects.ToList()) //the ToList() just makes a copy
            {
                Vector2 dif = mapObject.position - Player.Instance.Transform.WorldPosition;
                if (MathF.Abs(dif.X) < loadBound.X & MathF.Abs(dif.Y) < loadBound.Y)
                {
                    GameWorld.Instance.AddObject(mapObject.poolType.GetObject(mapObject.position));
                    unloadedMapObjects.Remove(mapObject);
                }

            }

        }

        public static void CheckForObjectsToUnload()
        {
            // trees
            foreach (Tree tree in TreePool.Instance.Active.ToList()) //the ToList() just makes a copy
            {
                Vector2 dif = tree.Transform.WorldPosition - Player.Instance.Transform.WorldPosition;
                if (MathF.Abs(dif.X) > unloadBound.X | MathF.Abs(dif.Y) > unloadBound.Y)
                {
                    TreePool.Instance.ReleaseObject(tree);
                }
            }

        }


        public static bool TryAddObjectToMap(GameObject gameObject)
        {
            bool success = false;
            if (gameObject is Tree)
            {
                unloadedMapObjects.Add((gameObject.Transform.WorldPosition, TreePool.Instance));
                success = true;
            }

            return success;
        }
    }
}
