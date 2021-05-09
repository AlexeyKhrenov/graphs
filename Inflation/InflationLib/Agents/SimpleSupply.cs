using InflationLib.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace InflationLib.Agents
{
    public class SimpleSupply : IEdge
    {
        public SimpleSupply(double price, IAgent from, IAgent to)
        {
            From = from;
            To = to;
            Price = price;
        }

        public IAgent From { get; set; }

        public IAgent To { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public void Reset()
        {
            Quantity = 0;
            Price = 0;
        }

        public void Ship(int quantity, double price)
        {
            Quantity += quantity;
            Price = price;
        }
    }
}
