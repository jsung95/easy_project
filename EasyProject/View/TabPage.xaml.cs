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
            userNameTxtBox.Text = App.nurse_dto.Nurse_name;
        }
        /*private void StatusBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                 (
                 new Uri("/View/TabItemPage/StatusPage.xaml", UriKind.Relative) //재고현황화면 --테스트
                 );

        }*/

        private void btn_close(object sender, RoutedEventArgs e)       //버튼 창닫기
        {
            Window.GetWindow(this).Close();
        }

    }
}
