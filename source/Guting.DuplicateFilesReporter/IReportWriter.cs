namespace Guting.DuplicateFilesReporter
{
    public interface IReportWriter
    {
        ValueTask Begin();
        ValueTask Close();

        ValueTask WriteLine(params string[] values);

        ValueTask StartReport(string reportName);
        ValueTask EndReport();

        ValueTask StartTable(params ReportColumn[] columns);
        ValueTask EndTable();
    }
}