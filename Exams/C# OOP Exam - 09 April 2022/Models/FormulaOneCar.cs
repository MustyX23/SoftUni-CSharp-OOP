using Formula1.Core;
using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Formula1.Models
{
    public abstract class FormulaOneCar : IFormulaOneCar
    {
        private string model;
        private int horsePower;
        private double engineDisplacement;

        protected FormulaOneCar(string model, int horsepower, double engineDisplacement)
        {
            Model = model;
            Horsepower = horsepower;
            EngineDisplacement = engineDisplacement;
        }

        public string Model 
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1CarModel, Model));
                }
                model = value;
            }
        }
        public int Horsepower 
        {
            get => horsePower;
            private set
            {
                if (value < 900 || value > 1050)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1HorsePower, Horsepower));
                }
                horsePower = value;
            }
        }
        public double EngineDisplacement 
        {
            get => engineDisplacement;
            private set
            {
                if (value < 1.6 || value > 2.00)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidF1EngineDisplacement, EngineDisplacement));
                }
                engineDisplacement = value;
            }
        }

        public double RaceScoreCalculator(int laps)
        {
            return EngineDisplacement / Horsepower * laps;
        }
    }
}
