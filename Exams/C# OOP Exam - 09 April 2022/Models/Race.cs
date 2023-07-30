using Formula1.Models.Contracts;
using Formula1.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private List<IPilot> pilots;

        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            pilots = new List<IPilot>();
            TookPlace = false;
        }

        public string RaceName 
        {
            get => raceName;
            private set 
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidRaceName, value));
                }
                raceName = value;
            }
        }
        public int NumberOfLaps 
        {
            get => numberOfLaps;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidLapNumbers, value));
                }
                numberOfLaps = value;
            }
        }
        public ICollection<IPilot> Pilots => pilots;
        public bool TookPlace { get; set ; }

        public void AddPilot(IPilot pilot)
        {
            pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"The {RaceName} race has:");
            result.AppendLine($"Participants: {pilots.Count}");
            result.AppendLine($"Number of laps: {NumberOfLaps}");
            if (TookPlace == true)
            {
                result.AppendLine($"Took place: Yes");
            }
            else
            {
                result.AppendLine("Took place: No");
            }
            //result.AppendLine($"Took place: { Yes/No }");

            return result.ToString().TrimEnd();
        }
    }
}
