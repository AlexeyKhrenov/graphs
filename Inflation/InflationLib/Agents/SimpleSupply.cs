using InflationLib.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace InflationLib.Agents
{
    public class SimpleSupply : IEdge
    {
        public SimpleSupply(int price, IAgent from, IAgent to)
        {
            From = from;
            To = to;
            Price = price;
        }

        public IAgent From { get; set; }

        public IAgent To { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
