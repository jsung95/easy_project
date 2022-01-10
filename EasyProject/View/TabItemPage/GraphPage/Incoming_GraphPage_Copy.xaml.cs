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
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace EasyProject.View.TabItemPage.GraphPage
{
    /// <summary>
    /// Incoming_GraphPage_Copy.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Incoming_GraphPage_Copy : Page
    {
        public Incoming_GraphPage_Copy()
        {
            InitializeComponent();
            var dash = Ioc.Default.GetService<ProductInOutViewModel>();
            dash.DashboardPrint4(dash.SelectedStartDate_Out, dash.SelectedEndDate_Out);
            dash.DashboardPrint5(dash.SelectedStartDate_Out, dash.SelectedEndDate_Out);
            dash.dashboardPrint_Pie();
        }

        private void rightBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/View/TabItemPage/GraphPage/Outgoing_GraphPage_Copy.xaml", UriKind.Relative) //재고현황화면 --테스트
                );
            var dash = Ioc.Default.GetService<ProductInOutViewModel>();
            dash.DashboardPrint4(dash.SelectedStartDate_Out, dash.SelectedEndDate_Out);
            dash.DashboardPrint5(dash.SelectedStartDate_Out, dash.SelectedEndDate_Out);
            dash.dashboardPrint_Pie();
        }
    }
}
