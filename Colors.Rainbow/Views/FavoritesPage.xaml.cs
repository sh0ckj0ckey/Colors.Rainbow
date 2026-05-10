using Colors.Rainbow.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Colors.Rainbow.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FavoritesPage : Page
    {
        private RainbowViewModel _viewModel = null;
        public FavoritesPage()
        {
            _viewModel = RainbowViewModel.Instance;
            this.InitializeComponent();
        }

        private void OnClickCopy(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item && item.Tag != null)
            {
                string hex = item.Tag.ToString();

                Windows.ApplicationModel.DataTransfer.DataPackage dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
                dataPackage.SetText(hex);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
            }
        }

        private void OnClickDelete(object sender, RoutedEventArgs e)
        {
            if (sender is MenuFlyoutItem item && item.Tag != null)
            {
                string hex = item.Tag.ToString();
                RainbowViewModel.Instance.RemoveFavorite(hex);
            }
        }
    }
}
