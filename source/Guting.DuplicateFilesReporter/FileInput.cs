namespace Guting.DuplicateFilesReporter
{
    public sealed class FileInput : ReportInput
    {
        public FileInput(string filePath, Settings settings) : base(filePath, settings)
        {
        }

        public override IEnumerable<FileInfo> GetFiles()
        {
            var file = new FileInfo(Path);
            yield return file;
        }
    }
}