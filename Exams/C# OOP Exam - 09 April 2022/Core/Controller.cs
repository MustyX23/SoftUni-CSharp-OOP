using Formula1.Core.Contracts;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using Formula1.Repositories;
using Formula1.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Formula1.Utilities;
using System.Reflection;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private IRepository<IPilot> pilotRepository;
        private IRepository<IRace> raceRepository;
        private IRepository<IFormulaOneCar> carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = pilotRepository.FindByName(pilotName);
            IFormulaOneCar car = carRepository.FindByName(carModel);

            if (pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            if (car == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);
            carRepository.Remove(car);

            return String.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }
        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race = raceRepository.FindByName(raceName);
            IPilot pilot = pilotRepository.FindByName(pilotFullName);

            if (race == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            if (pilot == null || !pilot.CanRace || race.Pilots.Contains(pilot))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);

            return String.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }
        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (carRepository.FindByName(model) != null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            IFormulaOneCar car;
            switch (type)
            {
                case "Ferrari":
                    car = new Ferrari(model, horsepower, engineDisplacement);
                    break;
                case "Williams":
                    car = new Williams(model, horsepower, engineDisplacement);
                    break;
                default:
                    throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            carRepository.Add(car);
            return $"Car {type}, model {model} is created.";
        }
        public string CreatePilot(string fullName)
        {
            if (pilotRepository.FindByName(fullName) != null)
            {
                throw new InvalidOperationException($"Pilot {fullName} is already created.");
            }

            IPilot pilot = new Pilot(fullName);
            pilotRepository.Add(pilot);

            return $"Pilot {fullName} is created.";
        }
        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (raceRepository.FindByName(raceName) != null)
            {
                throw new InvalidOperationException($"Race {raceName} is already created.");
            }

            IRace race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);

            return $"Race {raceName} is created.";
        }
        public string PilotReport()
        {
            List<IPilot> sortedPilots = new List<IPilot>(pilotRepository.Models);
            sortedPilots.Sort((p1, p2) => p2.NumberOfWins.CompareTo(p1.NumberOfWins));

            StringBuilder report = new StringBuilder();
            foreach (IPilot pilot in sortedPilots)
            {
                report.AppendLine(pilot.ToString());
            }

            return report.ToString().TrimEnd();
        }
        public string RaceReport()
        {
            StringBuilder report = new StringBuilder();
            foreach (IRace race in raceRepository.Models)
            {
                if (race.TookPlace)
                {
                    report.AppendLine(race.RaceInfo());
                }
            }

            return report.ToString().TrimEnd();
        }
        public string StartRace(string raceName)
        {
            IRace race = raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException($"Race {raceName} does not exist.");
            }

            if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException($"Race {raceName} cannot start with less than three participants.");
            }

            if (race.TookPlace)
            {
                throw new InvalidOperationException($"Can not execute race {raceName}.");
            }

            List<IPilot> sortedPilots = new List<IPilot>(race.Pilots);
            sortedPilots.Sort((p1, p2) => p2.Car.RaceScoreCalculator(race.NumberOfLaps).CompareTo(p1.Car.RaceScoreCalculator(race.NumberOfLaps)));

            sortedPilots[0].WinRace();
            sortedPilots[0].Car.RaceScoreCalculator(race.NumberOfLaps);
            sortedPilots[1].Car.RaceScoreCalculator(race.NumberOfLaps);
            sortedPilots[2].Car.RaceScoreCalculator(race.NumberOfLaps);

            race.TookPlace = true;

            StringBuilder result = new StringBuilder();
            result.Append($"Pilot {sortedPilots[0].FullName} wins the {raceName} race.");
            result.Append($"Pilot {sortedPilots[1].FullName} is second in the {raceName} race.");
            result.Append($"Pilot {sortedPilots[2].FullName} is third in the {raceName} race.");

            return result.ToString();
        }

    }
}