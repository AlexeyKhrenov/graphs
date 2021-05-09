using InflationLib.Agents;
using System;
using System.Collections.Generic;
using System.Text;

namespace InflationLib.Graph
{
    public interface IEdge
    {
        IAgent From { get; }

        IAgent To { get; }
    }
}
