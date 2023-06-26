using System;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[] carInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            double carfuelQuantity = double.Parse(carInfo[1]);
            double carliters = double.Parse(carInfo[2]);

            Vehicle car = new Car(carfuelQuantity, carliters);

            string[] truckInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            double truckfuelQuantity = double.Parse(truckInfo[1]);
            double truckliters = double.Parse(truckInfo[2]);

            Vehicle truck = new Truck(truckfuelQuantity, truckliters);

            int numberOfCommands = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfCommands; i++)
            {
                string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (input[0] == "Drive")
                {
                    double kilometers = Convert.ToDouble(input[2]);
                    if (input[1] == "Car")
                    {
                        car.Drive(kilometers);
                    }
                    else if (input[1] == "Truck")
                    {
                        truck.Drive(kilometers);
                    }
                }
                else if (input[0] == "Refuel")
                {
                    double fuel = double.Parse(input[2]);
                    if (input[1] == "Car")
                    {
                        car.Refuel(fuel);
                    }
                    else if (input[1] == "Truck")
                    {
                        truck.Refuel(fuel);
                    }
                }
            }
            Console.WriteLine(car);
            Console.WriteLine(truck);
        }
    }
}
