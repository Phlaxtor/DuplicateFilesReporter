namespace Guting.DuplicateFilesReporter
{
    public sealed class FileGrouping
    {
        private readonly ICollection<FoundFile> _files = new List<FoundFile>(1);

        public FileGrouping(string hash)
        {
            Hash = hash;
        }

        public int Count => _files.Count;
        public string Hash { get; }

        public void Add(FoundFile file)
        {
            _files.Add(file);
        }
    }
}