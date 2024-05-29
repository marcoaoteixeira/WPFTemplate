using Microsoft.Extensions.DependencyInjection;
using Nameless.WPF.Client.ViewModels.Pages;
using Nameless.WPF.Client.ViewModels.Windows;
using Nameless.WPF.Client.Views.Pages;
using Nameless.WPF.Client.Views.Windows;
using Wpf.Ui;

namespace Nameless.WPF.Client {
    public static class ServiceCollectionExtension {
        #region Public Static Methods

        public static IServiceCollection RegisterViews(this IServiceCollection self) {
            return self
                .AddSingleton<INavigationWindow, MainWindow>()
                .AddSingleton<AppConfigurationPage>();
        }

        public static IServiceCollection RegisterViewModels(this IServiceCollection self) {
            return self
                .AddSingleton<MainViewModel>()
                .AddSingleton<AppConfigurationViewModel>();
        }

        #endregion
    }
}
