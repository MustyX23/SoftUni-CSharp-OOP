using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {

        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            Cost = cost;
            EnduranceLevel = 1;
        }

        public double Cost { get; private set;  }

        public int EnduranceLevel
        {
            get => enduranceLevel;
            private set
            {
                if (value > 20)
                {
                    enduranceLevel = 20;

                    throw new ArgumentException(ExceptionMessages.EnduranceLevelExceeded);
                }
                enduranceLevel = value;
            }
        }

        public void IncreaseEndurance()
        {
            EnduranceLevel++;
        }
    }
}
