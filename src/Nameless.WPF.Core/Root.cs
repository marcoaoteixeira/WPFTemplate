using System.Text;

namespace Nameless.WPF {
    /// <summary>
    /// This class was defined to be an entrypoint for this project assembly.
    /// 
    /// *** DO NOT IMPLEMENT ANYTHING HERE ***
    /// 
    /// But, it's OK to use this class for all constants or default values
    /// that you'll use throughout this project.
    /// </summary>
    public static class Root {
        #region Public Static Inner Classes

        public static class Defaults {
            #region Public Static Read-Only Properties

            public static Encoding Encoding { get; } = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

            #endregion
        }

        public static class Constants {
            #region Public Constants

            public const string OPTIONS_TOKEN = "Options";

            #endregion
        }

        #endregion
    }
}
