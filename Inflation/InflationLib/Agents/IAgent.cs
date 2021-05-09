using InflationLib.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace InflationLib.Agents
{
    public interface IAgent
    {
        Guid Id { get; }

        void Act();

        bool CanConsumeFrom(IAgent from);

        IEdge AddChild(IAgent child);

        IReadOnlyCollection<IAgent> Children { get; }
    }
}
