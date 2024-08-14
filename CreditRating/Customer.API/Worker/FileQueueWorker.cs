using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Customer.API.Interfaces;

namespace Customer.API.Worker
{
     public class FileQueueWorker : IHostedService
    {
        private readonly ILogger<FileQueueWorker> _logger;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;


        public FileQueueWorker(IConfiguration configuration,
            ICustomerService customerService,
            ILogger<FileQueueWorker> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _customerService = customerService;
            var connectionString = _configuration["MQ_RABBIT:MQ_CONNECTIONSTRING"];
            var username = _configuration["MQ_RABBIT:MQ_USERNAME"];
            var password = _configuration["MQ_RABBIT:MQ_PASSWORD"];
            _queueName = _configuration["MQ_RABBIT:EXCHANGE"];

            var factory = new ConnectionFactory()
            {
                Uri = new Uri(connectionString),
                UserName = username,
                Password = password,
                
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var args = new Dictionary<string, object>();
            _channel.QueueDeclare(queue: _queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,                                  
                                  arguments: args);
                                  
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                try
                {
                    if (ea.BasicProperties.Headers != null && ea.BasicProperties.Headers.ContainsKey("Origin"))
                    {
                        var origin = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["Origin"]);

                        if (origin == "Proposal.API")
                        {
                            var result = _customerService.ProcessMessage(origin, message).Result;
                            if (result)
                            {
                                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                            }
                        }
                        else if (origin == "Card.API")
                        {
                            var result = _customerService.ProcessMessage(origin, message).Result;
                            if (result)
                            {
                                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Origem desconhecida");
                        // Processamento genérico ou tratamento de erro
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error processing message: " + ex.Message, ex);
                    _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                }
                
            };
            _channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close(200, "Goodbye");
            _connection.Close();
            return Task.CompletedTask;
        }
    }
}