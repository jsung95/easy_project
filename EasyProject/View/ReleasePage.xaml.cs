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
    /// ReleasePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ReleasePage : Page
    {
        public ReleasePage()
        {
            InitializeComponent();
            

        }

        
       

            private void reset_Btn_Click(object sender, RoutedEventArgs e)
        {
            mount_TxtBox.Text = "";
        }

        private void cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
               (
               new Uri("/View/TabItemPage/StatusPage.xaml", UriKind.Relative)
               );
        }

        private void signUp_Btn_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
