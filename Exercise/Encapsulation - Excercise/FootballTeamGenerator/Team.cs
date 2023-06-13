using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballTeamGenerator
{
    public class Team
    {
        private List<Player> numberOfPlayers;
        private string name;

        public string Name 
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                name = value;
            }
        }
        public int Rating
        {
            get 
            {
                if (numberOfPlayers.Count == 0)
                {
                    return 0;
                }
                return (int)Math.Round(numberOfPlayers.Average(p => p.OverallRating()), 0);
            }
        }

        public Team(string name)
        {
            Name = name;
            numberOfPlayers = new List<Player>();         
        }

        public void AddPlayer(Player player)
        {
            numberOfPlayers.Add(player);
        }
        public void RemovePlayer(string playerName)
        {
            Player playerToRemove = numberOfPlayers.FirstOrDefault(p => p.Name == playerName);

            if (playerToRemove != null)
            {
                numberOfPlayers.Remove(playerToRemove);
            }
            else
            {
                throw new ArgumentException($"Player {playerName} is not in {Name} team.");
            }
        }
    }
}
