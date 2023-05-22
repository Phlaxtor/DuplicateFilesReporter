namespace Guting.DuplicateFilesReporter
{
    public sealed class DirectoryInput : ReportInput
    {
        public DirectoryInput(string directoryPath, Settings settings) : base(directoryPath, settings)
        {
        }

        public override IEnumerable<FileInfo> GetFiles()
        {
            var info = new DirectoryInfo(Path);
            var options = GetOptions();
            foreach (var file in info.GetFiles(Settings.SearchPattern, options))
            {
                yield return file;
            }
        }

        private EnumerationOptions GetOptions()
        {
            var options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            options.MatchCasing = Settings.MatchCasing ? MatchCasing.CaseSensitive : MatchCasing.CaseInsensitive;
            options.MatchType = MatchType.Simple;
            options.MaxRecursionDepth = Settings.MaxRecursionDepth;
            options.RecurseSubdirectories = Settings.Recursive;
            options.ReturnSpecialDirectories = Settings.ReturnSpecialDirectories;
            return options;
        }
    }
}