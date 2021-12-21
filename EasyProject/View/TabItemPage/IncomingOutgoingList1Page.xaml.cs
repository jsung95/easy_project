using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;


namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// IncomingOutgoingList1Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IncomingOutgoingList1Page : Page
    {
        public IncomingOutgoingList1Page()
        {
            InitializeComponent();
            export_btn.Click += Export_btn_Click;
        }

        private void Export_btn_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.SelectAllCells();
            dataGrid1.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataGrid1);
            dataGrid1.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string today = String.Format(DateTime.Now.ToString("yyyyMMddhhmmss"));
            string f_path = @"c:\temp\MyTest" + today + ".csv";

            using (StreamWriter sw = File.CreateText(f_path))
            {
                sw.Write(result);
            }
                ;
        }
    }
}
