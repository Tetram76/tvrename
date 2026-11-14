namespace TVRename;

internal class UrlCache
{
    private readonly ConcurrentDictionary<string, string> internalCache = new();

    public async Task<string> GetUrlAsync(string s, bool instanceSearchJsonUseCloudflare)
    {
        if (internalCache.TryGetValue(s, out string? value))
        {
            return value;
        }

        string newValue = await HttpHelper.GetUrlAsync(s, instanceSearchJsonUseCloudflare);
        try
        {
            internalCache.TryAdd(s, newValue);
        }
        catch (OverflowException)
        {
            return newValue;
        }
        return newValue;
    }
}
