namespace Nameless.Infrastructure {
    public interface IAppConfigurationProvider<out TAppConfiguration>
        where TAppConfiguration : class, new() {
        #region Properties

        TAppConfiguration Current { get; }

        #endregion
    }
}
