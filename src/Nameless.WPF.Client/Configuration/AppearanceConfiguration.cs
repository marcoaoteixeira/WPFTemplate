using Wpf.Ui.Appearance;

namespace Nameless.WPF.Client.Configuration {
    public sealed class AppearanceConfiguration {
        #region Public Static Read-Only Properties

        public static AppearanceConfiguration Default => new();

        #endregion

        #region Public Properties

        public ApplicationTheme Theme { get; set; } = ApplicationTheme.Light;

        #endregion
    }
}
