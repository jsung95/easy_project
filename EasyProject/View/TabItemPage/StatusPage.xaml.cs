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
using EasyProject.Model;

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// StatusPage.xaml에 대한 상호 작용 논리
    /// </summary>
    //public class TestData
    //{
    //    public int test1 { get; set; }
    //    public string test2 { get; set; }
    //    public int test3 { get; set; }
    //    public string test4 { get; set; }
    //    public string test5 { get; set; }
    //    public string test6 { get; set; }
    //    public string test7 { get; set; }
    //    public string test8 { get; set; }

    //}
    public partial class StatusPage : Page {
        //int pIndex = 1;
        //private int numberOf;
        public ChartValues<float> Values { get; set; }
        private enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        List<object> myLst = new List<object>();

        public String userDept = null;
        //public WindowStartupLocation WindowStartupLocation { get; }

        public StatusPage()
        {
            InitializeComponent();
            cbNumberOfRecords.Items.Add("10");
            cbNumberOfRecords.Items.Add("20");
            cbNumberOfRecords.Items.Add("30");
            cbNumberOfRecords.Items.Add("50");
            cbNumberOfRecords.Items.Add("100");
            cbNumberOfRecords.SelectedItem = 10;
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
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
            userDept = (deptName_ComboBox1.SelectedValue as DeptModel).Dept_name;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            searchText_ComboBox.Items.Add("제품코드");
            searchText_ComboBox.Items.Add("제품명");
            searchText_ComboBox.SelectedIndex = 0;

        }

        private void RowButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("버튼을 클릭했습니다.");
        }
        //#region Pagination 
        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            //Navigate((int)PagingMode.First);
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            //Navigate((int)PagingMode.Next);

        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            //Navigate((int)PagingMode.Previous);

        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            //Navigate((int)PagingMode.Last);
        }

        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Navigate((int)PagingMode.PageCountChange);
        }
        private void Part_comboBox_Selection(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        //private void Navigate(int mode)
        //{
        //    int count;
        //    switch (mode)
        //    {
        //        case (int)PagingMode.Next:
        //            btnPrev.IsEnabled = true;
        //            btnFirst.IsEnabled = true;
        //            MessageBox.Show(numberOf+"입니다.");
        //            if (myLst.Count >= (pIndex * numberOf))
        //            {
        //                if (myLst.Skip(pIndex *
        //                numberOf).Take(numberOf).Count() == 0)
        //                {
        //                    dataGrid.ItemsSource = null;
        //                    dataGrid.ItemsSource = myLst.Skip((pIndex *
        //                    numberOf) - numberOf).Take(numberOf);
        //                    count = (pIndex * numberOf) +
        //                    (myLst.Skip(pIndex *
        //                    numberOf).Take(numberOf)).Count();
        //                }
        //                else
        //                {
        //                    dataGrid.ItemsSource = null;
        //                    dataGrid.ItemsSource = myLst.Skip(pIndex * numberOf).Take(numberOf);
        //                    count = (pIndex * numberOf) + (myLst.Skip(pIndex * numberOf).Take(numberOf)).Count();
        //                    pIndex++;
        //                }

        //                lblpageInformation.Content = count + " of " + myLst.Count;
        //            }
        //            else
        //            {
        //                btnNext.IsEnabled = false;
        //                btnLast.IsEnabled = false;
        //            }
        //            break;
        //        case (int)PagingMode.Previous:
        //            btnNext.IsEnabled = true;
        //            btnLast.IsEnabled = true;
        //            if (pIndex > 1)
        //            {
        //                pIndex -= 1;
        //                dataGrid.ItemsSource = null;
        //                if (pIndex == 1)
        //                {
        //                    dataGrid.ItemsSource = myLst.Take(numberOf);
        //                    count = myLst.Take(numberOf).Count();
        //                    lblpageInformation.Content = count + " of " + myLst.Count;
        //                }
        //                else
        //                {
        //                    dataGrid.ItemsSource = myLst.Skip
        //                    (pIndex * numberOf).Take(numberOf);
        //                    count = Math.Min(pIndex * numberOf, myLst.Count);
        //                    lblpageInformation.Content = count + " of " + myLst.Count;
        //                }
        //            }
        //            else
        //            {
        //                btnPrev.IsEnabled = false;
        //                btnFirst.IsEnabled = false;
        //            }
        //            break;

        //        case (int)PagingMode.First:
        //            pIndex = 2;
        //            Navigate((int)PagingMode.Previous);
        //            break;
        //        case (int)PagingMode.Last:
        //            pIndex = (myLst.Count / numberOf);
        //            Navigate((int)PagingMode.Next);
        //            break;

        //        case (int)PagingMode.PageCountChange:
        //            pIndex = 1;
        //            numberOf = Convert.ToInt32(cbNumberOfRecords.SelectedItem);
        //            dataGrid.ItemsSource = null;
        //            dataGrid.ItemsSource = myLst.Take(numberOf);
        //            count = (myLst.Take(numberOf)).Count();
        //            lblpageInformation.Content = count + " of " + myLst.Count;
        //            btnNext.IsEnabled = true;
        //            btnLast.IsEnabled = true;
        //            btnPrev.IsEnabled = true;
        //            btnFirst.IsEnabled = true;
        //            break;
        //    }
        //}
        //#endregion
        private void goDialog_Btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate
                (
                new Uri("/View/DialogPage.xaml", UriKind.Relative)
                );



        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (deptName_ComboBox1.SelectedValue != null)
            {
                var deptModelObject = deptName_ComboBox1.SelectedValue as DeptModel;
                var deptNameText = deptModelObject.Dept_name;
                var userText = userDept;

                
                if (deptNameText.Equals(userText) || userText == null)
                {
                    Console.WriteLine(userText + "같은 부서일때");
                    buttonColumn.Visibility = Visibility.Visible;
                }
                else
                {
                    Console.WriteLine(userText + "다른 부서일때");
                    buttonColumn.Visibility = Visibility.Hidden;
                }
            }
        } 
    }
}
