using System.IO;
using System.Text.Json;
using Nameless.Infrastructure;

namespace Nameless.Impl.Infrastructure {
    public sealed class AppConfigurationProvider : IAppConfigurationProvider<AppConfiguration>, IDisposable {
        #region Private Read-Only Fields

        private readonly IApplicationContext _applicationContext;

        #endregion

        #region Private Fields

        private bool _disposed;

        #endregion

        #region Public Constructors

        public AppConfigurationProvider(IApplicationContext applicationContext) {
            ArgumentNullException.ThrowIfNull(applicationContext, nameof(applicationContext));

            _applicationContext = applicationContext;

            Initialize();
        }

        #endregion

        #region Destructor

        ~AppConfigurationProvider() {
            Dispose(disposing: false);
        }

        #endregion

        #region Private Methods

        private void Initialize()
            => Current = FetchFromFileSystem();

        private AppConfiguration FetchFromFileSystem() {
            var file = _applicationContext.FileProvider.GetFileInfo(Root.Constants.APP_CONFIG_FILE_NAME);

            if (!file.Exists) { return AppConfiguration.Default; }

            using var fileStream = file.CreateReadStream();
            using var streamReader = new StreamReader(fileStream);
            var secret = streamReader.ReadToEnd();
            var json = CryptoHelper.Decrypt(secret);

            return JsonSerializer.Deserialize<AppConfiguration>(json)
                ?? AppConfiguration.Default;
        }

        private void StoreIntoFileSystem(AppConfiguration appConfiguration) {
            var file = _applicationContext.FileProvider.GetFileInfo(Root.Constants.APP_CONFIG_FILE_NAME);
            var json = JsonSerializer.Serialize(appConfiguration);

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

        #region IAppConfigurationProvider<AppConfiguration> Members

        public AppConfiguration Current { get; private set; } = AppConfiguration.Default;

        #endregion

        #region IDisposable Members

        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
