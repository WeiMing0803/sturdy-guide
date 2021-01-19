using System;
using System.Text;
using RabbitMQ.Client;

namespace Sending
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接工厂
            var factory = new ConnectionFactory()
            {
                HostName = "39.99.140.121",
                Port = 5672,
                UserName = "test",
                Password = "test",
                VirtualHost = "/zsq"
            };

            //创建连接
            using (var connection = factory.CreateConnection())

            //创建通道
            using (var channel = connection.CreateModel())
            {
                //创建一个名叫"hello"的消息队列
                channel.QueueDeclare(
                    queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                string message = "Holle World";

                var body = Encoding.UTF8.GetBytes(message);

                //向消息队列发送消息message
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "hello",
                    basicProperties: null,
                    body: body);
                Console.WriteLine("[x] sent {0}", message);
            }

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
