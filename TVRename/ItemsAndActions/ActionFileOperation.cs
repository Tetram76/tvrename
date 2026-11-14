namespace TVRename;

public abstract class ActionFileOperation : Action
{
    protected TVSettings.TidySettings? Tidyup;

    protected void DoTidyUp(DirectoryInfo? di) => FileHelper.DoTidyUp(di, Tidyup);

    protected void DeleteOrRecycleFolder(DirectoryInfo? di) => FileHelper.DeleteOrRecycleFolder(di, Tidyup);
}
