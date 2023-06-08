using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaCalories
{
    public class Dough
    {
        private const double BaseDoughCaloriesPerGram = 2;
        private readonly Dictionary<string, double> flourTypesCalories;
        private readonly Dictionary<string, double> bakingTechniquesCalories;

        private string flourType;
        private string bakingTechnique;
        private double weight;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            flourTypesCalories
                = new Dictionary<string, double> { { "white", 1.5 }, { "wholegrain", 1.0 } };
            bakingTechniquesCalories
                = new Dictionary<string, double> { { "crispy", 0.9 }, { "chewy", 1.1 }, {"homemade", 1.0 } };

            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public string FlourType
        {
            get
            {
                return flourType;
            }
            private set
            {
                if (!flourTypesCalories.ContainsKey(value))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value.ToLower();
            }
        }
        public string BakingTechnique 
        {
            get 
            { 
                return bakingTechnique; 
            }
            private set
            {
                if (!bakingTechniquesCalories.ContainsKey(value))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value.ToLower();
            }
        }
        public double Weight
        {
            get
            {
                return weight;
            }
            private set
            {
                //"Dough weight should be in the range [1..200]."
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        } 
        public double Calories 
        {
            get
            {
                double flourTypeModifier = bakingTechniquesCalories[BakingTechnique];
                double techniqueModifier = flourTypesCalories[FlourType];

                return BaseDoughCaloriesPerGram * weight * flourTypeModifier * techniqueModifier;    
            }
        }
    }
}
