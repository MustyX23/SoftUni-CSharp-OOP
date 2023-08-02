using OnlineShop.Common.Constants;
using OnlineShop.Common.Enums;
using OnlineShop.Models.Products.Components;
using OnlineShop.Models.Products.Computers;
using OnlineShop.Models.Products.Peripherals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using IComponent = OnlineShop.Models.Products.Components.IComponent;

namespace OnlineShop.Core
{
    public class Controller : IController
    {
        private List<IComputer> computers;

        public Controller()
        {
            computers = new List<IComputer>();
        }
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            if (computerType != nameof(DesktopComputer) && computerType != nameof(Laptop))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComputerType);
            }
            IComputer computer = computers.FirstOrDefault(c => c.Id == id);

            if (computer != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComputerId);
            }

            if (computerType == nameof(DesktopComputer))
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else if (computerType == nameof(Laptop))
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            computers.Add(computer);
            return String.Format(SuccessMessages.AddedComputer, id);
        }
        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            IComputer computer = computers.FirstOrDefault(c => c.Id == computerId);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            if (componentType != nameof(CentralProcessingUnit) && componentType != nameof(Motherboard)
                && componentType != nameof(PowerSupply) && componentType != nameof(RandomAccessMemory)
                && componentType != nameof(SolidStateDrive) && componentType != nameof(VideoCard))
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }   
            
            IComponent component = computer.Components.FirstOrDefault(c => c.Id == id);

            if (component != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingComponentId);
            }
            if (componentType == nameof(CentralProcessingUnit))
            {
                component = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(Motherboard))
            {
                component = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(PowerSupply))
            {
                component = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(RandomAccessMemory))
            {
                component = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(SolidStateDrive))
            {
                component = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(VideoCard))
            {
                component = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
            }
            computer.AddComponent(component);
            return String.Format(SuccessMessages.AddedComponent, componentType, id, computerId);

        }
        public string RemoveComponent(string componentType, int computerId)
        {
            IComputer computer = computers.FirstOrDefault(c => c.Id == computerId);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IComponent component = computer.RemoveComponent(componentType);
            return String.Format(SuccessMessages.RemovedComponent, componentType, component.Id);
        }


        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            IComputer computer = computers.FirstOrDefault(c => c.Id == computerId);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }
            if (peripheralType != nameof(Headset) && peripheralType != nameof(Keyboard)
                && peripheralType != nameof(Monitor) && peripheralType != nameof(Mouse))
            {
                throw new ArgumentException(ExceptionMessages.InvalidPeripheralType);
            }
            IPeripheral peripheral = computer.Peripherals.FirstOrDefault(p => p.Id == id);
            if (peripheral != null)
            {
                throw new ArgumentException(ExceptionMessages.ExistingPeripheralId);
            }

            if (peripheralType == nameof(Headset))
            {
                peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Keyboard))
            {
                peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Monitor))
            {
                peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Mouse))
            {
                peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            computer.AddPeripheral(peripheral);
            return String.Format(SuccessMessages.AddedPeripheral, peripheralType, id, computerId);

        }
        public string RemovePeripheral(string peripheralType, int computerId)
        {
            IComputer computer = computers.FirstOrDefault(c => c.Id == computerId);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            IPeripheral peripheral = computer.RemovePeripheral(peripheralType);
            return String.Format(SuccessMessages.RemovedPeripheral, peripheralType, peripheral.Id);
        }

        public string BuyComputer(int id)
        {
            IComputer computer = computers.FirstOrDefault(c => c.Id == id);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            computers.Remove(computer);
            return computer.ToString();
        }


        public string BuyBest(decimal budget)
        {
            if (computers.Count == 0)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            IComputer bestComputer = computers.OrderByDescending(c => c.OverallPerformance).FirstOrDefault(c => c.Price <= budget);

            if (bestComputer == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.CanNotBuyComputer, budget));
            }

            computers.Remove(bestComputer);
            return bestComputer.ToString();
        }
        
        public string GetComputerData(int id)
        {
            IComputer computer = computers.FirstOrDefault(c => c.Id == id);
            if (computer == null)
            {
                throw new ArgumentException(ExceptionMessages.NotExistingComputerId);
            }

            return computer.ToString();
        }       
        
    }
}
