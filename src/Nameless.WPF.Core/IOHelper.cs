﻿using System.IO;

namespace Nameless.WPF {
    public static class IOHelper {
        #region Public Static Methods

        public static void EnsureDirectory(string directoryPath) {
            if (!Directory.Exists(directoryPath)) {
                Directory.CreateDirectory(directoryPath);
            }
        }

        #endregion
    }
}
