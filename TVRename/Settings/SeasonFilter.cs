namespace TVRename;

public class SeasonFilter
{
    public bool HideIgnoredSeasons { get; set; }

    public bool Filter(ShowConfiguration si, ProcessedSeason sea)
    {
        if (sea.SeasonNumber == 0 && TVSettings.Instance.IgnoreAllSpecials)
        {
            return true;
        }

        return !HideIgnoredSeasons || !si.IgnoreSeasons.Contains(sea.SeasonNumber);
    }
}
