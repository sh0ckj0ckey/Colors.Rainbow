using System;
using Colors.Rainbow.ViewModel;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Controls;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Colors.Rainbow.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private RainbowViewModel _viewModel = null;
        private string _appVersion = string.Empty;
        public SettingsPage()
        {
            _viewModel = RainbowViewModel.Instance;
            _appVersion = GetAppVersion();
            this.InitializeComponent();
        }

        private string GetAppVersion()
        {
            try
            {
                Package package = Package.Current;
                PackageId packageId = package.Id;
                PackageVersion version = packageId.Version;
                return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
            }
            catch (Exception) { }
            return "";
        }
    }
}
