using InflationLib.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InflationLib.Agents
{
    public class SimpleAgent : IAgent
    {
        private double priceStep;
        private List<SimpleSupply> supplies;
        private List<SimpleSupply> consumers;
        private bool? raisedPriceInPreviousRound;
        private double? previousRoundIncome;

        public SimpleAgent(
            double startingPrice,
            double priceStep,
            int maxProduction,
            Product product)
        {
            Product = product;
            Price = startingPrice;
            MaxProduction = maxProduction;
            this.priceStep = priceStep;

            Id = Guid.NewGuid();
            supplies = new List<SimpleSupply>();
            consumers = new List<SimpleSupply>();
            raisedPriceInPreviousRound = null;
        }

        public Product Product { get; }

        public int MaxProduction { get; }

        public int Stock { get; private set; }

        public double Price { get; private set; }

        public Guid Id { get; }

        public IReadOnlyCollection<IAgent> Children => consumers.Select(x => x.To).ToList();

        public void Act()
        {
            var income = (MaxProduction - Stock) * Price;

            if (raisedPriceInPreviousRound == null)
            {
                RaisePrice();
            }

            if (income > previousRoundIncome ^ raisedPriceInPreviousRound.Value == true)
            {
                ReducePrice();
            }
            else
            {
                RaisePrice();
            }

            Reset();
            Buy();

            previousRoundIncome = income;
        }

        public IEdge AddChild(IAgent child)
        {
            var consumer = (SimpleAgent)child;

            var edge = new SimpleSupply(Price, this, consumer);

            this.consumers.Add(edge);
            consumer.supplies.Add(edge);

            return edge;
        }

        public bool CanConsumeFrom(IAgent from)
        {
            var supplier = (SimpleAgent)from;
            return Product.Precursor == supplier.Product;
        }

        private void Ship(int quantity, SimpleSupply supply)
        {
            if (Stock == 0)
            {
                throw new ArgumentException();
            }

            Stock -= quantity;
            supply.Ship(quantity, Price);
        }

        private void Reset()
        {
            foreach (var supply in supplies)
            {
                supply.Reset();
            }

            Stock = 0;
        }

        private void RaisePrice()
        {
            Price += priceStep;
        }

        private void ReducePrice()
        {
            Price = Price - priceStep;
            if (Price < 0)
            {
                Price = 0;
            }
        }

        private void Buy()
        {
            double avgPrice = 0;

            foreach (var supply in supplies.OrderBy(x => x.Price))
            {
                var supplier = (SimpleAgent)supply.From;

                while (Stock < MaxProduction)
                {
                    if (supplier.Stock == 0)
                    {
                        continue;
                    }

                    var nextPrice = (Stock * avgPrice + supplier.Price) / ((double)Stock + 1);
                    if (nextPrice <= Price)
                    {
                        avgPrice = nextPrice;
                        supplier.Ship(1, supply);
                        Stock++;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }
}
