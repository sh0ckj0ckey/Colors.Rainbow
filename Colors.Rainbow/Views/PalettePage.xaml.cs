using Colors.Rainbow.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Colors.Rainbow.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PalettePage : Page
    {
        private RainbowViewModel _viewModel = null;
        public PalettePage()
        {
            _viewModel = RainbowViewModel.Instance;
            this.InitializeComponent();
        }

        private void FavButton_Click(object sender, RoutedEventArgs e)
        {
            if (RainbowViewModel.Instance.CheckFavorite(Palette.Color.ToString()))
            {
                RainbowViewModel.Instance.RemoveFavorite(Palette.Color.ToString());
                FavoriteIcon.Visibility = Visibility.Collapsed;
            }
            else
            {
                RainbowViewModel.Instance.AddFavorite(Palette.Color.ToString());
                FavoriteIcon.Visibility = Visibility.Visible;
            }
        }
    }
}
