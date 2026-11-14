namespace TVRename;

// ReSharper disable once InconsistentNaming
internal class RecentWPLExporter(TVDoc doc) : RecentExporter(doc)
{
    public override bool Active() => TVSettings.Instance.ExportRecentWPL;
    protected override string Name() => "Recent WPL Exporter";

    protected override string Location() => TVSettings.Instance.ExportRecentWPLTo;

    protected override string GenerateHeader() => $"<?wpl version=\"1.0\"?>\r\n<smil>\r\n    <head>\r\n        <meta name=\"Generator\" content=\"TV Rename -- {Helpers.DisplayVersion}\"/>\r\n        <title>Recent ASX Export</title>\r\n    </head>\r\n    <body>\r\n        <seq>";

    protected override string GenerateRecord(ProcessedEpisode ep, FileInfo file, string name, TimeSpan? length)
    {
        string filen = System.Security.SecurityElement.Escape(file.UrlPathFullName());
        return $"             <media src={filen.InDoubleQuotes()}/>";
    }

    protected override string GenerateFooter() => "         </seq>\r\n    </body>\r\n</smil>";
}
