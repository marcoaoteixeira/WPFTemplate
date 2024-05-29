using System.Diagnostics;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Nameless.WPF.Client.Configuration;
using Nameless.WPF.Configuration;
using Nameless.WPF.Impl.Configuration;
using Wpf.Ui.Appearance;

namespace Nameless.WPF.Client.ViewModels.Pages
{
    public partial class AppConfigurationViewModel : ObservableObject {
        #region Private Read-Only Fields

        private readonly IApplicationContext _applicationContext;
        private readonly IConfigurationProvider<RootConfiguration> _configurationManager;

        #endregion

        #region Private Fields

        private bool _initialized;

        #endregion

        #region Private Fields for Observables

        [ObservableProperty]
        private ApplicationTheme _currentTheme = ApplicationTheme.Light;

        [ObservableProperty]
        private bool _confirmBeforeExit = true;

        #endregion

        #region Public Properties

        public ApplicationTheme[] AvailableThemes { get; } = [
            ApplicationTheme.Light,
            ApplicationTheme.Dark,
            ApplicationTheme.HighContrast
        ];

        #endregion

        #region Public Constructors

        public AppConfigurationViewModel(IApplicationContext applicationContext, IConfigurationProvider<RootConfiguration> configurationManager) {
            ArgumentNullException.ThrowIfNull(applicationContext, nameof(applicationContext));
            ArgumentNullException.ThrowIfNull(configurationManager, nameof(configurationManager));

            _applicationContext = applicationContext;
            _configurationManager = configurationManager;

            Initialize();
        }

        #endregion

        #region Private Methods

        private void Initialize() {
            if (_initialized) { return; }

            
            ConfirmBeforeExit = _configurationManager.Current.Behavior.ConfirmBeforeExit;
            CurrentTheme = _configurationManager.Current.Appearance.Theme;

            _initialized = true;
        }

        partial void OnCurrentThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue) {
            if (oldValue == newValue) { return; }

            _configurationManager.Current.Appearance.Theme = newValue;

            ApplicationThemeManager.Apply(newValue);
        }

        partial void OnConfirmBeforeExitChanged(bool oldValue, bool newValue) {
            if (oldValue == newValue) { return; }

            _configurationManager.Current.Behavior.ConfirmBeforeExit = newValue;
        }

        #endregion

        #region Private Methods for Commands

        [RelayCommand]
        private Task OpenAppDataDirectoryAsync() {
            using var process = Process.Start(fileName: Root.App.EXPLORER,
                                              arguments: _applicationContext.AppDataDirectory);

            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task OpenAppLogFileAsync() {
            var logFilePath = Path.Combine(typeof(Root).Assembly.GetDirectoryPath(Root.App.LOG_FILE_NAME));
            using var process = Process.Start(fileName: Root.App.TEXT_EDITOR,
                                              arguments: logFilePath);

            return Task.CompletedTask;
        }

        #endregion
    }
}
