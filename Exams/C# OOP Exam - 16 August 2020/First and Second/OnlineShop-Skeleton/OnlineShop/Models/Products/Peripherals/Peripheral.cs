using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Models.Products.Peripherals
{
    public abstract class Peripheral : Product, IPeripheral
    {
        protected Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            ConnectionType = connectionType;
        }

        public string ConnectionType { get; private set; }

        public override string ToString()
        {
            string productType = this.GetType().Name;
            return $" Overall Performance: {OverallPerformance:F2}. Price: {Price:F2} - {productType}: {Manufacturer} {Model} (Id: {Id}) Connection Type: {ConnectionType}";
        }

    }
}
