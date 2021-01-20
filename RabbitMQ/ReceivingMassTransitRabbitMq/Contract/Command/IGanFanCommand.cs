namespace Contract.Command
{
    //指令
    public interface IGanFanCommand
    {
        int Index { get; }

        string DepartmentName { get; }
    }
}