using EasyProject.ViewModel;
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

namespace EasyProject.View
{
    /// <summary>
    /// PasswordChangePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PasswordChangePage : Page
    {
        public PasswordChangePage()
        {
            InitializeComponent();
            backBtn.Click += backBtn_Click;
            pwChangeBtn.Click += pwChangeBtn_Click;
        }

        private void pwChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var temp = Ioc.Default.GetService<PasswordChangeViewModel>();
            temp.PasswordChange();
            if (temp.IsChanged == true)
            {
                NavigationService.Navigate( new Uri("/View/LoginPage.xaml", UriKind.Relative) ); //로그인 화면
            }
            else
            {
                return;
            }
        }//pwChangeBtn_Click

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
           (
                new Uri("/View/LoginPage.xaml", UriKind.Relative) //로그인 화면
           );
        }
    }
}
