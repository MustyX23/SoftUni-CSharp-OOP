using OnlineShop.Common.Constants;
using OnlineShop.Common.Enums;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Models.Products.Computers
{
    public abstract class Computer : Product, IComputer
    {
        private List<IComponent> components;
        private List<IPeripheral> peripherals;

        public override double OverallPerformance
        {
            get
            {
                if (components.Count == 0)
                    return base.OverallPerformance;

                double componentsAveragePerformance = components.Average(c => c.OverallPerformance);
                return base.OverallPerformance + componentsAveragePerformance;
            }
        }

        public override decimal Price => base.Price + components.Sum(c => c.Price) + peripherals.Sum(p => p.Price);
        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            components = new List<IComponent>();
            peripherals = new List<IPeripheral>();
        }

        public override string ToString()
        {
            string productType = this.GetType().Name;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Overall Performance: {OverallPerformance:F2}. Price: {Price:F2} - {productType}: {Manufacturer} {Model} (Id: {Id})");

            if (components.Count > 0)
            {
                sb.AppendLine(" Components (" + components.Count + "):");
                foreach (var component in components)
                {
                    sb.AppendLine(" " + component.ToString());
                }
            }

            if (peripherals.Count > 0)
            {
                sb.AppendLine($" Peripherals ({peripherals.Count}); Average Overall Performance ({peripherals.Average(p => p.OverallPerformance):F2}):");
                foreach (var peripheral in peripherals)
                {
                    sb.AppendLine(" " + peripheral.ToString());
                }
            }
            else
            {
                sb.AppendLine(" Peripherals (0); Average Overall Performance (0.00):");
            }

            return sb.ToString().TrimEnd();
        }

        public IReadOnlyCollection<IComponent> Components => components.AsReadOnly();

        public IReadOnlyCollection<IPeripheral> Peripherals => peripherals.AsReadOnly();

        public void AddComponent(IComponent component)
        {
            if (components.Any(c => c.GetType() == component.GetType()))
            {
                string componentType = component.GetType().Name;
                string computerType = this.GetType().Name;

                throw new ArgumentException(String.Format(ExceptionMessages.NotExistingComponent, componentType, computerType, Id));
            }

            components.Add(component);
        }
        public void AddPeripheral(IPeripheral peripheral)
        {
            if (peripherals.Any(p => p.GetType() == peripheral.GetType()))
            {
                string peripheralType = peripheral.GetType().Name;
                string computerType = this.GetType().Name;

                throw new ArgumentException(String.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, computerType, Id));
            }

            peripherals.Add(peripheral);
        }
        public IComponent RemoveComponent(string componentType)
        {
            var component = components.FirstOrDefault(c => c.GetType().Name == componentType);
            if (component == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.NotExistingComponent, componentType, GetType().Name, Id));
            }

            components.Remove(component);
            return component;
        }
        public IPeripheral RemovePeripheral(string peripheralType)
        {
            var peripheral = peripherals.FirstOrDefault(p => p.GetType().Name == peripheralType);
            if (peripheral == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.NotExistingPeripheral, peripheralType, GetType().Name, Id));
            }

            peripherals.Remove(peripheral);
            return peripheral;
        }
    }
}
