namespace TVRename;

public abstract class ScanShowActivity(TVDoc doc) : ScanMediaActivity(doc)
{
    protected abstract Task CheckAsync(ShowConfiguration si, DirFilesCache dfc, TVDoc.ScanSettings settings);

    public async Task CheckIfActiveAsync(ShowConfiguration si, DirFilesCache dfc, TVDoc.ScanSettings settings)
    {
        if (Active())
        {
            await CheckAsync(si, dfc, settings);
            LogActionListSummary();
        }
    }
}
