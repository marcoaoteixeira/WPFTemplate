namespace Nameless.WPF.Configuration {
    public interface IConfigurationProvider<out TConfiguration>
        where TConfiguration : IRootConfiguration, new() {
        #region Properties

        TConfiguration Current { get; }

        #endregion
    }
}
