using Prism.Windows.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AdventureWorks.Shopper.Views
{
    public sealed partial class CheckoutHubPage : NavigationAwarePage
    {
        public CheckoutHubPage()
        {
#if WINDOWS_APP
            this.TopAppBar = new AppBar
            {
                Style = (Style)App.Current.Resources["AppBarStyle"],
                Content = new TopAppBarUserControl()
            };
            // x:Uid="TopAppBar"
#endif
            this.InitializeComponent();
        }
    }
}
