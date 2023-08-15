using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players;
        private IRepository<ITeam> teams;

        public Controller()
        {
            players = new PlayerRepository();
            teams = new TeamRepository();
        }
        public string NewTeam(string name)
        {
            if (teams.ExistsModel(name))
            {
                return String.Format(OutputMessages.TeamAlreadyExists, name, "TeamRepository");
            }

            ITeam team = new Team(name);
            teams.AddModel(team);
            return String.Format(OutputMessages.TeamSuccessfullyAdded, name, "TeamRepository");
        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != "Goalkeeper" && typeName != "CenterBack" && typeName != "ForwardWing") 
            {
                return String.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }

            if (players.ExistsModel(name))
            {
                var existingPlayer = players.GetModel(name);
                return String.Format(OutputMessages.PlayerIsAlreadyAdded, name, "PlayerRepository", existingPlayer.GetType().Name);
            }

            IPlayer player = null;

            if (typeName == nameof(Goalkeeper))
            {
                player = new Goalkeeper(name);
            }
            else if (typeName == nameof(CenterBack))
            {
                player = new CenterBack(name);
            }
            else if (typeName == nameof(ForwardWing))
            {
                player = new ForwardWing(name);
            }

            players.AddModel(player);
            return String.Format(OutputMessages.PlayerAddedSuccessfully, name);

        }
        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
                return $"Player with the name {playerName} does not exist in the PlayerRepository.";

            if (!teams.ExistsModel(teamName))
                return $"Team with the name {teamName} does not exist in the TeamRepository.";

            var player = players.GetModel(playerName);
            var team = teams.GetModel(teamName);

            if (player.Team != null)
                return $"Player {playerName} has already signed with {player.Team}.";

            team.SignContract(player);
            player.JoinTeam(teamName);
            return $"Player {playerName} signed a contract with {teamName}.";
        }
        public string NewGame(string firstTeamName, string secondTeamName)
        {
            var firstTeam = teams.GetModel(firstTeamName);
            var secondTeam = teams.GetModel(secondTeamName);

            if (firstTeam.OverallRating > secondTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();
                return $"Team {firstTeamName} wins the game over {secondTeamName}!";
            }
            else if (secondTeam.OverallRating > firstTeam.OverallRating)
            {
                secondTeam.Win();
                firstTeam.Lose();
                return $"Team {secondTeamName} wins the game over {firstTeamName}!";
            }
            else
            {
                firstTeam.Draw();
                secondTeam.Draw();
                return $"The game between {firstTeamName} and {secondTeamName} ends in a draw!";
            }
        }
        public string PlayerStatistics(string teamName)
        {
            var team = teams.GetModel(teamName);
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"***{teamName}***");

            foreach (var player in team.Players.OrderByDescending(p => p.Rating).ThenBy(p => p.Name))
            {
                stringBuilder.AppendLine(player.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }

        public string LeagueStandings()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("***League Standings***");

            foreach (var team in teams.Models.OrderByDescending(t => t.PointsEarned).ThenByDescending(t => t.OverallRating).ThenBy(t => t.Name))
            {
                stringBuilder.AppendLine(team.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }


    }
}