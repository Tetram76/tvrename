namespace TVRename;



public abstract class ActionDateTouch(DateTime time) : ActionFileMetaData
{
    protected readonly DateTime UpdateTime = time;

    #region Action Members

    public override string Name => "Update Timestamp";
    public override long SizeOfWork => 100;

    #endregion Action Members

    public override string ScanListViewGroup => "lvgUpdateFileDates";
    public override int IconNumber => 7;

    public override string AirDateString =>
        UpdateTime.CompareTo(DateTime.MaxValue) != 0 ? UpdateTime.ToShortDateString() : string.Empty;

    public override DateTime? AirDate => UpdateTime;

    public override QueueName Queue() => QueueName.later;
    public override int Order => 10;
}
