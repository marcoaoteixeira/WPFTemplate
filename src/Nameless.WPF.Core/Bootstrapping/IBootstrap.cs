namespace Nameless.WPF.Bootstrapping {
    public interface IBootstrap {
        #region Methods

        Task RunAsync(CancellationToken cancellationToken);

        #endregion
    }
}
