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
            backButton.Click += backBtn_Click;
            name.TextChanged += textChangedEventHandler;
        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/TabWindow.xaml", UriKind.Relative) //회원가입화면
                );
        }
        private void textChangedEventHandler(object sender, TextChangedEventArgs args)
        {
            MessageBox.Show(sender.ToString());//수정 예정
        }
    }
}
