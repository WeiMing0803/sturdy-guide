using Microsoft.Extensions.DependencyInjection;
using MassTransit;

namespace SendExtension
{
  public static class MQExtension
    {
        public static void AddRabbitMQService(this IServiceCollection service)
        {
            //通过MassTransit创建MQ联接工厂
            service.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("rabbitmq://39.99.140.121:5672", hostConfig =>
                    {
                        hostConfig.Username("test");
                        hostConfig.Password("test");
                    });
                });
            });
            service.AddMassTransitHostedService();
        }
    }
}
