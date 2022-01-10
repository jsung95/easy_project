﻿using System;
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
using Excel = Microsoft.Office.Interop.Excel;
using EasyProject.Model;
using System.Globalization;
using EasyProject.ViewModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// IncomingOutgoingList2Page.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IncomingOutgoingList2Page : Page
    {
        public int i = 0;
        public String userDept00 = null;
        public bool isComboBoxDropDownOpened = false;
        public IncomingOutgoingList2Page()
        {
            InitializeComponent();
            export_btn.Click += Export_btn_Click;
           // userDept00 = (deptName_ComboBox1.SelectedValue as DeptModel).Dept_name;
        }
        //private void OnDropDownOpened(object sender, EventArgs e)
        //{
        //    isComboBoxDropDownOpened = true;

        //    var deptModelObject = deptName_ComboBox1.SelectedValue as DeptModel;
        //    var deptNameText = deptModelObject.Dept_name;
        //    userDept00 = deptNameText.ToString();
        //}
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var temp = Ioc.Default.GetService<ProductInOutViewModel>();
            //temp.DashboardPrint();

            temp.DashboardPrint2();
            temp.DashboardPrint3();
            //temp.DashboardPrint4(temp.SelectedCategory1);
            //temp.DashboardPrint_Pie();


        }


        private void Export_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGrid2.SelectAllCells();
                dataGrid2.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, dataGrid2);
                dataGrid2.UnselectAllCells();
                String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

                string today = String.Format(DateTime.Now.ToString("yyyy/MM/dd/HH/mm"));


                string f_path = @"c:\temp\[" + userDept00 + "]" + "출고현황_" + today + ".csv";
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
            catch (Exception ex)
            {
                //로그 Level : error
            }
        }
    }
    public class MultipleTextFormatConverterKey2 : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format((string)parameter, values);


        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

}
