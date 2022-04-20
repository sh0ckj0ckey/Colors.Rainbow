using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class WindowsColors : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private SeeColors.ViewModel.RainbowViewModel _viewModel = null;
        SeeColors.ViewModel.RainbowViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel != value)
                {
                    _viewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public static WindowsColors Current;

        //VM存储所有的颜色，不会改变，ColorsList存储的是GridView要显示的颜色
        public ObservableCollection<WindowsColor> ColorsList = new ObservableCollection<WindowsColor>();
        public WindowsColor SelectedColor;

        public WindowsColors()
        {
            this.InitializeComponent();
            Current = this;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;

            ViewModel = SeeColors.ViewModel.RainbowViewModel.Instance;

            if (MainPage.SettingContainer.Values["language"] == null || MainPage.SettingContainer.Values["language"].ToString() == "en_US")
                Switch2EN();
            else
                Switch2CN();

            if (MainPage.SettingContainer.Values["theme"] == null || MainPage.SettingContainer.Values["theme"].ToString() == "light")
                Switch2Light();
            else
                Switch2Dark();

            foreach (var item in ViewModel.vWindowsColors)
            {
                ColorsList.Add(item);
            }

            SharedShadow.Receivers.Add(BackgroundGrid);
            InfoColorGrid.Translation += new System.Numerics.Vector3(0, 0, 16);
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (InfoGrid.Visibility == Visibility.Visible)
            {
                HideGridScale.Begin();
            }
        }

        private void Switch2CN()
        {
            searchBox.PlaceholderText = "寻找一个颜色";
            notFound.Text = "没有找到这个颜色";
        }
        private void Switch2EN()
        {
            searchBox.PlaceholderText = "Find a color";
            notFound.Text = "Color not found";
        }
        private void Switch2Dark()
        {
            this.RequestedTheme = ElementTheme.Dark;
        }
        private void Switch2Light()
        {
            this.RequestedTheme = ElementTheme.Light;
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
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {
                SelectedColor = ColorsList[listView.SelectedIndex];
                ShowInfo();
            }
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridView.SelectedIndex != -1)
            {
                SelectedColor = ColorsList[gridView.SelectedIndex];
                ShowInfo();
            }
        }


        /// <summary>
        /// 关闭详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            HideGridScale.Begin();
        }

        private void HideGridScale_Completed(object sender, object e)
        {
            gridView.IsEnabled = true;
            listView.IsEnabled = true;
            searchBox.IsEnabled = true;
            InfoGrid.Visibility = Visibility.Collapsed;
            CopySuccessTextBlock.Visibility = Visibility.Collapsed;
            CopyFailedTextBlock.Visibility = Visibility.Collapsed;
            gridView.SelectedIndex = -1;
            listView.SelectedIndex = -1;
        }


        /// <summary>
        /// 显示详情颜色
        /// </summary>
        public void ShowInfo()
        {
            SolidColorBrush solidColorBrush = new SolidColorBrush(SelectedColor.VisualColor);
            InfoColor1Ellipse.Fill = solidColorBrush;
            InfoColor2Ellipse.Fill = solidColorBrush;
            InfoColor3Ellipse.Fill = solidColorBrush;
            InfoNameTextBlock.Text = SelectedColor.Name;
            double total = SelectedColor.RValue + SelectedColor.GValue + SelectedColor.BValue;
            if (total > 0)
            {
                RGrid.Width = (SelectedColor.RValue / total) * RGBBarGrid.Width;
                GGrid.Width = (SelectedColor.GValue / total) * RGBBarGrid.Width;
                BGrid.Width = (SelectedColor.BValue / total) * RGBBarGrid.Width;
            }
            else
            {
                RGrid.Width = 16;
                GGrid.Width = 16;
                BGrid.Width = 16;
            }
            RTextBlock.Text = SelectedColor.RValue.ToString();
            GTextBlock.Text = SelectedColor.GValue.ToString();
            BTextBlock.Text = SelectedColor.BValue.ToString();
            HEXRichEditBox.IsReadOnly = false;
            HEXRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, SelectedColor.Hex);
            HEXRichEditBox.IsReadOnly = true;
            InfoGrid.Visibility = Visibility.Visible;
            ShowGridScale.Begin();
            gridView.IsEnabled = false;
            listView.IsEnabled = false;
            searchBox.IsEnabled = false;
        }

        /// <summary>
        /// 复制hex按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.ApplicationModel.DataTransfer.DataPackage dataPackage = new Windows.ApplicationModel.DataTransfer.DataPackage();
                dataPackage.SetText(SelectedColor.Hex);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(dataPackage);
                CopyFailedTextBlock.Visibility = Visibility.Collapsed;
                CopySuccessTextBlock.Visibility = Visibility.Visible;
                ShowSuccess.Begin();
            }
            catch
            {
                CopySuccessTextBlock.Visibility = Visibility.Collapsed;
                CopyFailedTextBlock.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 点击窗口旁的区域关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HideGridScale.Begin();
        }

        // 搜索颜色
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var filteredColor = ViewModel.vWindowsColors.Where(p => p.Name.StartsWith(searchBox.Text, StringComparison.CurrentCultureIgnoreCase)).ToList();
                ColorsList.Clear();
                filteredColor.ForEach(p => ColorsList.Add(p));
                if (ColorsList.Count <= 0)
                {
                    notFound.Visibility = Visibility.Visible;
                }
                else
                {
                    notFound.Visibility = Visibility.Collapsed;
                }
            }
            catch { }
        }
    }
}
