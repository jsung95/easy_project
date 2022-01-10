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
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = Ioc.Default.GetService<ProductInOutViewModel>();
            //temp.DashboardPrint();

            temp.DashboardPrint2();
            temp.DashboardPrint3();
            //temp.DashboardPrint4(temp.SelectedCategory1);
            //temp.DashboardPrint_Pie();


        }
    }
}
