using InflationLib.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InflationLib.Agents
{
    public class SimpleAgent : IAgent
    {
        private List<SimpleSupply> suppliers;
        private List<SimpleSupply> consumers;

        public SimpleAgent(int startingPrice, int startingProduction, Product product)
        {
            Product = product;
            Price = startingPrice;
            ProductionQuantity = startingProduction;

            Id = Guid.NewGuid();
            suppliers = new List<SimpleSupply>();
            consumers = new List<SimpleSupply>();
        }

        public Product Product { get; }

        public int ProductionQuantity { get; }

        public int Price { get; }

        public int ProductRequired => (int)(ProductionQuantity / Product.Coeff);

        public Guid Id { get; }

        public IReadOnlyCollection<IAgent> Children => consumers.Select(x => x.To).ToList();

        public void Act()
        {
        }

        public IEdge AddChild(IAgent child)
        {
            var consumer = (SimpleAgent)child;

            var edge = new SimpleSupply(Price, this, consumer);

            this.consumers.Add(edge);
            consumer.suppliers.Add(edge);

            return edge;
        }

        public bool CanConsumeFrom(IAgent from)
        {
            var supplier = (SimpleAgent)from;
            return Product.Precursor == supplier.Product;
        }
    }
}
