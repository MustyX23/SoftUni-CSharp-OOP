using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Robot : IIdentifiable
    {
        public Robot(string model, string iD)
        {
            Model = model;
            ID = iD;
        }

        public string Model { get; set; }
        public string ID { get; set; }
    }
}
