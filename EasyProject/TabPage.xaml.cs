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
        public TabPage()
        {
            InitializeComponent();
            signUpBtn.Click += signUpBtn_Click;
            loginBtn.Click += loginBtn_Click;
        }
        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/SignupPage.xaml", UriKind.Relative) //회원가입화면
                );
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/LoginPage.xaml", UriKind.Relative) //로그인화면
                );
        }
    }
}
