namespace TVRename.Tests.Model;

/// <summary>
/// Pins when a cached series known to the library is marked for download again: a search result only, as any cached
/// media, and, for a series, the absence of seasons.
/// </summary>
public sealed class CachedSeriesInfoTests
{
    [Fact]
    public void UpgradeSearchResultToDirty_SearchResultOnly_BecomesDirtyFullRecord()
    {
        CachedSeriesInfo series = new(new Locale(), TVDoc.ProviderType.TMDB) { IsSearchResultOnly = true };

        series.UpgradeSearchResultToDirty();

        Assert.True(series.Dirty);
        Assert.False(series.IsSearchResultOnly);
    }

    [Fact]
    public void UpgradeSearchResultToDirty_FullRecordWithoutSeasons_BecomesDirty()
    {
        CachedSeriesInfo series = new(new Locale(), TVDoc.ProviderType.TMDB);

        series.UpgradeSearchResultToDirty();

        Assert.True(series.Dirty);
        Assert.False(series.IsSearchResultOnly);
    }
}
