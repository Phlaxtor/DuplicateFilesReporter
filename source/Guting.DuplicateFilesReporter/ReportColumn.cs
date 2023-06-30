namespace Guting.DuplicateFilesReporter
{
    public readonly struct ReportColumn
    {
        public ReportColumn(string name, ReportColumnType type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public ReportColumnType Type { get; }
    }
}