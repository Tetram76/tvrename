namespace TVRename;

internal abstract class MoviesExporter(List<MovieConfiguration> shows) : Exporter
{
    protected readonly List<MovieConfiguration> Shows = shows;
}
