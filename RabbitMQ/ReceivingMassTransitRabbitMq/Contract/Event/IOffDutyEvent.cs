using System;

namespace Contract.Event
{
    //事件
    public interface IOffDutyEvent
    {
        DateTime ClosingTime { get; set; }

        string Department { get; set; }
    }
}