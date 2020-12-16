using System;
using System.Text;
using ChatApi.Configuration;
using ChatInfrastructure;
using ChatInfrastructure.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ChatApi.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private IConnection _connection;

        private readonly ILogger<PublisherService> _logger;

        public PublisherService(IOptions<RabbitMqConfiguration> rabbitMqOptions, ILogger<PublisherService> logger)
        {
            _logger = logger;

            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
        }

        public bool Publish(string message)
        {
            if (!ConnectionExists()) return false;

            using (var channel = _connection.CreateModel())
            {
                var queue = channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var team = AvailableTeamsRepository.GetTeam();
                var teamCapacity = team.GetCapacity();

                if (queue.MessageCount == teamCapacity)
                    return false;

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }

            return true;
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not create RabbitMQ connection");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}
