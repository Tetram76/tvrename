namespace TVRename;

public class ScanProgressReport : ProgressReport
{
    public string LatestAction { get; set; } = string.Empty;
}

public class ProgressReport
{
    public int ProgressPercentage { get; set; }
    public string UpdateText { get; set; } = string.Empty;

}
