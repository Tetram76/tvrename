namespace TVRename;

internal abstract class ShowsExporter(List<ShowConfiguration> shows) : Exporter
{
    protected readonly List<ShowConfiguration> Shows = shows;
}
