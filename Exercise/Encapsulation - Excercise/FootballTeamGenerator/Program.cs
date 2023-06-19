using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FootballTeamGenerator
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Team> teams = new List<Team>();

            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                string[] teamInfo = input.Split(';');
                string action = teamInfo[0];

                if (action == "Team")
                {
                    string teamName = teamInfo[1];
                    AddTeam(teamName, teams);
                }
                else if (action == "Add")
                {
                    //"Add;{TeamName};{PlayerName};{Endurance};{Sprint};{Dribble};{Passing};{Shooting}" - add a new 
                    //Player to the Team;

                    string teamName = teamInfo[1];                    
                    string playerName = teamInfo[2];
                    int endurance = int.Parse(teamInfo[3]);
                    int sprint = int.Parse(teamInfo[4]);
                    int dribble = int.Parse(teamInfo[5]);
                    int passing = int.Parse(teamInfo[6]);
                    int shooting = int.Parse(teamInfo[7]);

                    AddPlayer(teamName, playerName, endurance, sprint, dribble, passing, shooting, teams);
                    
                }
                else if (action == "Remove")
                {
                    //Remove;Arsenal;Aaron_Ramsey
                    string teamName = teamInfo[1];
                    string playerName = teamInfo[2];

                    RemovePlayer(teamName, playerName, teams);
                }
                else if (action == "Rating")
                {
                    string teamName = teamInfo[1];
                    RateTeam(teamName, teams);
                }

            }
        }
        static void AddTeam(string teamName, List<Team> teams)
        {
            teams.Add(new Team(teamName));
        }

        static void AddPlayer(string teamName, string playerName, int endurance, int sprint, int dribble, int passing, int shooting, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team == null)
            {
                Console.WriteLine($"Team {teamName} does not exist.");
                return;
            }

            try
            {
                Player player = new Player(playerName, endurance, sprint, dribble, passing, shooting);
                team.AddPlayer(player);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }           
        }
        static void RemovePlayer(string teamName, string personName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team == null)
            {
                Console.WriteLine($"Team {teamName} does not exist.");
                return;
            }

            try
            {
                team.RemovePlayer(personName);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        static void RateTeam(string teamName, List<Team> teams)
        {
            Team team = teams.FirstOrDefault(t => t.Name == teamName);

            if (team == null)
            {
                Console.WriteLine($"Team {teamName} does not exist.");
                return;
            }

            Console.WriteLine($"{teamName} - {team.Rating}");

        }
    }
}
