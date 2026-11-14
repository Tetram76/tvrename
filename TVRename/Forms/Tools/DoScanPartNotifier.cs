namespace TVRename.Forms.Tools;

public class DoScanPartNotifier : TaskNotifier
{
    private readonly PostScanActivity activity;

    public DoScanPartNotifier(PostScanActivity activity, CancellationTokenSource cancellationToken) : base(activity.ActivityName(), cancellationToken)
    {
        this.activity = activity;
    }

    public async Task DoAsync()
    {
        var progressHandler = new Progress<TaskProgress>(UpdateProgress);

        task = activity.CheckAsync(progressHandler, cancellationToken.Token);
        Start(task);
        await task;
        Close();
    }
}

