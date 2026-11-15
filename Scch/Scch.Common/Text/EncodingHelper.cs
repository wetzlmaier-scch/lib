using System;
using System.Text;

namespace Scch.Common.Text
{
    /// <summary>
    /// Helper functions for <see cref="Encoding"/>.
    /// </summary>
    public static class EncodingHelper
    {
        private static readonly Lazy<Encoding> _ansi = new Lazy<Encoding>(
            () => CodePagesEncodingProvider.Instance.GetEncoding(0) ?? Encoding.Default);

        /// <summary>
        /// Gets the encoding of the system's active ANSI code page.
        /// </summary>
        /// <remarks>
        /// This is what <see cref="Encoding.Default"/> returned on the .NET Framework. On .NET,
        /// <see cref="Encoding.Default"/> is always UTF-8, which would change the bytes produced for non-ASCII text.
        /// </remarks>
        public static Encoding Ansi
        {
            get { return _ansi.Value; }
        }
    }
}
