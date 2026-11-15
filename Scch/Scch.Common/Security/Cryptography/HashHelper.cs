using System;
using System.IO;
using System.Security.Cryptography;
using Scch.Common.Text;

namespace Scch.Common.Security.Cryptography;

public static class HashHelper
{
    public const string FileExtension = ".md5";

    public static string CalculateFileMD5(string filename)
    {
        using (var stream = File.OpenRead(filename))
        {
            return CalculateMD5(stream);
        }
    }

    public static string CalculateMD5(string content)
    {
        using (var stream = new MemoryStream(EncodingHelper.Ansi.GetBytes(content)))
        {
            return CalculateMD5(stream);
        }
    }

    public static string CalculateMD5(Stream stream)
    {
        using (var md5 = MD5.Create())
        {
            return Convert.ToBase64String(md5.ComputeHash(stream), Base64FormattingOptions.None);
        }
    }
}
