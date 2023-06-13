using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Dough
    {
        private int weight;
        private string type;
        private string bakingTechnique;
        private double totalKCals;

        public int Weight
        {
            get { return this.weight; }
            set
            {
                if (value >= 1 && value <= 200)
                {
                    this.weight = value;
                }
                else
                {
                    throw new InvalidOperationException("Dough weight should be in the range [1..200].");
                }
            }
        }

        public string Type
        {
            get { return this.type; }
            set
            {
                if (value.ToLower() == "white" || value.ToLower() == "wholegrain")
                {
                    this.type = value.ToLower();
                }
                else
                {
                    throw new InvalidOperationException("Invalid type of dough.");
                }
            }
        }

        public string BakingTechnique
        {
            get { return this.bakingTechnique; }
            set
            {
                if (value.ToLower() == "crispy" || value.ToLower() == "chewy" || value.ToLower() == "homemade")
                {
                    this.bakingTechnique = value.ToLower();
                }
                else
                {
                    throw new InvalidOperationException("Invalid type of dough.");
                }
            }
        }

        public Dough()
        {

        }

        public double CalculateKCals()
        {
            this.totalKCals = this.weight * 2;

            if (this.type == "white")
            {
                this.totalKCals *= 1.5;
            }

            if (this.bakingTechnique == "crispy")
            {
                this.totalKCals *= 0.9;
            }
            else if (this.bakingTechnique == "chewy")
            {
                this.totalKCals *= 1.1;
            }

            return this.totalKCals;
        }
    }
}
