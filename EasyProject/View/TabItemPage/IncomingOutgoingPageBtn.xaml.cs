using EasyProject.Model;
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

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// IncomingOutgoingPageBtn.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IncomingOutgoingPageBtn : Page
    {
        public String userDept00 = null;
        public bool isComboBoxDropDownOpened = false;
        public IncomingOutgoingPageBtn()
        {
            InitializeComponent();
            IncomingBtn.Click += Incoming_Click;
            OutgoingBtn.Click += Outgoing_Click;
        }
        private void OnDropDownOpened(object sender, EventArgs e)
        {
            isComboBoxDropDownOpened = true;

            var deptModelObject = deptName_ComboBox1.SelectedValue as DeptModel;
            var deptNameText = deptModelObject.Dept_name;
            userDept00 = deptNameText.ToString();
        }

        private void Incoming_Click(object sender, RoutedEventArgs e)
        {
            ListFrame.Source = new Uri("IncomingOutgoingList1Page.xaml", UriKind.Relative);
        }
        private void Outgoing_Click(object sender, RoutedEventArgs e)
        {
            ListFrame.Source = new Uri("IncomingOutgoingList2Page.xaml", UriKind.Relative);
        }
    }
}
