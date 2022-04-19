using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class WindowsColors : Page
    {
        public static WindowsColors Current;

        //AllColors存储所有的颜色，不会改变，ColorsList存储的是GridView要显示的颜色
        public ObservableCollection<WindowsColor> ColorsList = new ObservableCollection<WindowsColor>();
        public ObservableCollection<WindowsColor> AllColors = null;
        private List<String> suggestions = new List<string>();
        public WindowsColor selecetedColor;

        public WindowsColors()
        {
            this.InitializeComponent();
            Current = this;
            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            if (MainPage.SettingContainer.Values["language"] == null || MainPage.SettingContainer.Values["language"].ToString() == "en_US")
            {
                Switch2EN();
            }
            else
            {
                Switch2CN();
            }
            if (MainPage.SettingContainer.Values["theme"] == null || MainPage.SettingContainer.Values["theme"].ToString() == "light")
            {
                Switch2Light();
            }
            else
            {
                Switch2Dark();
            }
            AllColors = GetCollectionAll();
            foreach (var item in AllColors)
            {
                ColorsList.Add(item);
            }

        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (InfoGrid.Visibility==Visibility.Visible)
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

        /// <summary>
        /// 加载所有的颜色到集合中
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<WindowsColor> GetCollectionAll()
        {
            if (AllColors == null)
            {
                AllColors = new ObservableCollection<WindowsColor>();
                Type t = typeof(Colors);
                PropertyInfo[] properties = t.GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    string method = "get_" + property.Name;
                    MethodInfo get_method = t.GetMethod(method);
                    Color getColor = (Color)get_method.Invoke(this, null);
                    WindowsColor ec = new WindowsColor
                    {
                        Name = property.Name,
                        RValue = getColor.R,
                        GValue = getColor.G,
                        BValue = getColor.B,
                        Hex = getColor.ToString(),
                        VisualColor = getColor
                    };
                    AllColors.Add(ec);
                }
            }
            return AllColors;
        }

        /// <summary>
        /// 开始搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void searchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            suggestions.Clear();
            suggestions = AllColors.Where(p => p.Name.StartsWith(sender.Text, StringComparison.CurrentCultureIgnoreCase)).Select(p => p.Name).ToList();
            searchBox.ItemsSource = suggestions;
            var filteredColor = AllColors.Where(p => p.Name.StartsWith(searchBox.Text, StringComparison.CurrentCultureIgnoreCase)).ToList();
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
        private void searchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            var filteredColor = AllColors.Where(p => p.Name.StartsWith(searchBox.Text, StringComparison.CurrentCultureIgnoreCase)).ToList();
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

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView.SelectedIndex != -1)
            {
                selecetedColor = ColorsList[listView.SelectedIndex];
                ShowInfo();
            }
        }

        private void gridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gridView.SelectedIndex != -1)
            {
                selecetedColor = ColorsList[gridView.SelectedIndex];
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
            SolidColorBrush solidColorBrush = new SolidColorBrush(selecetedColor.VisualColor);
            InfoColor1Ellipse.Fill = solidColorBrush;
            InfoColor2Ellipse.Fill = solidColorBrush;
            InfoColor3Ellipse.Fill = solidColorBrush;
            InfoColorGrid.BorderBrush = solidColorBrush;
            InfoNameTextBlock.Text = selecetedColor.Name;
            RGrid.Width = (selecetedColor.RValue / (selecetedColor.RValue + selecetedColor.GValue + selecetedColor.BValue)) * RGBBarGrid.Width;
            GGrid.Width = (selecetedColor.GValue / (selecetedColor.RValue + selecetedColor.GValue + selecetedColor.BValue)) * RGBBarGrid.Width;
            BGrid.Width = (selecetedColor.BValue / (selecetedColor.RValue + selecetedColor.GValue + selecetedColor.BValue)) * RGBBarGrid.Width;
            RTextBlock.Text = selecetedColor.RValue.ToString();
            GTextBlock.Text = selecetedColor.GValue.ToString();
            BTextBlock.Text = selecetedColor.BValue.ToString();
            HEXRichEditBox.IsReadOnly = false;
            HEXRichEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.None, selecetedColor.Hex);
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
                dataPackage.SetText(selecetedColor.Hex);
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
        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            HideGridScale.Begin();
        }
    }
}
