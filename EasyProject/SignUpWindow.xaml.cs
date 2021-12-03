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
    /// SignUpWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignUpWindow : Page
    {
        public SignUpWindow()
        {
            InitializeComponent();
            backBtn.Click += backBtn_Click;
            rewriteBtn.Click += rewriteBtn_Click;
            signUpBtn.Click += signUpBtn_Click;
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/TabWindow.xaml", UriKind.Relative) //회원가입화면
                );
        }
        private void rewriteBtn_Click(object sender, RoutedEventArgs e)
        {
            name.Text = "";
            user_id.Text = "";
            user_password.Password = "";
            re_user_password.Password = "";
        }
        private void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            if(user_password.Password == re_user_password.Password)
            {
                MessageBox.Show(name.Text + " " + user_id.Text + " " + user_password.Password);
            }
            else
            {
                MessageBox.Show("비밀번호가 맞지 않습니다.");
            }

        }
    }
}
