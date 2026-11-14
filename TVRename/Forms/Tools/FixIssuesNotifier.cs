namespace TVRename.Forms.Tools;

public class FixIssuesNotifier : TaskNotifier
{
    Progress<TaskProgress> progressHandler;
    public FixIssuesNotifier(CancellationTokenSource cts) : base("Fix Issues", cts)
    {
        progressHandler = new Progress<TaskProgress>(UpdateProgress);
    }
    public async Task Run(LongOperation operation)
    {
        task = operation.StartAsync(progressHandler, cancellationToken.Token);
        await Task.Yield();

        await task;
        Close();
    }
}
