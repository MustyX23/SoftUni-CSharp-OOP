using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products.Components
{
    public abstract class Component : Product, IComponent
    {
        protected Component(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            Generation = generation;
        }

        public int Generation { get; private set; }

        public override string ToString()
        {
            string productType = this.GetType().Name;
            return $" Overall Performance: {OverallPerformance:F2}. Price: {Price:F2} - {productType}: {Manufacturer} {Model} (Id: {Id}) Generation: {Generation}";
        }
    }
}
