using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Athletes
{
    public class Boxer : Athlete
    {
        private const int InititalBoxerStamina = 60;
        public Boxer(string fullName, string motivation, int numberOfMedals)
            : base(fullName, motivation, numberOfMedals, InititalBoxerStamina)
        {
        }

        //Can train only in a BoxingGym.

        public override void Exercise()
        {
            this.Stamina += 15;
            if (this.Stamina > 100)
            {
                Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
