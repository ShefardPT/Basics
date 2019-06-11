using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Basics
{
    public class Sanitizer
    {
        private static readonly char[] _unsafeChars = { '"', '<', '>', '#', '%', '{', '}', '|', '\\', '^', '~', '[', ']', '`', ' ' };

        private static readonly char[] _reservedChars = { ';', '/', '?', ':', '@', '=', '&' };

        public static string SanitizeURL(string str)
        {
            str = str.Trim().ToLowerInvariant();

            str = ReplaceMultipleWhitespacesByDash(str);
            str = RemoveUnsafeCharacters(str);

            return str;
        }

        private static string ReplaceMultipleWhitespacesByDash(string str)
        {
            //return Regex.Replace(str, @"\s+", "-");

            var builder = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsWhiteSpace(str[i]))
                {
                    if (!char.IsWhiteSpace(str[i - 1]))
                    {
                        builder.Append('-'); 
                    }

                    continue;
                }

                builder.Append(str[i]);
            }

            return builder.ToString();
        }

        private static string RemoveUnsafeCharacters(string str)
        {
            var builder = new StringBuilder();

            foreach (var c in str)
            {
                if (_unsafeChars.All(ch => ch != c) &&
                    _reservedChars.All(ch => ch != c))
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
    }
}
