using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receiving
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
               //创建一个名为"hello"的队列，防止producer端没有创建该队列
               channel.QueueDeclare(
                   queue: "hello",
                   durable: false,
                   exclusive: false,
                   autoDelete: false,
                   arguments: null);

               //回调，当Receiving收到消息后就会执行该函数
               var consumer = new EventingBasicConsumer(channel);
               consumer.Received += (model, ea) =>
               {
                   var body = ea.Body.ToArray();
                   var message = Encoding.UTF8.GetString(body);
                   Console.WriteLine("[x] Received {0}", message);
               };

               //消费队列中"hello"的消息
               channel.BasicConsume(
                   queue: "hello",
                   autoAck: true,
                   consumer: consumer);

               Console.WriteLine(" Press [enter] to exit.");
               Console.ReadLine();
           }
        }
    }
}
