using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Linq;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Windows.Navigation;
using System.Windows.Media;
using System.Collections;
using System.IO;
using System.Windows.Input;
using System.Text;

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// StatusPage.xaml에 대한 상호 작용 논리
    /// </summary>
public partial class StatusPage : Page {
        int pIndex = 1;
        private int numberOf;
        public ChartValues<float> Values { get; set; }
        private enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        List<object> myLst = new List<object>();

        public WindowStartupLocation WindowStartupLocation { get; }

        public StatusPage()
        {
            InitializeComponent();
            //cbNumberOfRecords.Items.Add("10");
            //cbNumberOfRecords.Items.Add("20");
            //cbNumberOfRecords.Items.Add("30");
            //cbNumberOfRecords.Items.Add("50");
            //cbNumberOfRecords.Items.Add("100");
            //cbNumberOfRecords.SelectedItem = 10;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            deptName_ComboBox1.SelectedIndex = (int)App.nurse_dto.Dept_id - 1;
            this.Loaded += MainWindow_Loaded;

            //chart.LegendLocation = LiveCharts.LegendLocation.Top;

            ////세로 눈금 값 설정
            //chart.AxisY.Add(new LiveCharts.Wpf.Axis { MinValue = 0, MaxValue = 1000 });
            //chart.AxisX.Add(new LiveCharts.Wpf.Axis { Labels = new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" } });
            Values = Values = new ChartValues<float>
            {
               3,4,6,3,2,6
            };
            //DataContext = this;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchText_ComboBox.Items.Add("제품코드");
            searchText_ComboBox.Items.Add("제품명");
            searchText_ComboBox.SelectedIndex = 0;
        }
        //private List<object> GetData()
        //{
        //    dataGrid.SelectAllCells();
        //    dataGrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
        //    ApplicationCommands.Copy.Execute(null, dataGrid);
        //    dataGrid.UnselectAllCells();
        //    //String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);

        //    List<object> genericList = new List<object>();

        //    genericList.Add((object)Clipboard.GetData(DataFormats.CommaSeparatedValue));
            
        //    //Student studentObj;
        //    //Random randomObj = new Random();
        //    //for (int i = 0; i < 1000; i++)
        //    //{
        //    //    studentObj = new Student();
        //    //    studentObj.ProductCode = "ProductCode " + i;
        //    //    studentObj.ProductName = "ProductName " + i;
        //    //    studentObj.Category = "Category " + i;
        //    //    studentObj.Total = "Total " + i;
        //    //    studentObj.User = "User " + i;
        //    //    studentObj.ExpirationDate = "ExpirationDate " + i;
        //    //    studentObj.UseDate = "UseDate " + i;
        //    //    studentObj.StateContent = "StateContent " + i;

        //    //    //studentObj.Age = (uint)randomObj.Next(1, 100);

        //    //    genericList.Add(studentObj);
        //    //    //dataGrid.Items.dd(studentObj);
        //    //}

        //    return genericList;
        //}

        private void RowButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("버튼을 클릭했습니다.");
        }
       
        private void goDialog_Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/View/DialogPage.xaml", UriKind.Relative)
                );

           

        }

    }
}
