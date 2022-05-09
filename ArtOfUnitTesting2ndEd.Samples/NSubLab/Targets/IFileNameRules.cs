namespace NSubLab.Targets
{
    public interface IFileNameRules
    {
        bool IsValidLogFileName(string fileName);

        string GetFileNameIndex(int index);
    }
}