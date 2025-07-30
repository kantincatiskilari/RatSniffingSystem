using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Infrastructure.Logging
{
    public interface ILogFileWriter
    {
        /// <summary>
        /// Belirtilen dosya adına bir satır ekler.
        /// </summary>
        Task WriteLineAsync(string fileName, string line);

        /// <summary>
        /// Yeni bir dosya oluşturur veya varsa üzerine yazar.
        /// </summary>
        Task OverwriteAsync(string fileName, IEnumerable<string> lines);
    }
}
