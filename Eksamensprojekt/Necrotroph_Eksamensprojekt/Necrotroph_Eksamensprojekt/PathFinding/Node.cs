using Necrotroph_Eksamensprojekt.GameObjects;
using System;
using System.Collections.Generic;

namespace Necrotroph_Eksamensprojekt.PathFinding
{
    /// <summary>
    /// Represents a traversable point.
    /// </summary>
    public class Node
    {
        private int x;
        private int y;
        private List<Edge> edges;

        /// <summary>
        /// The x-coordinate of the <see cref="Node"/>.
        /// </summary>
        public virtual int X { get => x; private set => x = value; }
        /// <summary>
        /// The y-coordinate of the <see cref="Node"/>.
        /// </summary>
        public virtual int Y { get => y; private set => y = value; }
        internal List<Edge> Edges { get => edges; private set => edges = value; }

        /// <summary>
        /// Constructs a new <see cref="Node"/>.
        /// </summary>
        /// <param name="x">The x-coordinate of the <see cref="Node"/>.</param>
        /// <param name="y">The y-coordinate of the <see cref="Node"/>.</param>
        public Node(int x, int y)
        {
            this.X = x;
            this.Y = y;
            edges = new List<Edge>(8);
        }

        protected Node()
        {
            edges = new List<Edge>(8);
        }


        /// <summary>
        /// Adds an <see cref="Edge"/> to this <see cref="Node"/>.
        /// </summary>
        /// <param name="edge">The <see cref="Edge"/> to be added.</param>
        internal void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        /// <summary>
        /// Removes an <see cref="Edge"/> from this <see cref="Node"/>.
        /// </summary>
        /// <param name="edge">The <see cref="Edge"/> to remove</param>
        /// <returns><see cref="true"/> if <paramref name="edge"/> was successfully removed.</returns>
        internal bool RemoveEdge(Edge edge)
        {
            return Edges.Remove(edge);
        }

        /// <summary>
        /// Calculates the distance between this and another <see cref="Node"/>.
        /// </summary>
        /// <param name="other">The other <see cref="Node"/> to measured between.</param>
        /// <returns>A <see cref="float"/>, representing the distance between the two <see cref="Node"/>'s.</returns>
        internal float DistanceTo(Node other)
        {
            return (float)Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }

        /// <summary>
        /// Calculates the distance between this <see cref="Node"/> and a point.
        /// </summary>
        /// <param name="coord">The point to measured between.</param>
        /// <returns>A <see cref="float"/>, representing the distance between the <see cref="Node"/> and the point.</returns>
        internal float DistanceTo((int x, int y) coord)
        {
            return (float)Math.Sqrt(Math.Pow(X - coord.x, 2) + Math.Pow(Y - coord.y, 2));
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

    }

    public class MovableNode : Node
    {
        private Transform transform;
        private Graph graph;

        public MovableNode(Graph graph, GameObject gameObject)
        {
            transform = gameObject.Transform;
            this.graph = graph;
        }
        public override int X => (int)transform.WorldPosition.X;
        public override int Y => (int)transform.WorldPosition.Y;


        public void UpdateEdges()
        {
            
        }
    }
}
