using Nameless.WPF.Client.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Nameless.WPF.Client.Views.Pages {
    public partial class AppConfigurationPage : INavigationAware {
        #region Public Properties

        public AppConfigurationViewModel ViewModel { get; }

        #endregion

        #region Public Constructors

        public AppConfigurationPage(AppConfigurationViewModel viewModel) {
            ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

            ViewModel = viewModel;

            InitializeComponent();
        }

        #endregion

        #region INavigationAware Members

        public void OnNavigatedTo() { }

        public void OnNavigatedFrom() { }

        #endregion
    }
}
