using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string _queueName = "bm";
        private static Publisher _publisher;


        public ActionResult Index()
        {
            for (int i = 0; i < 10; i++)
            {
                _publisher = new Publisher(_queueName, "Hellov!-" + i.ToString());
                System.Threading.Thread.Sleep(1000);
            }


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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