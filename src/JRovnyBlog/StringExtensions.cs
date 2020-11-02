using System;
using System.Security.Cryptography;
using System.Text;

namespace JRovnyBlog
{
    public static class StringExtensions
    {
        public static string MD5Hash(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;

            using (var hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(source);
                var hashBytes = hash.ComputeHash(sourceBytes);
                return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
            }
        }
    }
}