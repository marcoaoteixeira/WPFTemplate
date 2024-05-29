using Nameless.WPF.Objects;

namespace Nameless.WPF.Client.Objects {
    public sealed record SnackbarNotification : Notification {
        #region Public Properties

        public string? Title { get; init; }

        #endregion
    }
}
