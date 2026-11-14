namespace TVRename;

internal class DefaultDoRenameTvCheck(ShowConfiguration show, TVDoc doc) : DefaultTvShowCheck(show, doc)
{
    protected override string FieldName => "Rename Check";

    protected override bool Field => Show.DoRename;

    protected override bool Default => TVSettings.Instance.DefShowDoRenaming;

    protected override async Task FixInternal()
    {
        Show.DoRename = Default;
    }
}
