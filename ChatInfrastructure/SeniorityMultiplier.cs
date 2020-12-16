using System.Collections.Generic;
using ChatInfrastructure.DataModels;

namespace ChatInfrastructure
{
    public sealed class SeniorityMultiplier
    {
        public static Dictionary<SeniorityLevel, double> Multipliers = new Dictionary<SeniorityLevel, double>
        {
            {SeniorityLevel.Junior, 0.4},
            {SeniorityLevel.Mid, 0.6},
            {SeniorityLevel.Senior, 0.8},
            {SeniorityLevel.TeamLead, 0.5},
        };

        public static double GetSeniorityMultiplier(SeniorityLevel seniorityLevel)
        {
            return Multipliers[seniorityLevel];
        }
    }
}