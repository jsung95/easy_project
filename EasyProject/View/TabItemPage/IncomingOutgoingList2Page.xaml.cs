using System;
using System.Collections.Generic;
using System.IO;
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
    /// IncomingOutgoingList2Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IncomingOutgoingList2Page : Page
    {
        public int i = 0;
        public IncomingOutgoingList2Page()
        {
            InitializeComponent();
            export_btn.Click += Export_btn_Click;
        }
        private void Export_btn_Click(object sender, RoutedEventArgs e)
        {
            dataGrid2.SelectAllCells();
            dataGrid2.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataGrid2);
            dataGrid2.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

            //string f = @"c:\temp"; 
            string today = String.Format(DateTime.Now.ToString("yyyyMMddhhmmss"));
            string f_path = @"c:\temp\MyTest"+today+".csv";

            using (StreamWriter sw = File.CreateText(f_path))
            {
                sw.Write(result);
            } 
                ;
        }
    }
}
