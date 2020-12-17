using System.Collections.Generic;
using System.Linq;
using ChatInfrastructure.DataModels;

namespace ChatCoordinator
{
    public class AgentChatCoordinatorService
    {
        private readonly List<Queue<Agent>> _agentsBySeniority;

        public AgentChatCoordinatorService(List<Agent> agents, List<OverflowAgent> overflowAgents)
        {
            _agentsBySeniority = new List<Queue<Agent>>
            {
                new Queue<Agent>(agents.Where(x => x.SeniorityLevel == SeniorityLevel.Junior)),
                new Queue<Agent>(agents.Where(x => x.SeniorityLevel == SeniorityLevel.Mid)),
                new Queue<Agent>(agents.Where(x => x.SeniorityLevel == SeniorityLevel.Senior)),
                new Queue<Agent>(agents.Where(x => x.SeniorityLevel == SeniorityLevel.TeamLead)),
                new Queue<Agent>(overflowAgents)
            };
        }

        public Agent AssignChat(string message)
        {
            Agent assignedAgent = null;

            foreach (var agents in _agentsBySeniority)
            {
                if (!agents.Any(x => x.HasAvailableCapacity())) continue;

                var isChatAssigned = false;
                for (var i = 0; i < agents.Count; i++)
                {
                    var agent = agents.Dequeue();
                    if (!agent.HasAvailableCapacity())
                    {
                        agents.Enqueue(agent);
                        continue;
                    }

                    isChatAssigned = agent.StartChat(message);
                    agents.Enqueue(agent);

                    assignedAgent = agent;
                    break;
                }

                if (isChatAssigned) break;
            }

            return assignedAgent;
        }

        public void DeallocateChat(string message)
        {
            // TODO
        }
    }
}
