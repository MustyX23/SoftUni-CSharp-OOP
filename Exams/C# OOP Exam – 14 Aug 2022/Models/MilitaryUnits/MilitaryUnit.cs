using PlanetWars.Models.MilitaryUnits.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    public abstract class MilitaryUnit : IMilitaryUnit
    {

        private double cost;
        private int enduranceLevel;

        protected MilitaryUnit(double cost)
        {
            Cost = cost;
            EnduranceLevel = 1;
        }

        public double Cost 
        {
            get
            {
                return cost;
            }  
            private set { cost = value; }
        }

        public int EnduranceLevel
        {
            get => enduranceLevel;
            private set
            {
                enduranceLevel = value;
            }
        }

        public void IncreaseEndurance()
        {
            EnduranceLevel += 1;
            if (EnduranceLevel > 20)
            {
                EnduranceLevel = 20;
                throw new ArgumentException("Endurance level cannot exceed 20 power points.");
            }
        }
    }
}
