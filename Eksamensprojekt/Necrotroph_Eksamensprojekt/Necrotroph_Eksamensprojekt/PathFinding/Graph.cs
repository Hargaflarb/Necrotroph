using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.PathFinding
{
    /// <summary>
    /// Represents a collection of <see cref="Node"/>'s used for pathfinding.
    /// </summary>
    public sealed class Graph
    {
        private Dictionary<(int, int), Node> nodes;

        internal Dictionary<(int, int), Node> Nodes { get => nodes; private set => nodes = value; }

        /// <summary>
        /// Constructs a new <see cref="Graph"/>.
        /// </summary>
        public Graph()
        {
            nodes = new Dictionary<(int, int), Node>();
        }

        /// <summary>
        /// Adds a <see cref="Node"/> to the <see cref="Graph"/>.
        /// </summary>
        /// <param name="x">The x-coordinate of the added <see cref="Node"/>.</param>
        /// <param name="y">The y-coordinate of the added <see cref="Node"/>.</param>
        /// <returns>The added <see cref="Node"/>.</returns>
        public Node AddNode(int x, int y)
        {
            Node node = new Node(x, y);
            Nodes.Add((x, y), node);
            return node;
        }
        /// <summary>
        /// Adds a range of <see cref="Node"/>'s.
        /// </summary>
        /// <param name="coords">The <see cref="Node"/>' corosponding coordinates</param>
        public void AddNodes((int x, int y)[] coords)
        {
            foreach ((int x, int y) coord in coords)
            {
                Nodes.Add((coord.x, coord.y), new Node(coord.x, coord.y));
            }
        }

        /// <summary>
        /// Removes a <see cref="Node"/> from the <see cref="Graph"/>.
        /// </summary>
        /// <param name="x">The x-coordinate of the removed <see cref="Node"/>.</param>
        /// <param name="y">The y-coordinate of the removed <see cref="Node"/>.</param>
        /// <returns><see cref="true"/> if the <see cref="Node"/> was successfully removed.</returns>
        public bool RemoveNode(int x, int y)
        {
            return nodes.Remove((x, y));
        }
        /// <summary>
        /// Removes a <see cref="Node"/> from the <see cref="Graph"/>.
        /// </summary>
        /// <param name="node">The <see cref="Node"/> to be removed.</param>
        /// <returns><see cref="true"/> if the <see cref="Node"/> was successfully removed.</returns>
        public bool RemoveNode(Node node)
        {
            return nodes.Remove((node.X, node.Y));
        }

        /// <summary>
        /// Clears all nodes from the <see cref="Graph"/>.
        /// </summary>
        public void ClearNodes()
        {
            nodes.Clear();
        }

        /// <summary>
        /// Adds an <see cref="Edge"/> to and from both <see cref="Node"/>'s.
        /// </summary>
        /// <param name="length">The distance between the two <see cref="Node"/>'s.</param>
        /// <param name="fromNode">The first <see cref="Node"/>.</param>
        /// <param name="toNode">The second <see cref="Node"/>.</param>
        /// <param name="weight">The value that the <see cref="Edge"/>'s are weighted with.<code nl="\n"/>
        /// A higher number means it's less likely to be travled through.</param>
        /// <remarks>
        /// Only use <see cref="Node"/>'s that is from this <see cref="Graph"/>.
        /// </remarks>
        public void AddEdge(float length, Node fromNode, Node toNode, float weight = 1)
        {
            new Edge(fromNode, toNode, length, weight);
            new Edge(toNode, fromNode, length, weight);
        }
        /// <summary>
        /// Adds an <see cref="Edge"/> to and from both <see cref="Node"/>'s.
        /// </summary>
        /// <param name="fromNode">The first <see cref="Node"/>.</param>
        /// <param name="toNode">The second <see cref="Node"/>.</param>
        /// <param name="weight">The value that the <see cref="Edge"/>'s are weighted with.<code nl="\n"/>
        /// A higher number means it's less likely to be travled through.</param>
        /// <remarks>
        /// Only use <see cref="Node"/>'s that is from this <see cref="Graph"/>.
        /// </remarks>
        public void AddEdge(Node fromNode, Node toNode, float weight = 1)
        {
            new Edge(fromNode, toNode, weight);
            new Edge(toNode, fromNode, weight);
        }

        /// <summary>
        /// Tries to add an <see cref="Edge"/> to and from both <see cref="Node"/>'s.
        /// </summary>
        /// <param name="length">The length of the <see cref="Edge"/>.</param>
        /// <param name="fromNode">The first <see cref="Node"/>.</param>
        /// <param name="coord">The coordinates of the second <see cref="Node"/>.</param>
        /// <param name="weight">The value that the <see cref="Edge"/>'s are weighted with.<code nl="\n"/>
        /// A higher number means it's less likely to be travled through.</param>
        /// <returns>True if the <see cref="Graph"/> contained a <see cref="Node"/> with the given coordinates.</returns>
        public bool TryAddEdge(float length, Node fromNode, (int, int) coord, float weight = 1)
        {
            if (Nodes.TryGetValue(coord, out Node toNode))
            {
                AddEdge(length, fromNode, toNode, weight);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Tries to add an <see cref="Edge"/> to and from both <see cref="Node"/>'s.
        /// </summary>
        /// <param name="fromNode">The first <see cref="Node"/>.</param>
        /// <param name="coord">The coordinates of the second <see cref="Node"/>.</param>
        /// <param name="weight">The value that the <see cref="Edge"/>'s are weighted with.<code nl="\n"/>
        /// A higher number means it's less likely to be travled through.</param>
        /// <returns>True if the <see cref="Graph"/> contained a <see cref="Node"/> with the given coordinates.</returns>
        public bool TryAddEdge(Node fromNode, (int, int) coord, float weight = 1)
        {
            if (Nodes.TryGetValue(coord, out Node toNode))
            {
                AddEdge(fromNode, toNode, weight);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds an <see cref="Edge"/> from the <paramref name="fromNode"/> to the <paramref name="toNode"/>.
        /// </summary>
        /// <param name="length">The distance between the two <see cref="Node"/>'s.</param>
        /// <param name="fromNode">The origin of the <see cref="Edge"/>.</param>
        /// <param name="toNode">The target of the <see cref="Edge"/>.</param>
        /// <param name="weight">The value that the <see cref="Edge"/> is weighted with.<code nl="\n"/>
        /// A higher number means it's less likely to be travled through.</param>
        public void AddDirectionalEdge(float length, Node fromNode, Node toNode, float weight = 1)
        {
            new Edge(fromNode, toNode, length, weight);
        }
        /// <summary>
        /// Adds an <see cref="Edge"/> from the <paramref name="fromNode"/> to the <paramref name="toNode"/>.
        /// </summary>
        /// <param name="fromNode">The origin of the <see cref="Edge"/>.</param>
        /// <param name="toNode">The target of the <see cref="Edge"/>.</param>
        /// <param name="weight">The value that the <see cref="Edge"/> is weighted with.<code nl="\n"/>
        /// A higher number means it's less likely to be travled through.</param>
        public void AddDirectionalEdge(Node fromNode, Node toNode, float weight = 1)
        {
            new Edge(fromNode, toNode, weight);
        }

        /// <summary>
        /// Finds a path from <paramref name="startCoord"/> to <paramref name="endCoord"/> using the AStar algorithm.
        /// </summary>
        /// <param name="startCoord">The coordinates of the starting <see cref="Node"/>, if present in the <see cref="Graph"/>.</param>
        /// <param name="endCoord">The coordinates of the ending <see cref="Node"/>, if present in the <see cref="Graph"/>.</param>
        /// <param name="successful">Is true if the task succeeded.</param>
        /// <returns>A <see cref="LinkedList{Node}"/> containing the <see cref="Node"/>'s in the order they are travled through.</returns>
        public LinkedList<Node> AStar((int, int) startCoord, (int, int) endCoord, out bool successful)
        {
            if (Nodes.TryGetValue(startCoord, out Node startNode) & Nodes.TryGetValue(endCoord, out Node endNode))
            {
                return AStar(startNode, endNode, out successful);
            }
            successful = false;
            return default;
        }

        /// <summary>
        /// Finds a path from <paramref name="startNode"/> to <paramref name="endNode"/> using the AStar algorithm.
        /// </summary>
        /// <param name="startNode">The starting <see cref="Node"/>.</param>
        /// <param name="endNode">The ending <see cref="Node"/>.</param>
        /// <param name="successful">Is true if the task succeeded.</param>
        /// <returns>A <see cref="LinkedList{Node}"/> containing the <see cref="Node"/>'s in the order they are travled through.</returns>
        public static LinkedList<Node> AStar(Node startNode, Node endNode, out bool successful)
        {
            //starting shit
            //        child, parent
            Dictionary<Node, Node> closedList = new Dictionary<Node, Node>() { { startNode, startNode } };
            Dictionary<Node, (Node parent, float currentDistance)> openList = new Dictionary<Node, (Node, float)>() { { startNode, (startNode, 0) } };
            Node currentNode = startNode;
            successful = true;

            while (currentNode != endNode)// if current node is the final node.
            {
                // for each edge
                foreach (Edge edge in currentNode.Edges)
                {
                    Node searchedNode = edge.ToNode;

                    //if not already selected path
                    if (!closedList.ContainsKey(searchedNode))
                    {
                        float alternativDistance = openList[currentNode].currentDistance + edge.WeightedLength;

                        //if searched node isn't already searched
                        if (!openList.ContainsKey(searchedNode))
                        {// add node
                            //          child            parent
                            openList.Add(searchedNode, (currentNode, alternativDistance));
                        }
                        //if searched node is pointing to a longer path
                        else if (openList[searchedNode].currentDistance > alternativDistance)
                        {// change parent
                            //          child            parent
                            openList[searchedNode] = (currentNode, alternativDistance);
                        }
                    }
                }

                // breaks if no path could be found
                if (openList.Count == 1)
                {
                    successful = false;
                    break;
                }

                // removes current node, so it's not chosen as next node
                openList.Remove(currentNode);


                //find out which one is closer to the goal (add to closed list)
                Node nextNode = default;
                float nextNodeDistance = float.MaxValue;
                foreach (KeyValuePair<Node, (Node parent, float distance)> pair in openList)
                {
                    float F = pair.Value.distance + pair.Key.DistanceTo(endNode);
                    if (F < nextNodeDistance)
                    {
                        nextNodeDistance = F;
                        nextNode = pair.Key;
                    }
                }


                closedList.Add(nextNode, openList[nextNode].parent);
                currentNode = nextNode;
            }


            LinkedList<Node> finalPath = new LinkedList<Node>();
            while (currentNode != startNode)
            {
                finalPath.AddFirst(currentNode);
                currentNode = closedList[currentNode];
            }
            finalPath.AddFirst(startNode);

            return finalPath;
        }

        /// <summary>
        /// Finds a path from <paramref name="startCoord"/> to a <see cref="Node"/> that is a given <paramref name="distance"/> or farther away from the <paramref name="escapeCoord"/> using the AStar algorithm.
        /// </summary>
        /// <param name="startCoord">The coordinates of the starting <see cref="Node"/>.</param>
        /// <param name="escapeCoord">The coordinates of the escaped <see cref="Node"/>.</param>
        /// <param name="distance">The target distance from the <paramref name="escapeCoord"/>.</param>
        /// <param name="successful">Is true if the task succeeded.</param>
        /// <returns>A <see cref="LinkedList{Node}"/> containing the <see cref="Node"/>'s in the order they are travled through.</returns>
        public LinkedList<Node> AwayStar((int, int) startCoord, (int, int) escapeCoord, float distance, out bool successful)
        {
            if (Nodes.TryGetValue(startCoord, out Node startNode) & Nodes.TryGetValue(escapeCoord, out Node escapeNode))
            {
                return AwayStar(startNode, escapeNode, distance, out successful);
            }
            successful = false;
            return default;
        }

        /// <summary>
        /// Finds a path from <paramref name="startNode"/> to a <see cref="Node"/> that is a given <paramref name="distance"/> or farther away from the <paramref name="escapeNode"/> using the AStar algorithm.
        /// </summary>
        /// <param name="startNode">The starting <see cref="Node"/>.</param>
        /// <param name="escapeNode">The escaped <see cref="Node"/>.</param>
        /// <param name="distance">The target distance from the <paramref name="escapeNode"/>.</param>
        /// <param name="successful">Is true if the task succeeded.</param>
        /// <returns>A <see cref="LinkedList{Node}"/> containing the <see cref="Node"/>'s in the order they are travled through.</returns>
        public static LinkedList<Node> AwayStar(Node startNode, Node escapeNode, float distance, out bool successful)
        {
            //starting shit
            //        child, parent
            Dictionary<Node, Node> closedList = new Dictionary<Node, Node>() { { startNode, startNode } };
            Dictionary<Node, (Node parent, float currentDistance)> openList = new Dictionary<Node, (Node, float)>() { { startNode, (startNode, 0) } };
            Node currentNode = startNode;
            successful = true;

            while (currentNode.DistanceTo(escapeNode) <= distance)// if current node is the final node.
            {
                // for each edge
                foreach (Edge edge in currentNode.Edges)
                {
                    Node searchedNode = edge.ToNode;

                    //if not already selected path
                    if (!closedList.ContainsKey(searchedNode))
                    {
                        float alternativDistance = openList[currentNode].currentDistance + edge.WeightedLength;

                        //if searched node isn't already searched
                        if (!openList.ContainsKey(searchedNode))
                        {// add node
                            //          child            parent
                            openList.Add(searchedNode, (currentNode, alternativDistance));
                        }
                        //if searched node is pointing to a longer path
                        else if (openList[searchedNode].currentDistance > alternativDistance)
                        {// change parent
                            //          child            parent
                            openList[searchedNode] = (currentNode, alternativDistance);
                        }
                    }
                }

                // breaks if no path could be found
                if (openList.Count == 1)
                {
                    successful = false;
                    break;
                }

                // removes current node, so it's not chosen as next node
                openList.Remove(currentNode);


                //find out which one is closer to the goal (add to closed list)
                Node nextNode = default;
                float nextNodeDistance = float.MaxValue;
                foreach (KeyValuePair<Node, (Node parent, float distance)> pair in openList)
                {
                    float F = pair.Value.distance + (distance - pair.Key.DistanceTo(escapeNode));
                    if (F < nextNodeDistance)
                    {
                        nextNodeDistance = F;
                        nextNode = pair.Key;
                    }
                }


                closedList.Add(nextNode, openList[nextNode].parent);
                currentNode = nextNode;
            }


            LinkedList<Node> finalPath = new LinkedList<Node>();
            while (currentNode != startNode)
            {
                finalPath.AddFirst(currentNode);
                currentNode = closedList[currentNode];
            }
            finalPath.AddFirst(startNode);

            return finalPath;
        }

        /// <summary>
        /// Finds the <see cref="Node"/> that is closest to <paramref name="coord"/>.
        /// </summary>
        /// <param name="coord">The point to which the closest <see cref="Node"/> is found.</param>
        /// <param name="successful">Is true if a <see cref="Node"/> was found.</param>
        /// <returns>The <see cref="Node"/> that is the closest to <paramref name="coord"/>. Returns <see cref="null"/> if no <see cref="Node"/> was found.</returns>
        public Node FindNearestNode((int, int) coord, out bool successful)
        {
            return FindNearestNode(coord, float.MaxValue, out successful);
        }

        /// <summary>
        /// Finds the <see cref="Node"/> that is closest to <paramref name="coord"/>, within the <paramref name="maximumDistance"/>.
        /// </summary>
        /// <param name="coord">The point to which the closest <see cref="Node"/> is found.</param>
        /// <param name="maximumDistance">The maximum distance from <paramref name="coord"/> a <see cref="Node"/> can be, before it is excluded from the search.</param>
        /// <param name="successful">Is true if a fitting <see cref="Node"/> was found.</param>
        /// <returns>The <see cref="Node"/> that is the closest to <paramref name="coord"/>. Returns <see cref="null"/> if no fitting <see cref="Node"/> was found.</returns>
        public Node FindNearestNode((int, int) coord, float maximumDistance, out bool successful)
        {
            successful = false;
            Node closestNode = null;
            float shortestDistance = maximumDistance;
            float currentDistance;
            foreach (KeyValuePair<(int x, int y), Node> node in Nodes)
            {
                if (shortestDistance > (currentDistance = node.Value.DistanceTo(coord)))
                {
                    shortestDistance = currentDistance;
                    closestNode = node.Value;
                    successful = true;
                }
            }
            return closestNode;
        }

        /// <summary>
        /// Finds the <paramref name="maxAmount"/> number of <see cref="Node"/>s that are closest to <paramref name="coord"/>.
        /// </summary>
        /// <param name="coord">The point to which the closest <see cref="Node"/>s are found.</param>
        /// <param name="maxAmount">The size of the list of closest <see cref="Node"/>s.</param>
        /// <returns>A <see cref="List{}"/> of the <see cref="Node"/>s that are closest to <paramref name="coord"/>.</returns>
        public List<Node> FindNearestNodes((int, int) coord, int maxAmount)
        {
            return FindNearestNodes(coord, maxAmount, float.MaxValue);
        }

        /// <summary>
        /// Finds the <paramref name="maxAmount"/> number of <see cref="Node"/>s that are closest to <paramref name="coord"/>, within the <paramref name="maximumDistance"/>.
        /// </summary>
        /// <param name="coord">The point to which the closest <see cref="Node"/>s are found.</param>
        /// <param name="maxAmount">The size of the list of closest <see cref="Node"/>s.</param>
        /// <param name="maximumDistance">The maximum distance from <paramref name="coord"/> a <see cref="Node"/> can be, before it is excluded from the search.</param>
        /// <returns>A <see cref="List{}"/> of the <see cref="Node"/>s that are closest to <paramref name="coord"/>.</returns>
        public List<Node> FindNearestNodes((int, int) coord, int maxAmount, float maximumDistance)
        {
            List<Node> closestNodes = new List<Node>(maxAmount);
            float borderDistance = maximumDistance;


            float currentDistance;
            foreach (KeyValuePair<(int x, int y), Node> node in Nodes)
            {
                if (borderDistance > (currentDistance = node.Value.DistanceTo(coord)))
                {
                    closestNodes.Add(node.Value);
                    if (closestNodes.Count >= maxAmount)
                    {
                        borderDistance = currentDistance;
                    }
                }
            }
            return closestNodes;
        }

    }

}
