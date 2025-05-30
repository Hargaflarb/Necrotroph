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
using System.Security.Cryptography;
using Necrotroph_Eksamensprojekt.Menu;

namespace Necrotroph_Eksamensprojekt
{
    public static class Map
    {
        private const float treeSpacing = 800;
        private static Random random;
        private static readonly Vector2 size;
        private static readonly Vector2 loadBound;
        private static readonly Vector2 unloadBound;
        private static List<(Vector2 position, ObjectPool poolType)> unloadedMapObjects;

        static Map()
        {
            random = new Random();
            size = new Vector2(10000, 10000);
            loadBound = new Vector2(1250, 900);
            unloadBound = new Vector2(1350, 1000);
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
                    InGame.Instance.AddObject(mapObject.poolType.GetObject(mapObject.position));
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


        public static void GenerateMap()
        {
            float widthAmount = (size.X / treeSpacing) * 0.5f;
            float heightAmount = (size.Y / treeSpacing) * 0.5f;
            for (float x = -widthAmount; x < widthAmount; x++)
            {
                for (float y = -heightAmount; y < heightAmount; y++)
                {
                    Vector2 offset = new Vector2(random.Next(50) - 25, random.Next(50) - 25);
                    unloadedMapObjects.Add(((new Vector2(x, y) * treeSpacing) + offset*6, TreePool.Instance));
                }
            }
            CheckForObjectsToUnload();
            InGame.Instance.AddAndRemoveGameObjects();
        }
    }
}
