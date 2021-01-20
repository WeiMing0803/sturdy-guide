using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using ReceiveMessage.Consumers;

namespace ReceiveExtensions
{
    public static  class MQExtensions
    {
        public static void AddRabbitMQService(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                //x.AddConsumers(GetConsumerList().ToArray());  //效果和下面两个是一样的
                x.AddConsumer<GanFanConsumer>();
                x.AddConsumer<OffDutyConsumer>();

                x.UsingRabbitMq((context, config) =>
               {
                   config.Host("rabbitmq://39.99.140.121:5672", hostConfig =>
                   {
                       hostConfig.Username("test");
                       hostConfig.Password("test");
                   });

                   config.ReceiveEndpoint("OffDuty", e =>
                   {
                       e.ConfigureConsumer<OffDutyConsumer>(context);
                   });

                   config.ReceiveEndpoint("GanFan", e =>
                   {
                       e.ConfigureConsumer<GanFanConsumer>(context);
                   });
               });
            });

            services.AddMassTransitHostedService();
        }

        private static IList<Assembly> GetConsumerList()
        {
            var Consumer = new List<Assembly>();
            Assembly lConsumers = Assembly.Load("ReceiveMessage.Consumers");
            foreach (var lConsumer in lConsumers.GetTypes())
            {
                if (lConsumer.Name.Contains("Consumer"))
                {
                    Consumer.Add(lConsumer.Assembly);
                }
            }

            return Consumer;
        }
    }
}