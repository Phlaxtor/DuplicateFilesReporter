using System.Text;

namespace Guting.DuplicateFilesReporter
{
    public sealed class ConsoleWriter : IReportWriter, IDisposable
    {
        private readonly StreamWriter _writer;

        public ConsoleWriter(Stream output)
        {
            _writer = new StreamWriter(output, Encoding.UTF8, -1, true);
        }

        public async ValueTask Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async ValueTask New(string name)
        {
            throw new NotImplementedException();
        }

        public async ValueTask WriteLine(params string[] values)
        {
            throw new NotImplementedException();
        }
    }
}