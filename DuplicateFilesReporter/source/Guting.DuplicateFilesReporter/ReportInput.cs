namespace Guting.DuplicateFilesReporter
{
    public abstract class ReportInput
    {
        protected ReportInput(string path, Settings settings)
        {
            Path = path;
            Settings = settings;
        }

        public string Path { get; }
        public Settings Settings { get; }

        public static ReportInput GetInput(string path, Settings settings)
        {
            if (Directory.Exists(path)) return new DirectoryInput(path, settings);
            if (File.Exists(path)) return new FileInput(path, settings);
            throw new ArgumentException($"Provided value does not refere to an existing directory/file: '{path}'", nameof(path));
        }

        public abstract IEnumerable<FileInfo> GetFiles();
    }
}