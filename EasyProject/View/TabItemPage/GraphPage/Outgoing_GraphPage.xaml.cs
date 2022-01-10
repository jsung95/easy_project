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
using EasyProject.Model;
using EasyProject.ViewModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace EasyProject.View.TabItemPage.GraphPage
{
    /// <summary>
    /// Outgoing_GraphPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Outgoing_GraphPage : Page
    {
        public Outgoing_GraphPage()
        {
            InitializeComponent();
            var temp = Ioc.Default.GetService<ProductInOutViewModel>();
            //temp.DashboardPrint();

            temp.DashboardPrint2();   //출고
            //temp.DashboardPrint3();   //입고
            //temp.DashboardPrint4(temp.SelectedCategory1);
            //temp.DashboardPrint_Pie();
        }
        

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/View/TabItemPage/GraphPage/Incoming_GraphPage.xaml", UriKind.Relative) //재고현황화면 --테스트
                );
            var dash = Ioc.Default.GetService<ProductInOutViewModel>();
            //temp.DashboardPrint();
            dash.DashboardPrint2();
            //dash.DashboardPrint2(dash.SelectedDept);
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/View/TabItemPage/GraphPage/DiscardProdPrice_GraphPage.xaml", UriKind.Relative) //재고현황화면 --테스트
                );
            var dash = Ioc.Default.GetService<ProductInOutViewModel>();
            //temp.DashboardPrint();
            dash.DashboardPrint2();
            //dash.DashboardPrint2(dash.SelectedDept);
        }
    }
}
