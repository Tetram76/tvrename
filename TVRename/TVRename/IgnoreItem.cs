// An "IgnoreItem" represents a file/episode to never ask the user about again. (Right-click->Ignore Selected / Options->Ignore List)



namespace TVRename;

public class IgnoreItem(string fileAndPath)
{
    public readonly string FileAndPath = fileAndPath;

    public bool SameFileAs(IgnoreItem? o)
    {
        if (string.IsNullOrEmpty(FileAndPath) || string.IsNullOrEmpty(o?.FileAndPath))
        {
            return false;
        }

        return FileAndPath == o.FileAndPath;
    }

    public bool MatchesEpisode(string folder, ProcessedEpisode episode)
    {
        if (!FileAndPath.StartsWith(folder, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (!folder.HasValue())
        {
            return false;
        }

        string plannedFilename = TVSettings.Instance.FilenameFriendly(TVSettings.Instance.NamingStyle.NameFor(episode));

        return FileAndPath.EndsWith(plannedFilename, StringComparison.OrdinalIgnoreCase);
    }
}
