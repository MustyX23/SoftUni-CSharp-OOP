using System;

namespace Telephony
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //0882134215 0882134333 0899213421 0558123 3333123
            string[] phoneNumbers = Console.ReadLine().Split(' ');
            //http://softuni.bg http://youtube.com http://www.g00gle.com
            string[]urls = Console.ReadLine().Split(' ');

            foreach (string phoneNumber in phoneNumbers)
            {
                ICall smartPhone = new Smartphone();
                smartPhone.Call(phoneNumber);
                ICall stationaryPhone = new StationaryPhone();
                stationaryPhone.Call(phoneNumber);
            }

            foreach (string url in urls)
            {
                IBrowse smartPhone = new Smartphone();
                smartPhone.Browse(url);

            }


        }
    }
}
