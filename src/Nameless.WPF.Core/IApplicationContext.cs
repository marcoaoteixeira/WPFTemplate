using Microsoft.Extensions.FileProviders;

namespace Nameless.WPF {
    public interface IApplicationContext {
        #region Properties

        string AppName { get; }
        Version AppVersion { get; }
        string AppDataDirectory { get; }
        IFileProvider FileProvider { get; }

        #endregion
    }
}
