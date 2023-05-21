namespace Guting.DuplicateFilesReporter
{
    public sealed class Settings
    {
        public static Settings GetDefaultSettings()
        {
            return new Settings
            {
                FileTypes = Array.Empty<string>(),
                FileTypesMode = FileTypeMode.Include,
                HashName = "MD5",
                MatchCasing = false,
                MaxRecursionDepth = int.MaxValue,
                Recursive = true,
                ReturnSpecialDirectories = true,
                SearchPattern = "*",
            };
        }

        public FileTypeMode FileTypesMode { get; init; }
        public string[] FileTypes { get; init; }
        public string HashName { get; init; }
        public bool MatchCasing { get; init; }
        public int MaxRecursionDepth { get; init; }
        public bool Recursive { get; init; }
        public bool ReturnSpecialDirectories { get; init; }
        public string SearchPattern { get; init; }
    }
}