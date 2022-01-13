using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
namespace Server
{
    class Program
    {
        private static Consumer _consumer;
        private static readonly string _queueName = "bm";
        static void Main(string[] args)
        {
            _consumer = new Consumer(_queueName);
            Console.ReadKey();
        }
    }

    #region okuyan
    public class Consumer
    {
        private readonly RabbitMQService _rabbitMQService;

        public Consumer(string queueName)
        {
            _rabbitMQService = new RabbitMQService();

            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    // Received event'i sürekli listen modunda olacaktır.
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[-] {0}", message);
                    };

                    channel.BasicConsume(queueName, true, consumer);
                    Console.ReadLine();
                }
            }
        }
    }

    public class RabbitMQService
    {
        // localhost üzerinde kurulu olduğu için host adresi olarak bunu kullanıyorum.
        private readonly string _hostName = "localhost";

        public IConnection GetRabbitMQConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory()
            {
                // RabbitMQ'nun bağlantı kuracağı host'u tanımlıyoruz. Herhangi bir güvenlik önlemi koymak istersek, Management ekranından password adımlarını tanımlayıp factory içerisindeki "UserName" ve "Password" property'lerini set etmemiz yeterlidir.
                HostName = _hostName
            };

            return connectionFactory.CreateConnection();
        }
    }
    #endregion
}
