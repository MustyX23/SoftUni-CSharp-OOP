using System;
using System.Collections.Generic;
using System.Text;

namespace RobotService.Models
{
    public class DomesticAssistant : Robot
    {
        public DomesticAssistant(string model)
            : base(model, 20000, 2000)
        {
        }
    }
}
