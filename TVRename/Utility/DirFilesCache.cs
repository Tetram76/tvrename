// Will cache the file lists of contents of single directories.  Will return the cached
// data, or read cache and return it.

namespace TVRename;

public class DirFilesCache
{
    private readonly ConcurrentDictionary<string, FileInfo[]> cache = [];
    private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

    public IEnumerable<FileInfo> GetFilesIncludeSubDirs(string folder) => Get(folder, true);

    public FileInfo[] GetFiles(string folder) => Get(folder, false);

    private FileInfo[] Get(string folder, bool includeSubs)
    {
        if (cache.TryGetValue(folder, out FileInfo[]? value))
        {
            return value;
        }

        DirectoryInfo di;
        try
        {
            di = new DirectoryInfo(folder);
        }
        catch
        {
            cache[folder] = [];
            return [];
        }
        if (!di.Exists)
        {
            cache[folder] = [];
            return [];
        }

        try
        {
            FileInfo[] files = includeSubs ? di.GetFiles("*", System.IO.SearchOption.AllDirectories) : di.GetFiles();
            cache[folder] = files;
            return files;
        }
        catch (System.IO.IOException)
        {
            Logger.Warn("IOException occurred trying to access " + folder);
            return [];
        }
        catch (UnauthorizedAccessException)
        {
            Logger.Warn("UnauthorizedAccessException occurred trying to access " + folder);
            return [];
        }
    }
}
