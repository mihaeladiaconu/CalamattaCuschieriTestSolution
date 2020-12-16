using System.Collections.Generic;

namespace ChatInfrastructure.DataModels
{
    public class Agent
    {
        public string Name { get; }
        public SeniorityLevel SeniorityLevel { get; }
        public int WorkCapacity { get; }
        public List<string> WorkList { get; }

        public Agent(string name, SeniorityLevel seniorityLevel)
        {
            Name = name;
            SeniorityLevel = seniorityLevel;
            WorkCapacity = (int) (10 * SeniorityMultiplier.GetSeniorityMultiplier(seniorityLevel));
            WorkList = new List<string>(WorkCapacity);
        }

        public bool HasAvailableCapacity()
        {
            return WorkList.Count != WorkCapacity;
        }

        public bool StartChat(string message)
        {
            if (!HasAvailableCapacity()) return false;

            WorkList.Add(message);
            return true;
        }

        public void EndChat(string message)
        {
            if (WorkList.Count == 0) return;

            WorkList.Remove(message);
        }
    }
}
