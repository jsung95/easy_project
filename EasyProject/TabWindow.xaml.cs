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
    /// TabWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TabWindow : Page
    {
        public TabWindow()
        {
            InitializeComponent();
            signUpBtn.Click += signUpBtn_Click;
            loginBtn.Click += loginBtn_Click;

        }
        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/SignUpWindow.xaml", UriKind.Relative)
                );
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/LoginWindow.xaml", UriKind.Relative)
                );
        }
    }
}
