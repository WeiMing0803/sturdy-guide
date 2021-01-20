using System;

namespace Contract.Event
{
    public interface IOffDutyEvent
    {
        DateTime ClosingTime { get; set; }
        string Department { get; set; }
    }
}