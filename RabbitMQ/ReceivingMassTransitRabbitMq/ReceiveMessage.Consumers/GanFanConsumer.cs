using Contract.Command;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ReceiveMessage.Consumers
{
    public class GanFanConsumer : IConsumer<IGanFanCommand>
    {
        private readonly ILogger<GanFanConsumer> _logger;

        public GanFanConsumer(ILogger<GanFanConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<IGanFanCommand> context)
        {
            var logInfo = await Task.Run<string>(() =>
                $"{context.Message.DepartmentName}下班，编号{context.Message.Index}开始干饭"
            );

            _logger.LogInformation(logInfo);
        }
    }
}