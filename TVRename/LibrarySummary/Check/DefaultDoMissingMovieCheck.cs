namespace TVRename;

internal class DefaultDoMissingMovieCheck(MovieConfiguration movie, TVDoc doc) : DefaultMovieCheck(movie, doc)
{
    protected override string FieldName => "Do Missing Check";

    protected override bool Field => Movie.DoMissingCheck;

    protected override bool Default => TVSettings.Instance.DefMovieDoMissingCheck;

    protected override async Task FixInternal()
    {
        Movie.DoMissingCheck = Default;
    }
}
