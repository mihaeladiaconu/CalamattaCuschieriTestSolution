using System.Collections.Generic;
using ChatInfrastructure.DataModels;
using Xunit;

namespace ChatUnitTests
{
    public class TeamTests
    {
        [Fact]
        public void VerifyTeamCapacityWithoutOverflow()
        {
            var agents = new List<Agent>()
            {
                new Agent("Mid 1", SeniorityLevel.Mid),
                new Agent("Mid 2", SeniorityLevel.Mid),
                new Agent("Junior 1", SeniorityLevel.Junior)
            };
            var team = new Team(agents, new List<OverflowAgent>());
            Assert.Equal(24, team.GetCapacity());
        }

        [Fact]
        public void VerifyTeamCapacityWithOverflow()
        {
            var agents = new List<Agent>()
            {
                new Agent("Mid 1", SeniorityLevel.Mid),
                new Agent("Mid 2", SeniorityLevel.Mid),
                new Agent("Junior 1", SeniorityLevel.Junior)
            };
            var overflowAgents = new List<OverflowAgent>()
            {
                new OverflowAgent("Overflow")
            };
            var team = new Team(agents, overflowAgents);
            Assert.Equal(28, team.GetCapacity());
        }
    }
}
