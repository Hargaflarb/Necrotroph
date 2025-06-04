using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necrotroph_Eksamensprojekt.PathFinding
{
    /// <summary>
    /// Represents a connection between two <see cref="Node"/>'s.
    /// </summary>
    internal sealed class Edge
    {
        private Node fromNode;
        private Node toNode;
        private float length;
        private float weight;

        internal Node FromNode { get => fromNode; private set => fromNode = value; }
        internal Node ToNode { get => toNode; private set => toNode = value; }
        /// <summary>
        /// Represents the distance between its <see cref="ToNode"/> and <see cref="FromNode"/>.
        /// </summary>
        public float Length { get => length; private set => length = value; }
        /// <summary>
        /// A value that adjusts how the <see cref="Length"/> is valued.<code nl="\n"/>
        /// The higher or lower the value the less or more likely it is to be pathed through. 1 is standard.
        /// </summary>
        public float Weight { get => weight; private set => weight = value; }
        /// <summary>
        /// Represents the <see cref="Length"/> after being weighted.
        /// </summary>
        public float WeightedLength { get => length * weight; }

        internal Edge(Node fromNode, Node toNode, float length, float weight)
        {
            FromNode = fromNode;
            FromNode.AddEdge(this);
            ToNode = toNode;
            Length = length;
            Weight = weight;
        }
        internal Edge(Node fromNode, Node toNode, float weight)
        {
            FromNode = fromNode;
            FromNode.AddEdge(this);
            ToNode = toNode;
            Length = FromNode.DistanceTo(ToNode);
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{FromNode} -> {ToNode} | l: {Length}, w: {weight}";
        }
    }
}
