﻿using System.IO;
using System.Reflection;

namespace Nameless.WPF {
    public static class AssemblyExtension {
        #region Public Static Methods

        /// <summary>
        /// Retrieves the assembly directory path.
        /// </summary>
        /// <param name="self">The current assembly.</param>
        /// <param name="combineWith">Parts to concatenate with the result directory path.</param>
        /// <returns>The path to the assembly folder.</returns>
        /// <exception cref="NullReferenceException">if <paramref name="self"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">if <paramref name="combineWith"/> is <c>null</c>.</exception>
        public static string GetDirectoryPath(this Assembly self, params string[] combineWith) {
            var location = $"file://{self.Location}";
            var uri = new UriBuilder(location);
            var path = Uri.UnescapeDataString(uri.Path);

            var result = Path.GetDirectoryName(path)!;

            return combineWith.Length > 0
                ? Path.Combine(combineWith.Prepend(result).ToArray())
                : result;
        }

        #endregion
    }
}
