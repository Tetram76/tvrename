using TVRename.Forms;

namespace TVRename;

// ReSharper disable once InconsistentNaming
public partial class TVRenameSplash : Form
{
    public TVRenameSplash()
    {
        InitializeComponent();
        lblVersion.Text = Helpers.DisplayVersion.ToUiVersion();
    }

    public void Update(string status, int progress, string details)
    {
        UpdateStatus(status);
        UpdateInfo(details);
        UpdateProgress(progress);
    }

    public void UpdateStatus(string status) => lblStatus.SafeInvoke(() => lblStatus.Text = status.ToUiVersion(), true);
    public void UpdateProgress(int progress) => prgComplete.SafeInvoke(() => prgComplete.SetProgress(progress), true);
    public void UpdateInfo(string info) => lblInfo.SafeInvoke(() => lblInfo.Text = info.ToUiVersion(), true);
}
