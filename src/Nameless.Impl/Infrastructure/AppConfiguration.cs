using Wpf.Ui.Appearance;

namespace Nameless.Impl.Infrastructure {
    public sealed class AppConfiguration {
        #region Public Static Read-Only Properties

        public static AppConfiguration Default => new () {
            Theme = ApplicationTheme.Light,
            ConfirmBeforeExit = true
        };

        #endregion

        #region Public Properties

        public ApplicationTheme Theme { get; set; } = ApplicationTheme.Light;
        public bool ConfirmBeforeExit { get; set; }

        #endregion
    }
}
