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
using Necrotroph_Eksamensprojekt.Menu;
using Necrotroph_Eksamensprojekt.PathFinding;
using System.Reflection.Metadata.Ecma335;


namespace Necrotroph_Eksamensprojekt
{
    /// <summary>
    /// Malthe
    /// </summary>
    public static class Map
    {
        #region Fields
        private const float treeSpacing = 800;
        private const float NodeSpacing = 200;
        private static readonly Vector2 size;
        private static readonly Vector2 loadBound;
        private static readonly Vector2 unloadBound;
        private static List<(Vector2 position, ObjectPool poolType)> unloadedMapObjects;
        private static Graph pathFindingGraph;
        private static Random rnd;
        #endregion
        #region Properties
        public static Vector2 UnloadBound { get { return unloadBound; } }
        public static Random Rnd;
        #endregion
        #region Constructors
        static Map()
        {
            Rnd = new Random(GameWorld.Seed);
            size = new Vector2(10000, 10000);
            loadBound = new Vector2(1250, 900);
            unloadBound = new Vector2(1350, 1000);
            unloadedMapObjects = new List<(Vector2 position, ObjectPool poolType)>();
            pathFindingGraph = new Graph();
        }
        #endregion
        #region Methods
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

            //lights
            foreach(LightRefill light in LightPool.Instance.Active.ToList())
            {
                Vector2 dif = light.Transform.WorldPosition - Player.Instance.Transform.WorldPosition;
                if (MathF.Abs(dif.X) > unloadBound.X | MathF.Abs(dif.Y) > unloadBound.Y)
                {
                    LightPool.Instance.ReleaseObject(light);
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
                    Vector2 offset = new Vector2(GameWorld.Rnd.Next(50) - 25, GameWorld.Rnd.Next(50) - 25);
                    Vector2 treePos = new Vector2(x, y) * treeSpacing;
                    unloadedMapObjects.Add(((new Vector2(x, y) * treeSpacing) + offset*6, TreePool.Instance));

                    float halfSpacing = treeSpacing * 0.5f;
                    Node FromNodeCenter = pathFindingGraph.AddNode((int)(treePos.X + halfSpacing), (int)(treePos.Y + halfSpacing));
                    Node FromNodeVertical = pathFindingGraph.AddNode((int)(treePos.X + halfSpacing), (int)treePos.Y);
                    Node FromNodeHorizontal = pathFindingGraph.AddNode((int)treePos.X, (int)(treePos.Y + halfSpacing));

                    pathFindingGraph.AddEdge(FromNodeCenter, FromNodeVertical);
                    pathFindingGraph.AddEdge(FromNodeCenter, FromNodeHorizontal);

                    Node ToNodeVertical = pathFindingGraph.FindNearestNode(((int)(treePos.X + halfSpacing), (int)(treePos.Y - halfSpacing)), 10, out bool success1);
                    if (success1)
                    {
                        //pathFindingGraph.AddEdge(FromNodeCenter, ToNodeVertical);
                        pathFindingGraph.AddEdge(FromNodeVertical, ToNodeVertical);
                    }

                    Node ToNodeHorisontal = pathFindingGraph.FindNearestNode(((int)(treePos.X - halfSpacing), (int)(treePos.Y + halfSpacing)), 10, out bool success2);
                    if (success2)
                    {
                        //pathFindingGraph.AddEdge(FromNodeCenter, ToNodeHorisontal);
                        pathFindingGraph.AddEdge(FromNodeHorizontal, ToNodeHorisontal);
                    }
                }
            }
            CheckForObjectsToUnload();
            InGame.Instance.AddAndRemoveGameObjects();
        }

        /// <summary>
        /// Using Astar, the next sub-destination is found.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="finalDestination"></param>
        /// <param name="nextDestination"></param>
        /// <returns></returns>
        public static Vector2 PathfoundDestination(Vector2 position, Vector2 finalDestination, Vector2 nextDestination)
        {
            // else if a new path is to be found.

            Node endingNode = pathFindingGraph.FindNearestNode(((int)finalDestination.X, (int)finalDestination.Y), out bool endSuccess);
            if (!endSuccess)
            {
                // shouldn't happen.
                return new Vector2(0, 0);
            }

            Node startingNode = pathFindingGraph.FindNearestNode(((int)position.X, (int)position.Y), out bool startSuccess);

            // if starting and ending point is the same - pathfinding isn't needed.
            if (endingNode == startingNode)
            {
                return finalDestination;
            }

            // if it's still aproching a Node.
            if ((float)Math.Sqrt(Math.Pow(nextDestination.X - position.X, 2) + Math.Pow(nextDestination.Y - position.Y, 2)) > 70)
            {
                // could add a "if distance to final is shorter; go to final"
                return nextDestination;
            }


            LinkedList<Node> path = Graph.AStar(startingNode, endingNode, out bool AStarSuccess);

            nextDestination = new Vector2(path.First.Next.Value.X, path.First.Next.Value.Y);
            return nextDestination;
            
        }

        public static void Clear()
        {
            unloadedMapObjects.Clear();
            pathFindingGraph.ClearNodes();
        }

        #endregion
    }
}
