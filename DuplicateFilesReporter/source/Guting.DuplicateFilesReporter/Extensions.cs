namespace Guting.DuplicateFilesReporter
{
    public static class Extensions
    {
        public static string GetFileType(this FileInfo file)
        {
            if (file.Extension.Length == file.Name.Length) return ReservedWord.NoExtensionType;
            return file.Extension.GetFileType();
        }

        public static string GetFileType(this string extension)
        {
            extension = extension.TrimStart('.');
            if (string.IsNullOrEmpty(extension)) return ReservedWord.NoExtensionType;
            extension = extension.ToUpper();
            return extension;
        }
    }
}