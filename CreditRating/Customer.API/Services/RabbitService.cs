using RabbitMQ.Client;
using System.Text;

namespace Customer.API.Services
{
    public class RabbitService
    {
        public Common.EnvironmentsBase environmentsBase;

        public RabbitService(IConfiguration _configuration)
        {
            environmentsBase = new Common.EnvironmentsBase(_configuration);
        }

        public void Publish(string message)
        {
            ConnectionFactory _factory = new ConnectionFactory()            
            {
                Uri = new Uri(environmentsBase.MQ_CONNECTIONSTRING),
                UserName = environmentsBase.MQ_USERNAME,
                Password = environmentsBase.MQ_PASSWORD
            };

            var body = Encoding.UTF8.GetBytes(message);
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();
            {
                channel.QueueDeclare(queue: environmentsBase.MQ_QUEUE, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicPublish(exchange: "", routingKey: environmentsBase.MQ_QUEUE, basicProperties: null, body: body);
            }

        }
    }
}
