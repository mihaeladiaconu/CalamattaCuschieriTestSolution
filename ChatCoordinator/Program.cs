using System;
using System.Text;
using ChatInfrastructure;
using ChatInfrastructure.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatCoordinator
{
    internal class Program
    {
        private const string QueueName = "ChatQueue";
        private const string HostName = "localhost";

        static void Main(string[] args)
        {
            Console.WriteLine("Chat coordinator started");

            var team = AvailableTeamsRepository.GetTeam();
            var agentChatCoordinatorService = new AgentChatCoordinatorService(team.Agents, team.OverflowAgents);

            var factory = new ConnectionFactory
            {
                HostName = HostName
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(QueueName, false, false, false, null);
                Console.WriteLine("Waiting for messages...");

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, eventArguments) =>
                {
                    var message = Encoding.UTF8.GetString(eventArguments.Body.ToArray());
                    
                    Console.WriteLine($"Assigning message {message} to agent...");

                    var assignedAgent = agentChatCoordinatorService.AssignChat(message);

                    Console.WriteLine($"Message {message} assigned to {assignedAgent.Name}");
                };

                channel.BasicConsume(QueueName, true, consumer);
            }

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
