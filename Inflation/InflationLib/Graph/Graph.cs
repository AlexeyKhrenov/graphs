using InflationLib.Agents;
using InflationLib.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InflationLib.Graph
{
    public class Graph
    {
        public Graph(IReadOnlyCollection<IAgent> nodes)
        {
            nodes = new List<IAgent>();
            edges = new List<IEdge>();

            this.nodes.AddRange(nodes);
        }

        private List<IAgent> nodes;
        private List<IEdge> edges;

        public IReadOnlyCollection<IEdge> Edges { get; }

        public IReadOnlyCollection<IAgent> Nodes { get; }

        public IAgent Root => Nodes.FirstOrDefault();

        public void GenerateEdges(IMath probability)
        {
            for (var i = 0; i < nodes.Count - 2; i++)
            {
                for (var j = 1; i < nodes.Count - 1; i++)
                {
                    var from = nodes[i];
                    var to = nodes[j];

                    DrawNode(from, to, probability);
                    DrawNode(to, from, probability);
                }
            }
        }

        public void ActBFS()
        {
            if (!Nodes.Any())
            {
                return;
            }

            var queue = new Queue<IAgent>();

            queue.Enqueue(Root);

            while (queue.TryDequeue(out var next))
            {
                next.Act();
                foreach (var child in next.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private void DrawNode(IAgent from, IAgent to, IMath probability)
        {
            if (to.CanConsumeFrom(from) && probability.TryLuck())
            {
                var newEdge = to.AddChild(from);
                edges.Add(newEdge);
            }
        }
    }
}
