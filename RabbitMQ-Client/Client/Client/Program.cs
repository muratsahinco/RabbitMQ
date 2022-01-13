using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Client
{
    class Program
    {
        private static readonly string _queueName = "bm";
        private static Publisher _publisher;


        static void Main(string[] args)
        {
            for (int i = 0; i < 1000; i++)
            {
                _publisher = new Publisher(_queueName, "Hellov!-" + i.ToString());
                System.Threading.Thread.Sleep(1000);
            }

            Console.ReadKey();
        }
    }

    #region yazan
    public class Publisher
    {
        private readonly RabbitMQService _rabbitMQService;

        public Publisher(string queueName, string message)
        {
            _rabbitMQService = new RabbitMQService();

            using (var connection = _rabbitMQService.GetRabbitMQConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queueName, false, false, false, null);

                    channel.BasicPublish("", queueName, null, Encoding.UTF8.GetBytes(message));

                    Console.WriteLine("{0} queue'su üzerine, \"{1}\" mesajı yazıldı.", queueName, message);
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
