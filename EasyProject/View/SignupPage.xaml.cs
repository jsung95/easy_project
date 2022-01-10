using EasyProject.Model;
using EasyProject.ViewModel;
using MaterialDesignThemes.Wpf;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// SignupPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SignupPage : Page
    {
        public SignupPage()
        {
            InitializeComponent();
            backBtn.Click += backBtn_Click;
            //rewriteBtn.Click += rewriteBtn_Click;
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
               new Uri("/View/LoginPage.xaml", UriKind.Relative) //회원가입화면
               );
        }
        /*
        private void rewriteBtn_Click(object sender, RoutedEventArgs e)
        {
            //name_TxtBox.Text = "";
            //id_TxtBox.Text = "";
            //password_PwBox.Password = "";
            //rePassword_PwBox.Password = "";
        }
        */
        //private async void ButtonError_Click(object sender, RoutedEventArgs e)
        //{
        //    ErrorNotificationMessage msg = new ErrorNotificationMessage();
        //    msg.Message = "회원가입 성공";

        //    await DialogHost.Show(msg, "RootDialog");
        //}
        private async void signUpBtn_Click(object sender, RoutedEventArgs e)
        {
            //await Task.Delay(1500);
            var temp = Ioc.Default.GetService<SignupViewModel>();
            var signupTask = Task.Run(() => temp.SignupInsert());
            bool signupResult = await signupTask; // signupTask 끝나면 결과를 signupResult에 할당
            if (signupResult == true) //회원가입에 성공하면
            {
                Console.WriteLine("isSignup : " + signupResult);
                Console.WriteLine("회원가입 성공");
                //ErrorNotificationMessage msg = new ErrorNotificationMessage();
                //msg.Message = "회원가입 성공";

                NavigationService.Navigate
                    (
                    new Uri("/View/LoginPage.xaml", UriKind.Relative) //로그인화면으로 이동
                    );
            }
            else //회원가입에 실패하면
            {
                Console.WriteLine("isSignup : " + signupResult);
                Console.WriteLine("회원가입 실패");

            }



/*            if (password_PwBox.Password == rePassword_PwBox.Password)
            {
                MessageBox.Show(name_TxtBox.Text + " " + id_TxtBox.Text + " " + password_PwBox.Password);
            }
            else
            {
                MessageBox.Show("비밀번호가 맞지 않습니다.");
            }*/

        }

        private void id_TxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }



    }
}
