namespace TVRename;

public class DirCacheEntry(FileInfo f)
{
    public readonly long Length = f.Length;
    public readonly FileInfo TheFile = f;
}
