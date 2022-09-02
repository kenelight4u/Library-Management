using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LM.Application.Helpers
{
    public static class StringExtensions
    {
        /// <summary>
        /// Checks whatever given collection object is null or has no item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified source]; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// Gets a substring of a string from beginning of the string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="len">The length.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="ArgumentNullException">str</exception>
        /// <exception cref="ArgumentException">len argument can not be bigger than given string's length!</exception>
        public static string Left(this string str, int len)
        {
            if (str == null) throw new ArgumentNullException("str");

            if (str.Length < len)
                throw new ArgumentException("len argument can not be bigger than given string's length!");

            return str.Substring(0, len);
        }

        /// <summary>
        /// Removes first occurrence of the given postfixes from end of the given string.
        /// Ordering is important. If one of the postFixes is matched, others will not be tested.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="postFixes">one or more postfix.</param>
        /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str == null) return null;

            if (str == string.Empty) return string.Empty;

            if (postFixes.IsNullOrEmpty()) return str;

            foreach (var postFix in postFixes)
                if (str.EndsWith(postFix))
                    return str.Left(str.Length - postFix.Length);

            return str;
        }
    }
}
