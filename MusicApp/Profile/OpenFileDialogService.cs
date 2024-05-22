using Microsoft.Win32;

public class OpenFileDialogService : IOpenFileDialogService
{
    private OpenFileDialog openFileDialog = new OpenFileDialog();

    public bool ShowDialog()
    {
        return openFileDialog.ShowDialog() == true;
    }

    public string FileName
    {
        get { return openFileDialog.FileName; }
    }
}

