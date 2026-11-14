namespace TVRename;

public interface IDialogParent : IWin32Window
{
    void ShowChildDialog(Form childForm);
}
