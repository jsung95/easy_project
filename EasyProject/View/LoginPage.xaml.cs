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
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            loginBtn.Click += loginBtn_Click;
            signUpBtn.Click += signUpBtn_Click;
            searchBtn.Click += searchBtn_Click;
            //체크박스 클릭 시 --> 아이디 다음번에 왔을 때 기억하는거 어떻게 할지  정해야함.
        }


        private void searchBtn_Click(object sender, RoutedEventArgs e) //ID/PW 찾기 버튼 클릭 시
        {
            //throw new NotImplementedException();
            //ID/PW 찾기 페이지 연결
            //MessageBox.Show("PW 변경 버튼 누르셨습니다.");
            NavigationService.Navigate
                (
                new Uri("/View/PasswordChangePage.xaml", UriKind.Relative) // 비밀번호 변경화면
                );
        }
        private void signUpBtn_Click(object sender, RoutedEventArgs e) //회원가입 버튼 클릭 시
        {
            //throw new NotImplementedException();
            //회원가입 창 연결.
            NavigationService.Navigate
                (
                new Uri("/View/SignupPage.xaml", UriKind.Relative) //회원가입화면
                );

        }
        private void loginBtn_Click(object sender, RoutedEventArgs e) //로그인 버튼 클릭 시
        {

            var button = sender as Button;
            if (button != null) 
            {
                button.Command.Execute(null);
            }

            NavigationService.Navigate(new Uri("/View/TabPage.xaml", UriKind.Relative));
        
        }
    }
}
