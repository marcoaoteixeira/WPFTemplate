using System.IO;
using System.Text.Json;
using Nameless.WPF.Configuration;

namespace Nameless.WPF.Impl.Configuration {
    public sealed class ConfigurationProvider<TConfiguration> : IConfigurationProvider<TConfiguration>, IDisposable
        where TConfiguration : IRootConfiguration, new() {
        #region Private Read-Only Fields

        private readonly IApplicationContext _applicationContext;
        private readonly string _configurationFileName;

        #endregion

        #region Private Fields

        private bool _disposed;

        #endregion

        #region Public Constructors

        public ConfigurationProvider(IApplicationContext applicationContext, string configurationFileName = Root.Constants.APP_CONFIG_FILE_NAME) {
            ArgumentNullException.ThrowIfNull(applicationContext, nameof(applicationContext));
            ArgumentException.ThrowIfNullOrWhiteSpace(configurationFileName, nameof(configurationFileName));

            _applicationContext = applicationContext;
            _configurationFileName = configurationFileName;

            Initialize();
        }

        #endregion

        #region Destructor

        ~ConfigurationProvider() {
            Dispose(disposing: false);
        }

        #endregion

        #region Private Methods

        private void Initialize()
            => Current = FetchFromFileSystem();

        private TConfiguration FetchFromFileSystem() {
            var file = _applicationContext.FileProvider.GetFileInfo(_configurationFileName);

            if (!file.Exists) { return new TConfiguration(); }

            using var fileStream = file.CreateReadStream();
            using var streamReader = new StreamReader(fileStream);
            var secret = streamReader.ReadToEnd();
            var json = CryptoHelper.Decrypt(secret);

            return JsonSerializer.Deserialize<TConfiguration>(json)
                ?? new TConfiguration();
        }

        private void StoreIntoFileSystem(TConfiguration configuration) {
            var file = _applicationContext.FileProvider.GetFileInfo(_configurationFileName);
            var json = JsonSerializer.Serialize(configuration);

            if (string.IsNullOrWhiteSpace(file.PhysicalPath)) {
                return;
            }

            using var fileStream = new FileStream(file.PhysicalPath, FileMode.Create, FileAccess.Write);
            using var streamWriter = new StreamWriter(fileStream);
            var secret = CryptoHelper.Encrypt(json);

            streamWriter.Write(secret);
        }

        private void Dispose(bool disposing) {
            if (_disposed) { return; }

            if (disposing) {
                StoreIntoFileSystem(Current);
            }

            _disposed = true;
        }

        #endregion

        #region IConfigurationProvider<TConfiguration> Members

        public TConfiguration Current { get; private set; } = new();

        #endregion

        #region IDisposable Members

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
