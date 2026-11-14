namespace TVRename;

// ReSharper disable once InconsistentNaming
internal class RecentXSPFExporter(TVDoc doc) : RecentExporter(doc)
{
    public override bool Active() => TVSettings.Instance.ExportRecentXSPF;

    protected override string Location() => TVSettings.Instance.ExportRecentXSPFTo;
    protected override string Name() => "Recent XSPF Exporter";

    protected override string GenerateHeader()
    {
        return
            "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<playlist xmlns=\"http://xspf.org/ns/0/\" version=\"1\">\r\n\t<title>Playlist</title>\r\n\t<trackList>";
    }

    protected override string GenerateRecord(ProcessedEpisode ep, FileInfo file, string name, TimeSpan? length)
    {
        string location = System.Security.SecurityElement.Escape(file.UrlPathFullName());
        return $"\t\t<track>\r\n\t\t\t<location>{location}</location>\r\n\t\t\t<title>{System.Security.SecurityElement.Escape(name)}</title>\r\n\t\t</track>";
    }

    protected override string GenerateFooter() => "\t</trackList>\r\n</playlist>";
}
