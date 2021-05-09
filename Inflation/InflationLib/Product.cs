using System;
using System.Collections.Generic;
using System.Text;

namespace InflationLib
{
    public class Product
    {
        public static Product Oil;
        public static Product Chemicals;
        public static Product Fertilizer;
        public static Product Fruits;
        public static Product People;
        
        static Product()
        {
            Oil = new Product(nameof(Oil));
            Chemicals = new Product(nameof(Chemicals));
            Fertilizer = new Product(nameof(Fertilizer));
            Fruits = new Product(nameof(Fruits));
            People = new Product(nameof(People));

            Oil.Precursor = People;
            Chemicals.Precursor = Oil;
            Fertilizer.Precursor = Chemicals;
            Fruits.Precursor = Fertilizer;
            People.Precursor = Fruits;
        }

        private Product(string name)
        {
            Coeff = 1;
            Name = name;
        }

        public Product Precursor { get; private set; }

        public double Coeff { get; }

        public string Name { get; }
    }
}
