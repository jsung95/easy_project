using EasyProject.ViewModel;
using log4net;
using MaterialDesignThemes.Wpf;
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

namespace EasyProject.View
{
    /// <summary>
    /// InsertPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InsertPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));
        public InsertPage()
        {
            log.Info("Constructor InsertPage() invoked.");
            InitializeComponent();
            formBtn.Click += formBtn_Click;
            excelBtn.Click += excelBtn_Click;
        }
        private void formBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Info("formBtn_Click(object, RoutedEventArgs) invoked.");
            try
            {
                formBtn.Background= System.Windows.Media.Brushes.Blue;
                excelBtn.Background = System.Windows.Media.Brushes.LightGray;
                InsertPageFrame.Source = new Uri("InsertPage_Form.xaml", UriKind.Relative);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            
        }
        private void excelBtn_Click(object sender, RoutedEventArgs e)
        {
            log.Info("excelBtn_Click(object, RoutedEventArgs) invoked.");
            try
            {
                formBtn.Background = System.Windows.Media.Brushes.LightGray;
                excelBtn.Background = System.Windows.Media.Brushes.Blue;
                InsertPageFrame.Source = new Uri("InsertPage_Excel.xaml", UriKind.Relative);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            
        }
    }
}
