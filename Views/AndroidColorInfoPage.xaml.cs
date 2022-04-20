using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace SeeColors_UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AndroidColorInfoPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private AndroidColor _SelectedColor = null;
        AndroidColor SelectedColor
        {
            get => _SelectedColor;
            set
            {
                if (_SelectedColor != value)
                {
                    _SelectedColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public AndroidColorInfoPage()
        {
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        /// <summary>
        /// 重写导航至此页面的代码,显示动画
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Windows.UI.Xaml.Media.Animation.NavigationTransitionInfo transition)
            {
                navigationTransition.DefaultNavigationTransitionInfo = transition;
            }
            else if (e.Parameter is AndroidColor color)
            {
                SelectedColor = color;
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame != null && rootFrame.CanGoBack)
            {
                e.Handled = true;
                rootFrame.GoBack();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AndroidColorsPage));
        }

        /// <summary>
        /// 复制hex
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MDColorsGridView.ContainerFromItem(e.ClickedItem) is GridViewItem container)
            {
                Windows.ApplicationModel.DataTransfer.DataPackage dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
                dataPackage.SetText((container.Content as AndroidColorInfo).Hex);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);

                if (MainPage.SettingContainer.Values["language"] == null || MainPage.SettingContainer.Values["language"].ToString() == "en_US")
                {
                    CopySuccessTextBlock.Text = "Copy Success";
                }
                else
                {
                    CopySuccessTextBlock.Text = "复制成功";
                }
                CopySuccessGrid.Visibility = Visibility.Visible;
                ShowSuccess.Begin();
            }
        }
        private void DoubleAnimation_Completed_1(object sender, object e)
        {
            HideSuccess.Begin();
        }
        private void DoubleAnimation_Completed(object sender, object e)
        {
            CopySuccessGrid.Visibility = Visibility.Collapsed;
        }
    }
}
