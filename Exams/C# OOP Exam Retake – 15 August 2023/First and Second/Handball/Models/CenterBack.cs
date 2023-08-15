using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class CenterBack : Player
    {
        public CenterBack(string name) 
            : base(name, 4)
        {
        }

        public override void DecreaseRating()
        {
            if (Rating - 1 >= 1)
            {
                Rating -= 1;
            }
            else
            {
                Rating = 1;
            }
        }

        public override void IncreaseRating()
        {
            if (Rating + 1 <= 10)
            {
                Rating += 1;
            }
            else
            {
                Rating = 10;
            }
        }
    }
}
