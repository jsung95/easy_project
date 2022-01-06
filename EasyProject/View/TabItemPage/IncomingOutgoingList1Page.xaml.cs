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
using System.Windows.Media;
using EasyProject.Model;
using Excel = Microsoft.Office.Interop.Excel;


namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// IncomingOutgoingList1Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IncomingOutgoingList1Page : Page
    {
        public String userDept00 = null;
        public bool isComboBoxDropDownOpened = false;
        Boolean headerflag = false;

        public IncomingOutgoingList1Page()
        {
            InitializeComponent();
            //Incomingdatagrid_LoadingRow();
            
            //dataGrid1.LayoutUpdated += new EventHandler(Incomingdatagrid_LayoutUpdated);
            export_btn.Click += Export_btn_Click;
            userDept00 = (deptName_ComboBox1.SelectedValue as DeptModel).Dept_name;
        }

        private void Incomingdatagrid_LayoutUpdated(object sender, EventArgs e)
        {
            if (headerflag)
            {

                DataGridColumn col = dataGrid1.Columns[6];
                col.CellStyle = (Style)Resources["DataGridCellStyle"];
                headerflag = false;
            }
        }

        private void OnDropDownOpened(object sender, EventArgs e)
        {
            isComboBoxDropDownOpened = true;

            var deptModelObject = deptName_ComboBox1.SelectedValue as DeptModel;
            var deptNameText = deptModelObject.Dept_name;
            userDept00= deptNameText.ToString();
        }

        //******************************************************************************
        //************************입고 유형별 색깔 다르게 주기**************************
        
       

        private void Export_btn_Click(object sender, RoutedEventArgs e)
        {
            dataGrid1.SelectAllCells();
            dataGrid1.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dataGrid1);
            dataGrid1.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string today = String.Format(DateTime.Now.ToString("yyyy/MM/dd"));

            
            string f_path = @"c:\temp\["+ userDept00 + "]"+"입고현황_" + today + ".csv";
            File.AppendAllText(f_path, result, UnicodeEncoding.UTF8);

            // Get the Excel application object.
            Excel.Application excel_app = new Excel.Application();

            // Make Excel visible (optional).
            excel_app.Visible = true;

            // Open the file.
            excel_app.Workbooks.Open(
                f_path,               // Filename
                Type.Missing,
                Type.Missing,

                   Excel.XlFileFormat.xlCSV,   // Format
                   Type.Missing,
                   Type.Missing,
                   Type.Missing,
                   Type.Missing,

                   ",",          // Delimiter
                   Type.Missing,
                   Type.Missing,
                   Type.Missing,
                   Type.Missing,
                   Type.Missing,
                   Type.Missing
            );
        }
    }
}
