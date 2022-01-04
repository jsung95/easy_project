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

namespace EasyProject.View.TabItemPage.GraphPage
{
    /// <summary>
    /// RemainExpire_GraphPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RemainExpire_GraphPage : Page
    {
        public RemainExpire_GraphPage()
        {
            InitializeComponent();
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/View/TabItemPage/GraphPage/DeptCate_GraphPage.xaml", UriKind.Relative) //재고현황화면 --테스트
                );
        }
    }
}
