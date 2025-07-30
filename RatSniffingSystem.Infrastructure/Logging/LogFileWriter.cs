using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Infrastructure.Logging
{
    public class LogFileWriter : ILogFileWriter
    {
        private readonly string _baseDirectory;

        public LogFileWriter(string baseDirectory)
        {
            _baseDirectory = baseDirectory;
            Directory.CreateDirectory(_baseDirectory);
        }

        public async Task WriteLineAsync(string fileName, string line)
        {
            var path = Path.Combine(_baseDirectory, fileName);
            await File.AppendAllTextAsync(path, line + Environment.NewLine, Encoding.UTF8);
        }

        public async Task OverwriteAsync(string fileName, IEnumerable<string> lines)
        {
            var path = Path.Combine(_baseDirectory, fileName);
            var content = string.Join(Environment.NewLine, lines);
            await File.WriteAllTextAsync(path, content, Encoding.UTF8);
        }
    }
}