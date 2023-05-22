using Guting.DuplicateFilesReporter;

namespace DuplicateFilesReporter
{
    internal class Program
    {
        private static readonly CancellationTokenSource _source = new CancellationTokenSource();

        private static void Cancel(object? sender, ConsoleCancelEventArgs e)
        {
            Cancel();
        }

        private static void Cancel()
        {
            try
            {
                if (_source.IsCancellationRequested) return;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Cancel report");
                _source.Cancel();
            }
            catch
            {
            }
        }

        private static async Task Main(string[] args)
        {
            try
            {
                Console.CancelKeyPress += Cancel;
                Settings settings = Settings.GetDefaultSettings();
                Reporter reporter = new Reporter(settings);
                reporter.AddFiles(args, _source.Token);
                await reporter.GroupFiles(_source.Token);
            }
            catch (Exception e)
            {
                var type = e.GetType();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {type.Name} {e.Message}");
                Cancel();
            }
            _source.Dispose();
        }
    }
}