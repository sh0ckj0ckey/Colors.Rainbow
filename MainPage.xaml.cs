using System;
using System.Collections.Generic;
using System.Linq;
using Colors.Rainbow;
using Colors.Rainbow.ViewModel;
using Colors.Rainbow.Views;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.System;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace Colors.Rainbow
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private RainbowViewModel _viewModel = null;

        // 导航栏项的Tag对应的Page
        private readonly List<(string Tag, Type Page)> _vPages = new List<(string Tag, Type Page)>
        {
            ("windows", typeof(ColorsPage)),
            ("palette", typeof(PalettePage)),
            ("settings", typeof(SettingsPage)),
        };

        public MainPage()
        {
            _viewModel = RainbowViewModel.Instance;
            this.InitializeComponent();

            SetTitleBarArea();
        }

        private void SetTitleBarArea()
        {
            try
            {
                var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.ExtendViewIntoTitleBar = true;

                // 设置为可拖动区域
                Window.Current.SetTitleBar(AppTitleBarGrid);

                var titleBar = ApplicationView.GetForCurrentView().TitleBar;

                titleBar.ButtonBackgroundColor = Windows.UI.Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Windows.UI.Colors.Transparent;
                titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.Gray;

                // 当窗口激活状态改变时，注册一个handler
                Window.Current.Activated += (s, e) =>
                {
                    try
                    {
                        if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
                            AppTitleLogo.Opacity = 0.7;
                        else
                            AppTitleLogo.Opacity = 1.0;
                    }
                    catch { }
                };
            }
            catch { }
        }

        private void MainNavigationView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainFramNavigateToPage("windows", null, new Windows.UI.Xaml.Media.Animation.EntranceNavigationTransitionInfo());

                // 处理系统的返回键
                Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += CoreDispatcher_AcceleratorKeyActivated;
                Window.Current.CoreWindow.PointerPressed += CoreWindow_PointerPressed;
                SystemNavigationManager.GetForCurrentView().BackRequested += System_BackRequested;
            }
            catch { }
        }

        private void MainNavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            try
            {
                if (args?.InvokedItemContainer?.Tag == null || string.IsNullOrWhiteSpace(args?.InvokedItemContainer?.Tag?.ToString())) return;

                if (args.InvokedItemContainer != null)
                {
                    var navItemTag = args.InvokedItemContainer.Tag.ToString();
                    MainFramNavigateToPage(navItemTag, args.RecommendedNavigationTransitionInfo);
                }

                // 清除返回
                MainFrame.BackStack.Clear();
                MainFrame.ForwardStack.Clear();
            }
            catch { }
        }

        private void MainFramNavigateToPage(string navItemTag, object parameter = null, Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transitionInfo = null)
        {
            try
            {
                Type page = null;

                var item = _vPages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                page = item.Page;

                var preNavPageType = MainFrame.CurrentSourcePageType;
                if (!(page is null) && !Type.Equals(preNavPageType, page))
                {
                    if (parameter != null || transitionInfo != null)
                    {
                        MainFrame.Navigate(page, parameter, transitionInfo);
                    }
                    else
                    {
                        MainFrame.Navigate(page);
                    }
                }
            }
            catch { }
        }

        #region 返回

        private void CoreDispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
        {
            // When Alt+Left are pressed navigate back
            if (e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown
                && e.VirtualKey == VirtualKey.Left
                && e.KeyStatus.IsMenuKeyDown == true
                && !e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void System_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = TryGoBack();
            }
        }

        private void CoreWindow_PointerPressed(CoreWindow sender, PointerEventArgs e)
        {
            // Handle mouse back button.
            if (e.CurrentPoint.Properties.IsXButton1Pressed)
            {
                e.Handled = TryGoBack();
            }
        }

        private bool TryGoBack()
        {
            try
            {
                if (!MainFrame.CanGoBack)
                    return false;

                // Don't go back if the nav pane is overlayed.
                if (MainNavigationView.IsPaneOpen &&
                    (MainNavigationView.DisplayMode == Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Compact ||
                     MainNavigationView.DisplayMode == Microsoft.UI.Xaml.Controls.NavigationViewDisplayMode.Minimal))
                    return false;

                MainFrame.GoBack();
                return true;
            }
            catch { }
            return false;
        }

        #endregion

    }
}
