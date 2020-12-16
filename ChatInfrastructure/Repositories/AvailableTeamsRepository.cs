using System;
using System.Collections.Generic;
using ChatInfrastructure.DataModels;

namespace ChatInfrastructure.Repositories
{
    public static class AvailableTeamsRepository
    {
        private static readonly List<Agent> TeamA = new List<Agent>
        {
            new Agent("A: Team Lead 1", SeniorityLevel.TeamLead),
            new Agent("A: Mid 1", SeniorityLevel.Mid),
            new Agent("A: Mid 2", SeniorityLevel.Mid),
            new Agent("A: Junior 1", SeniorityLevel.Junior),
        };

        private static readonly List<Agent> TeamB = new List<Agent>
        {
            new Agent("B: Senior 1", SeniorityLevel.Senior),
            new Agent("B: Mid 1", SeniorityLevel.Mid),
            new Agent("B: Junior 1", SeniorityLevel.Junior),
            new Agent("B: Junior 2", SeniorityLevel.Junior),
        };

        private static readonly List<Agent> TeamC = new List<Agent>
        {
            new Agent("C: Mid 1", SeniorityLevel.Mid),
            new Agent("C: Mid 2", SeniorityLevel.Mid),
        };

        public static List<OverflowAgent> OverflowAgents = new List<OverflowAgent>
        {
            new OverflowAgent("Overflow 1"),
            new OverflowAgent("Overflow 2"),
            new OverflowAgent("Overflow 3"),
            new OverflowAgent("Overflow 4"),
            new OverflowAgent("Overflow 5"),
            new OverflowAgent("Overflow 6"),
        };

        public static Team GetTeam()
        {
            if (DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 16)
                return new Team(TeamA, OverflowAgents);

            // night shift
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 8)
                return new Team(TeamC, new List<OverflowAgent>(0));

            return new Team(TeamB, new List<OverflowAgent>(0));
        }
    }
}