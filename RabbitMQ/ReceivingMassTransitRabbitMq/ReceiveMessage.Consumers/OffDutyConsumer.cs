using System;
using Contract.Event;
using MassTransit;
using System.Threading.Tasks;
using Contract.Command;
using Microsoft.Extensions.Logging;

namespace ReceiveMessage.Consumers
{
    public class OffDutyConsumer : IConsumer<IOffDutyEvent>
    {
        private readonly ILogger<OffDutyConsumer>  _logger;

        public OffDutyConsumer(ILogger<OffDutyConsumer> logger)
        {
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<IOffDutyEvent> context)
        {
            _logger.LogInformation(context.Message.Department + "下班时间到了：" + context.Message.ClosingTime);
            //接受事件，执行指令
            await DoGanFan(context);

        }

        private static Task DoGanFan(ConsumeContext<IOffDutyEvent> context)
        {
            return context.Publish<IGanFanCommand>(new
            {
                index = new Random().Next(),
                DepartmentName = context.Message.Department
            });
        }
    }
}