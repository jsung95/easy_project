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
    /// OrderPopupBoxPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderPopupBoxPage : Page
    {
        public OrderPopupBoxPage()
        {
            InitializeComponent();

            
        }
        public void PrintBtn(object e, RoutedEventArgs arg)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog().GetValueOrDefault(false))
            {
                printDialog.PrintVisual(this, this.Title);
            }
        }
    }

}
