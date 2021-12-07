using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EasyProject.View
{
    /// <summary>
    /// IncomingOutgoingPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public class Student
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Total { get; set; }
        public string User { get; set; }
        public string ExpirationDate { get; set; }
        public string UseDate { get; set; }
        public string StateContent { get; set; }
        //public uint Age { get; set; }

    }
    public partial class IncomingOutgoingPage : Page
    {
        int pageIndex = 1;
        private int numberOfRecPerPage;
        private enum PagingMode
        { First = 1, Next = 2, Previous = 3, Last = 4, PageCountChange = 5 };

        List<object> myList = new List<object>();

        public WindowStartupLocation WindowStartupLocation { get; }

        public IncomingOutgoingPage()
        {
            InitializeComponent();
            cbNumberOfRecords.Items.Add("10");
            cbNumberOfRecords.Items.Add("20");
            cbNumberOfRecords.Items.Add("30");
            cbNumberOfRecords.Items.Add("50");
            cbNumberOfRecords.Items.Add("100");
            cbNumberOfRecords.SelectedItem = 10;
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            myList = GetData();
            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
            int count = myList.Take(numberOfRecPerPage).Count();
            lblpageInformation.Content = count + " of " + myList.Count;
        }
        private List<object> GetData()
        {
            List<object> genericList = new List<object>();
            Student studentObj;
            Random randomObj = new Random();
            for (int i = 0; i < 1000; i++)
            {
                studentObj = new Student();
                studentObj.ProductCode = "ProductCode " + i;
                studentObj.ProductName = "ProductName " + i;
                studentObj.Category = "Category " + i;
                studentObj.Total = "Total " + i;
                studentObj.User = "User " + i;
                studentObj.ExpirationDate = "ExpirationDate " + i;
                studentObj.UseDate = "UseDate " + i;
                studentObj.StateContent = "StateContent " + i;

                //studentObj.Age = (uint)randomObj.Next(1, 100);

                genericList.Add(studentObj);
            }
            return genericList;
        }

        #region Pagination 
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
            int count;
            switch (mode)
            {
                case (int)PagingMode.Next:
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    if (myList.Count >= (pageIndex * numberOfRecPerPage))
                    {
                        if (myList.Skip(pageIndex *
                        numberOfRecPerPage).Take(numberOfRecPerPage).Count() == 0)
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip((pageIndex *
                            numberOfRecPerPage) - numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                            (myList.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                        }
                        else
                        {
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = myList.Skip(pageIndex *
                            numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = (pageIndex * numberOfRecPerPage) +
                            (myList.Skip(pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage)).Count();
                            pageIndex++;
                        }

                        lblpageInformation.Content = count + " of " + myList.Count;
                    }

                    else
                    {
                        btnNext.IsEnabled = false;
                        btnLast.IsEnabled = false;
                    }

                    break;
                case (int)PagingMode.Previous:
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    if (pageIndex > 1)
                    {
                        pageIndex -= 1;
                        dataGrid.ItemsSource = null;
                        if (pageIndex == 1)
                        {
                            dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
                            count = myList.Take(numberOfRecPerPage).Count();
                            lblpageInformation.Content = count + " of " + myList.Count;
                        }
                        else
                        {
                            dataGrid.ItemsSource = myList.Skip
                            (pageIndex * numberOfRecPerPage).Take(numberOfRecPerPage);
                            count = Math.Min(pageIndex * numberOfRecPerPage, myList.Count);
                            lblpageInformation.Content = count + " of " + myList.Count;
                        }
                    }
                    else
                    {
                        btnPrev.IsEnabled = false;
                        btnFirst.IsEnabled = false;
                    }
                    break;

                case (int)PagingMode.First:
                    pageIndex = 2;
                    Navigate((int)PagingMode.Previous);
                    break;
                case (int)PagingMode.Last:
                    pageIndex = (myList.Count / numberOfRecPerPage);
                    Navigate((int)PagingMode.Next);
                    break;

                case (int)PagingMode.PageCountChange:
                    pageIndex = 1;
                    numberOfRecPerPage = Convert.ToInt32(cbNumberOfRecords.SelectedItem);
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = myList.Take(numberOfRecPerPage);
                    count = (myList.Take(numberOfRecPerPage)).Count();
                    lblpageInformation.Content = count + " of " + myList.Count;
                    btnNext.IsEnabled = true;
                    btnLast.IsEnabled = true;
                    btnPrev.IsEnabled = true;
                    btnFirst.IsEnabled = true;
                    break;
            }
        }
        #endregion
    }
}
