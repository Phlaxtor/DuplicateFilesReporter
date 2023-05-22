using System.Security.Cryptography;

namespace Guting.DuplicateFilesReporter
{
    public sealed class FoundFile
    {
        private readonly FileInfo _file;
        private readonly Settings _settings;
        private byte[]? _hash;
        private string? _hashHex;
        private string? _type;

        public FoundFile(FileInfo file, Settings settings)
        {
            _file = file;
            _settings = settings;
        }

        public DateTime Accessed => _file.LastAccessTime;
        public DateTime Created => _file.CreationTime;
        public string FilePath => _file.FullName;
        public string Name => _file.Name;
        public long Size => _file.Length;
        public string Type => GetFileType();
        public DateTime Updated => _file.LastWriteTime;

        public async ValueTask<bool> EqualsAsync(FoundFile? other, CancellationToken cancellationToken)
        {
            if (other == null) return false;
            if (Size != other.Size) return false;
            byte[] thisHash = await GetHash(cancellationToken);
            byte[] otherHash = await other.GetHash(cancellationToken);
            return Equals(thisHash, otherHash);
        }

        public async Task<byte[]> GetHash(CancellationToken cancellationToken)
        {
            if (_hash != null) return _hash;
            using Stream stream = _file.OpenRead();
            using HashAlgorithm? hasher = HashAlgorithm.Create(_settings.HashName);
            if (hasher == null) throw new NotSupportedException($"Not supported value hash function '{_settings.HashName}'");
            _hash = await hasher.ComputeHashAsync(stream, cancellationToken);
            return _hash;
        }

        public async Task<string> GetHashHex(CancellationToken cancellationToken)
        {
            if (_hashHex != null) return _hashHex;
            byte[] hash = await GetHash(cancellationToken);
            _hashHex = Convert.ToHexString(hash);
            return _hashHex;
        }

        private bool Equals(byte[] a, byte[] b)
        {
            Span<byte> a1 = new Span<byte>(a);
            Span<byte> b1 = new Span<byte>(b);
            return a1.SequenceEqual(b1);
        }

        private string GetFileType()
        {
            if (_type != null) return _type;
            _type = _file.GetFileType();
            return _type;
        }
    }
}