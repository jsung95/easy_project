using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasyProject
{
    /// <summary>
    /// TabPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TabPage : Page
    {
        private string CurrentButtonName = "StatusPageTabButton";

        public TabPage()
        {
            InitializeComponent();
            userNameTxtBox.Text = App.nurse_dto.Nurse_name;
            this.Loaded += PageLoaded;
        }
        /*private void StatusBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                 (
                 new Uri("/View/TabItemPage/StatusPage.xaml", UriKind.Relative) //재고현황화면 --테스트
                 );

        }*/
        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (App.nurse_dto.Nurse_auth.Equals("NORMAL"))
            {
                InsertPageTabButton.Width = 0;
                AuthorityPageTabButton.Width = 0;
            }
            else if (App.nurse_dto.Nurse_auth.Equals("ADMIN"))
            {
                AuthorityPageTabButton.Width = 0;
            }
            else if (App.nurse_dto.Nurse_auth.Equals("SUPER"))
            {
                InsertPageTabButton.Width = 0;
                IncomingOutgoingPageTabButton.Width = 0;
                InsertPageTabButton.Width = 0;
                OrderPageTabButton.Width = 0;
            }
        }

        private void btn_close(object sender, RoutedEventArgs e)       //버튼 창닫기
        {
            Window.GetWindow(this).Close();
        }

        private void TabButtonClick(object sender, RoutedEventArgs e)       //버튼 창닫기
        {
            string buttonName = ((Button)e.Source).Name;
            Button currentButton = (Button)this.FindName(buttonName);

            //GridCursor.Margin = new Thickness((150 * index), 0, 0, 0);
            if (!buttonName.Equals(CurrentButtonName))
            {
                var previousButton = (Button)this.FindName(CurrentButtonName);
                previousButton.Background = null;
            }

            BrushConverter bc = new BrushConverter();

            switch (buttonName)
            {
                case "StatusPageTabButton":
                    TabFrame.Source = new Uri("TabItemPage/StatusPage.xaml", UriKind.Relative);
                    break;
                case "GraphPageTabButton":
                    TabFrame.Source = new Uri("TabItemPage/GraphTabPage.xaml", UriKind.Relative);
                    break;
                case "InsertPageTabButton":
                    TabFrame.Source = new Uri("TabItemPage/InsertPage.xaml", UriKind.Relative);
                    break;
                case "IncomingOutgoingPageTabButton":
                    TabFrame.Source = new Uri("TabItemPage/IncomingOutgoingPageBtn.xaml", UriKind.Relative);
                    break;
                case "OrderPageTabButton":
                    TabFrame.Source = new Uri("TabItemPage/OrderPage.xaml", UriKind.Relative);
                    break;
                case "AuthorityPageTabButton":
                    TabFrame.Source = new Uri("TabItemPage/AuthorityPage.xaml", UriKind.Relative);
                    break;

            }

            currentButton.Background = (Brush)bc.ConvertFrom("#F0EBE9");
            CurrentButtonName = buttonName;
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/LoginPage.xaml", UriKind.Relative));
        }
    }
}



