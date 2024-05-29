using Nameless.WPF.Configuration;

namespace Nameless.WPF.Client.Configuration {
    public class RootConfiguration : IRootConfiguration {
        #region Public Static Read-Only Properties

        public static RootConfiguration Default => new();

        #endregion

        #region Public Properties

        public AppearanceConfiguration Appearance { get; set; } = AppearanceConfiguration.Default;
        public BehaviorConfiguration Behavior { get; set; } = BehaviorConfiguration.Default;

        #endregion
    }
}
