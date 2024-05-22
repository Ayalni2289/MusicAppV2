public interface IOpenFileDialogService
{
    bool ShowDialog();
    string FileName { get; }
}
