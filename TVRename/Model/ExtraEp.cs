namespace TVRename;

public class ExtraEp(int series, int episode, ProcessedSeason.SeasonType st)
{
    public bool Done = false;
    public readonly int EpisodeId = episode;
    public readonly int SeriesId = series;
    public readonly ProcessedSeason.SeasonType Order = st;
}
