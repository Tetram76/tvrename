namespace TVRename;

public abstract class ActionDelete : ActionFileOperation
{
    public override string Name => "Delete";
    public override long SizeOfWork => 100;
    public override QueueName Queue() => QueueName.quickFileOperation;
    public override int IconNumber => 9;
    public override string ScanListViewGroup => "lvgActionDelete";

    public override string? DestinationFolder => TargetFolder;
    public override string DestinationFile => ProgressText;
}
