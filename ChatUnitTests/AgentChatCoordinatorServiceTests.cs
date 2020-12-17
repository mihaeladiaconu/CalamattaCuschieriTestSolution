using System.Collections.Generic;
using System.Linq;
using ChatCoordinator;
using ChatInfrastructure.DataModels;
using Xunit;

namespace ChatUnitTests
{
    public class AgentChatCoordinatorServiceTests
    {
        [Fact]
        public void AssignChatShouldConsiderSeniorLevel()
        {
            var agents = new List<Agent>
            {
                new Agent("Senior 1", SeniorityLevel.Senior),
                new Agent("Junior 1", SeniorityLevel.Junior)
            };

            var chatMessages = new[] {"message 1", "message 2", "message 3", "message 4", "message 5"};

            var chatCoordinatorService = new AgentChatCoordinatorService(agents, new List<OverflowAgent>());
            foreach (var chatMessage in chatMessages)
            {
                chatCoordinatorService.AssignChat(chatMessage);
            }

            Assert.Equal(4, agents.Where(x => x.SeniorityLevel == SeniorityLevel.Junior).Sum(x => x.WorkList.Count));
            Assert.Equal(1, agents.Where(x => x.SeniorityLevel == SeniorityLevel.Senior).Sum(x => x.WorkList.Count));
        }

        [Fact]
        public void AssignChatShouldConsiderMidLevel()
        {
            var agents = new List<Agent>
            {
                new Agent("Mid 1", SeniorityLevel.Mid),
                new Agent("Junior 1", SeniorityLevel.Junior),
                new Agent("Junior 2", SeniorityLevel.Junior),
            };

            var chatMessages = new[] { "message 1", "message 2", "message 3", "message 4", "message 5", "message 6" };

            var chatCoordinatorService = new AgentChatCoordinatorService(agents, new List<OverflowAgent>());
            foreach (var chatMessage in chatMessages)
            {
                chatCoordinatorService.AssignChat(chatMessage);
            }

            Assert.Equal(6, agents.Where(x => x.SeniorityLevel == SeniorityLevel.Junior).Sum(x => x.WorkList.Count));
            Assert.Equal(0, agents.Where(x => x.SeniorityLevel == SeniorityLevel.Mid).Sum(x => x.WorkList.Count));
        }
    }
}
