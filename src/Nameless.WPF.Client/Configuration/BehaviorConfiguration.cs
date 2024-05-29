namespace Nameless.WPF.Client.Configuration {
    public sealed class BehaviorConfiguration {
        #region Public Static Read-Only Properties

        public static BehaviorConfiguration Default => new();

        #endregion

        #region Public Properties

        public bool ConfirmBeforeExit { get; set; } = true;

        #endregion
    }
}
