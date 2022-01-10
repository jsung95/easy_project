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
using EasyProject.Model;

namespace EasyProject.View.TabItemPage.GraphPage
{
    /// <summary>
    /// Incoming_GraphPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Incoming_GraphPage : Page
    {
        public Incoming_GraphPage()
        {
            InitializeComponent();
            var dash = Ioc.Default.GetService<ProductInOutViewModel>();
            dash.DashboardPrint3(dash.SelectedStartDate_In, dash.SelectedEndDate_In);
            dash.DashboardPrint2(dash.SelectedStartDate_In, dash.SelectedEndDate_In);
            dash.dashboardPrint_Pie1();
            //temp.DashboardPrint();
            //dash.DashboardPrint3();
        }

        private void rightBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/View/TabItemPage/GraphPage/Outgoing_GraphPage.xaml", UriKind.Relative) //재고현황화면 --테스트
                );
            var dash = Ioc.Default.GetService<ProductInOutViewModel>();
            //temp.DashboardPrint();
            dash.DashboardPrint3(dash.SelectedStartDate_In, dash.SelectedEndDate_In);
            dash.DashboardPrint2(dash.SelectedStartDate_In, dash.SelectedEndDate_In);
            dash.dashboardPrint_Pie1();
        }
    }
}
