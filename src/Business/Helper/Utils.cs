using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Business.Helper
{
    public class Utils
    {
        public static string GetInputFilePath()
        {
            var assemblyDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(assemblyDirectory)) return string.Empty;

            var directoryInfo = new FileInfo(assemblyDirectory).Directory;
            if (directoryInfo == null) return string.Empty;

            return string.Concat(directoryInfo.Parent.Parent.Parent.FullName, "\\docs\\");
        }

        public static string Md5Hash(string text, MD5 hasher = null)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            var sb = new StringBuilder();
            if (hasher == null) hasher = MD5.Create();
            var encodedText = Encoding.Default.GetBytes(text);

            foreach (var b in hasher.ComputeHash(encodedText))
            {
                sb.Append(b.ToString("x2").ToLower());
            }

            return sb.ToString();
        }
    }
}
