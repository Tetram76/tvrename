namespace TVRename;




public abstract class ItemMissing(string theFileNoExt, string filename, string folder) : Item
{
    public string TheFileNoExt = theFileNoExt;
    public string Filename = filename;
    private readonly string folder = folder;

    public override string DestinationFile => Filename;
    public override string ScanListViewGroup => "lvgActionMissing";
    public override string DestinationFolder => folder;
    public override string TargetFolder
    {
        get
        {
            try
            {
                return new FileInfo(TheFileNoExt).DirectoryName;
            }
            catch (NotSupportedException)
            {
                return string.Empty;
            }
        }
    }

    public override bool CheckedItem { get => false; set { } }
    public override int IconNumber => 1;
    public abstract bool DoRename { get; }
    public abstract MediaConfiguration Show { get; }
    public override IgnoreItem? Ignore => GenerateIgnore(TheFileNoExt);

    public void AddComment(string p0)
    {
        ErrorText += p0;
        NotifyPropertyChanged();
    }
}
