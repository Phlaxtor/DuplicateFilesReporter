namespace Guting.DuplicateFilesReporter
{
    public sealed class Reporter
    {
        private readonly bool _allowAllTypes;
        private readonly IDictionary<string, FileGrouping> _duplicateFiles = new Dictionary<string, FileGrouping>();
        private readonly IList<FoundFile> _files = new List<FoundFile>();
        private readonly ISet<string> _types = new HashSet<string>();

        public Reporter(Settings settings)
        {
            Settings = settings;
            if (settings.FileTypes != null)
            {
                foreach (var type in settings.FileTypes)
                {
                    _types.Add(type.GetFileType());
                }
            }
            _allowAllTypes = _types.Count == 0;
        }

        public int Count => _files.Count;
        public Settings Settings { get; }

        public void AddFiles(IEnumerable<string> paths, CancellationToken cancellationToken)
        {
            foreach (string path in paths)
            {
                if (cancellationToken.IsCancellationRequested) break;
                AddFiles(path, cancellationToken);
            }
        }

        public void AddFiles(string path, CancellationToken cancellationToken)
        {
            var input = ReportInput.GetInput(path, Settings);
            foreach (FileInfo fileInfo in input.GetFiles())
            {
                if (cancellationToken.IsCancellationRequested) break;
                if (CanAddFile(fileInfo) == false) continue;
                var file = new FoundFile(fileInfo, Settings);
                _files.Add(file);
            }
        }

        public async Task GroupFiles(CancellationToken cancellationToken)
        {
            foreach (var file in _files)
            {
                if (cancellationToken.IsCancellationRequested) break;
                await Group(file, cancellationToken);
            }
        }

        private bool CanAddFile(FileInfo file)
        {
            if (_allowAllTypes) return true;
            string type = file.GetFileType();
            bool isSelectedFileType = _types.Contains(type);
            switch (Settings.FileTypesMode)
            {
                case FileTypeMode.Include: return isSelectedFileType == true;
                case FileTypeMode.Exclude: return isSelectedFileType == false;
                default: throw new ArgumentException($"Not supported mode '{Settings.FileTypesMode}'", nameof(Settings.FileTypesMode));
            }
        }

        private async Task Group(FoundFile file, CancellationToken cancellationToken)
        {
            var hash = await file.GetHashHex(cancellationToken);
            if (_duplicateFiles.TryGetValue(hash, out FileGrouping? files) == false)
            {
                _duplicateFiles[hash] = files = new FileGrouping(hash);
            }
            files.Add(file);
        }
    }
}