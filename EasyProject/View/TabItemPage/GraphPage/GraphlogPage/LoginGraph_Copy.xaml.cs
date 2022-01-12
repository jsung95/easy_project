using Microsoft.Toolkit.Mvvm.DependencyInjection;
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
using EasyProject.ViewModel;

namespace EasyProject.View.TabItemPage.GraphPage.GraphlogPage
{
    /// <summary>
    /// LoginGraph_Copy.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginGraph_Copy : Page
    {
        public LoginGraph_Copy()
        {
            InitializeComponent();
            var dash = Ioc.Default.GetService<LogViewModel>();
            dash.TodayLoginPrint();
            dash.TodayLogoutPrint();
        }


        private void RightBtn_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                               (
                               new Uri("/View/TabItemPage/GraphPage/GraphlogPage/LogoutGraph_Copy.xaml", UriKind.Relative) //재고현황화면 --테스트
                               );
        }
    }
}
