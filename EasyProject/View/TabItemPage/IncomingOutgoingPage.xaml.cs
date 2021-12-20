using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Data;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Page = Microsoft.Office.Interop.Excel.Page;
using System.Data;
using System.Runtime.InteropServices;

namespace EasyProject.View
{
    /// <summary>
    /// IncomingOutgoingPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IncomingOutgoingPage : Page
    {
        int pIndex = 1;
        private int numberOf;
        
        private enum PagingMode { 
            First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 
        };

        List<object> myList = new List<object>();

        public WindowStartupLocation WindowStartupLocation { get; }

        public IncomingOutgoingPage()
        {
            InitializeComponent();
 
            //DataGridTextColumn col1 = new DataGridTextColumn();
            //dataGrid1.Columns.Add(col1);
            //col1.Header = "ID"; //Header = "제품코드"

            //dataGrid1.Items.Add(new MyData
            //{
            //    ProductCode = "a",
            //});

            //col1.Binding = new Binding("ProductCode");
            // this.table = GetTable();

            // dataGrid1.ItemsSource = this.table.DefaultView;
            // this.dataGrid1.ItemsSource = this.table.DefaultView;
            //myList = (List<object>)dataGrid1.ItemsSource;

            //cbNumberOfRecords.Items.Add("10");
            //cbNumberOfRecords.Items.Add("20");
            //cbNumberOfRecords.Items.Add("30");
            //cbNumberOfRecords.Items.Add("50");
            //cbNumberOfRecords.Items.Add("100");
            //cbNumberOfRecords.SelectedItem = 10;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Loaded += Page_Loaded;
        }
            //private System.Data.DataTable GetTable()
            //{             

            //    //dataGrid1.Columns.Add(col1); ///제품코드 

            //    //table.Columns.Add("b"); ///제품명

            //    //table.Rows.Add("123");

            //    return table;
            //}

            private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //myList = GetData();
            //dataGrid1.ItemsSource = myList.Take(numberOfRecPerPage);

            int count = myList.Take(numberOf).Count();
            //lblpageInformation.Content = count + " of " + myList.Count;
        }
        //private List<object> GetData()
        //{
        //    List<object> genericList = new List<object>();
        //    Student studentObj;
        //    //Random randomObj = new Random();

        //    for (int i = 0; i < 1000; i++)
        //    {
        //        studentObj = new Student();
        //        studentObj.ProductCode = "A " + i;
        //        studentObj.ProductName = "B " + i;
        //        studentObj.Category = "CC " + i;
        //        studentObj.Total = "DDD " + i;
        //        studentObj.User = "EE " + i;
        //        studentObj.ExpirationDate = "FF " + i;
        //        studentObj.UseDate = "GGGGGGG " + i;
        //        studentObj.StateContent = "HHHHHH " + i;

        //        //studentObj.Age = (uint)randomObj.Next(1, 100);

        //        genericList.Add(studentObj);
        //    }
        //    return genericList;
        //}
        /*private void txtName                                                                                                                                                                                                                                                                                                                                                                                                                             _TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox t = (TextBox)sender;
            string filter = t.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dg.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    Student p = o as Student;
                    if (t.Name == "txtId")
                        return (p.Id == Convert.ToInt32(filter));
                    return (p.ExpirationDate.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }*/

        //#region Pagination 
        private void btnFirst_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.First);
        }

        private void btnNext_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Next);

        }

        private void btnPrev_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Previous);

        }

        private void btnLast_Click(object sender, System.EventArgs e)
        {
            Navigate((int)PagingMode.Last);
        }
        private void cbNumberOfRecords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigate((int)PagingMode.PageCountChange);
        }
        private void Navigate(int mode)
        {
            //int count;
            //switch (mode)
            //{
            //    case (int)PagingMode.Next:
            //        btnPrev.IsEnabled = true;
            //        btnFirst.IsEnabled = true;

            //        if (myList.Count >= (pIndex * numberOf))
            //        {
            //            if (myList.Skip(pIndex *
            //            numberOf).Take(numberOf).Count() == 0)
            //            {
            //                dataGrid1.ItemsSource = null;
            //                dataGrid1.ItemsSource = myList.Skip((pIndex *
            //                numberOf) - numberOf).Take(numberOf);
            //                count = (pIndex * numberOf) +
            //                (myList.Skip(pIndex *
            //                numberOf).Take(numberOf)).Count();
            //            }
            //            else
            //            {
            //                dataGrid1.ItemsSource = null;
            //                dataGrid1.ItemsSource = myList.Skip(pIndex * numberOf).Take(numberOf);
            //                count = (pIndex * numberOf) + (myList.Skip(pIndex * numberOf).Take(numberOf)).Count();
            //                pIndex++;
            //            }

            //            //lblpageInformation.Content = count + " of " + myList.Count;
            //        }
            //        else
            //        {
            //            btnNext.IsEnabled = false;
            //            btnLast.IsEnabled = false;
            //        }
            //        break;
            //    case (int)PagingMode.Previous:
            //        btnNext.IsEnabled = true;
            //        btnLast.IsEnabled = true;
            //        if (pIndex > 1)
            //        {
            //            pIndex -= 1;
            //            //dataGrid.ItemsSource = null;
            //            if (pIndex == 1)
            //            {
            //               //dataGrid.ItemsSource = myLst.Take(numberOf);
            //                count = myList.Take(numberOf).Count();
            //                //lblpageInformation.Content = count + " of " + myLst.Count;
            //            }
            //            else
            //            {
            //                //dataGrid.ItemsSource = myLst.Skip
            //                //(pIndex * numberOf).Take(numberOf);
            //                count = Math.Min(pIndex * numberOf, myList.Count);
            //                //lblpageInformation.Content = count + " of " + myLst.Count;
            //            }
            //        }
            //        else
            //        {
            //            btnPrev.IsEnabled = false;
            //            btnFirst.IsEnabled = false;
            //        }
            //        break;

            //    case (int)PagingMode.First:
            //        pIndex = 2;
            //        Navigate((int)PagingMode.Previous);
            //        break;
            //    case (int)PagingMode.Last:
            //        pIndex = (myList.Count / numberOf);
            //        Navigate((int)PagingMode.Next);
            //        break;

            //    case (int)PagingMode.PageCountChange:
            //        pIndex = 1;
            //        numberOf = Convert.ToInt32(cbNumberOfRecords.SelectedItem);
            //        dataGrid1.ItemsSource = null;
            //        dataGrid1.ItemsSource = myList.Take(numberOf);
            //        count = (myList.Take(numberOf)).Count();
            //       // lblpageInformation.Content = count + " of " + myLiist.Count;
            //        btnNext.IsEnabled = true;
            //        btnLast.IsEnabled = true;
            //        btnPrev.IsEnabled = true;
            //        btnFirst.IsEnabled = true;
            //        break;
            //}
        }
        //#endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //public Microsoft.Office.Interop.Excel.Application APP = null;
            //public Microsoft.Office.Interop.Excel.Workbook WB = null;
            //public Microsoft.Office.Interop.Excel.Worksheet WS = null;
            //public Microsoft.Office.Interop.Excel.Range Range = null;
            Excel.Application excel = new Excel.Application();
            excel.Visible = true; //www.yazilimkodlama.com
            Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];

            for (int j = 0; j < dataGrid1.Columns.Count; j++) //타이틀용 
            {
                Range myRange = (Range)sheet1.Cells[1, j + 1];
                sheet1.Cells[1, j + 1].Font.Bold = true; //제목을 굵게 표시
                sheet1.Columns[j + 1].ColumnWidth = 15; //열 너비 설정
                myRange.Value2 = dataGrid1.Columns[j].Header;
            }
            /*for (int i = 0; i < dataGrid1.Columns.Count; i++)
            { //www.yazilimkodlama.com
                for (int j = 0; j < dataGrid1.Items.Count; j++)
                {
                    TextBlock b = dataGrid1.Columns[i].GetCellContent(dataGrid1.Items[j]) as TextBlock;
                    // 
                    Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                    myRange.Value2 = b.Text;

                }
            }*/
        }

        HeaderFooter Page.LeftHeader => throw new NotImplementedException();

        HeaderFooter Page.CenterHeader => throw new NotImplementedException();

        HeaderFooter Page.RightHeader => throw new NotImplementedException();

        HeaderFooter Page.LeftFooter => throw new NotImplementedException();

        HeaderFooter Page.CenterFooter => throw new NotImplementedException();

        HeaderFooter Page.RightFooter => throw new NotImplementedException();
    }
}
