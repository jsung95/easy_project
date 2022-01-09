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
using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using EasyProject.ViewModel;
using System.Windows.Data;
using System.Globalization;
using log4net;
using System.Windows.Input;

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// StatusPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StatusPage : Page
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(App));
        public ChartValues<float> Values { get; set; }

        public bool isComboBoxDropDownOpened = false;

        public string categoryComboBoxFirstSelected = null;

        public StatusPage()
        {
            log.Info("StatusPage initialized");
            InitializeComponent();
            //dept_Label.Visibility = Visibility.Hidden;
            //Dept_comboBox.Visibility = Visibility.Hidden;

            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var deptModelObject = deptName_ComboBox1.SelectedValue as DeptModel;
            var deptNameText = deptModelObject.Dept_name; // 콤보박스에서 선택한 부서명
            var temp = Ioc.Default.GetService<ProductShowViewModel>();
            var userDept = temp.Depts[(int)App.nurse_dto.Dept_id - 1];  // 현재 사용자 소속 부서 객체
            var userDeptName = userDept.Dept_name;

            var dash = Ioc.Default.GetService<ProductShowViewModel>();
            dash.DashboardPrint1(dash.SelectedDept, dash.SelectedCategory1,dash.SelectedNumber);
            dash.DashboardPrint2(dash.SelectedDept);

            if (App.nurse_dto.Nurse_auth.Equals("ADMIN"))
            {
                ModifyToggleButtonPanel.Visibility = Visibility.Visible;

                if (deptNameText.Equals(userDeptName) || userDeptName == null)
                {
                    Console.WriteLine(userDeptName + "같은 부서일때");
                    buttonColumn.Visibility = Visibility.Visible;
                }
                else
                {
                    Console.WriteLine(userDeptName + "다른 부서일때");
                    buttonColumn.Visibility = Visibility.Hidden;
                }
            }
        }

        private void SomeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            if(comboBox.SelectedItem != null)
            {
                ((ProductShowViewModel)(this.DataContext)).ComboBoxCategoryName = (comboBox.SelectedItem as CategoryModel).Category_name;
                Console.WriteLine(((ProductShowViewModel)(this.DataContext)).ComboBoxCategoryName);

                if ((comboBox.SelectedItem as CategoryModel).Category_name !=
                ((ProductShowViewModel)(this.DataContext)).SelectedProduct.Category_name &&
                (comboBox.SelectedItem as CategoryModel).Category_name != "직접입력")
                {
                    //Console.WriteLine("다르다");

                    ((ProductShowViewModel)(this.DataContext)).SelectedProduct.Category_name = (comboBox.SelectedItem as CategoryModel).Category_name;
                    //Console.WriteLine(((ProductShowViewModel)(this.DataContext)).SelectedProduct.Category_name);
                }
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            var cmb = sender as ComboBox;

            if(e.Key == Key.Enter)
            {
                ((ProductShowViewModel)(this.DataContext)).AddNewCategory(cmb.Text);
                ((ProductShowViewModel)(this.DataContext)).SelectedProduct.Category_name = cmb.Text;
                ((ProductShowViewModel)(this.DataContext)).EditProduct();
            } 
        }

        private void OnDropDownOpened(object sender, EventArgs e)
        {
            isComboBoxDropDownOpened = true;

            var deptModelObject = deptName_ComboBox1.SelectedValue as DeptModel;
            var deptNameText = deptModelObject.Dept_name; // 콤보박스에서 선택한 부서명
            var temp = Ioc.Default.GetService<ProductShowViewModel>();
            var userDept = temp.Depts[(int)App.nurse_dto.Dept_id - 1];  // 현재 사용자 소속 부서 객체
            var userDeptName = userDept.Dept_name;

            if (App.nurse_dto.Nurse_auth.Equals("ADMIN"))
            {
                if (isComboBoxDropDownOpened)
                {

                    if (deptNameText.Equals(userDeptName) || userDeptName == null)
                    {
                        Console.WriteLine(userDeptName + "같은 부서일때");
                        buttonColumn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Console.WriteLine(userDeptName + "다른 부서일때");
                        buttonColumn.Visibility = Visibility.Hidden;
                    }
                }
            }
        }

        private void DataGridCheckboxClick(object sender, RoutedEventArgs e)
        {
            if (DataGridCheckbox.IsChecked == true)
            {
                //DataAndGraphGrid.ColumnDefinitions.Add(DataGridColumn);
                DataGridColumn.Width = new GridLength(1.8, GridUnitType.Star);
            }
            else
            {
                DataGridColumn.Width = new GridLength(0);
            }
        }

        private void GraphCheckboxClick(object sender, RoutedEventArgs e)
        {
            if (GraphCheckbox.IsChecked == true)
            {
                GraphColumn.Width = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                GraphColumn.Width = new GridLength(0);
            }
        }

        private void GraphCheckboxUnChecked(object sender, RoutedEventArgs e)
        {
            GraphCard.Visibility = Visibility.Visible;
        }



    }//class

    public class IsLesserThanConverter : IValueConverter
    {//Red
        public static readonly IValueConverter Instance = new IsLesserThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool checkBool = false;

            if (value != null && targetType != null)
            {
                int intValue = (int)value;//남은 일수
                int compareToValue = Int32.Parse(parameter.ToString());

                checkBool = intValue < compareToValue;
            }

            return checkBool;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsEqualOrLessGreaterThanConverter : IValueConverter
    {//Yellow
        public static readonly IValueConverter Instance = new IsEqualOrLessGreaterThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool checkBool = false;

            if (value != null && targetType != null)
            {
                int intValue = (int)value;//남은 일수
                int compareToValue = Int32.Parse(parameter.ToString());

                checkBool = ((intValue > compareToValue) && (intValue - compareToValue < 3))
                || (intValue == compareToValue);
            }

            return checkBool;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsGreaterThanConverter : IValueConverter
    {//Green
        public static readonly IValueConverter Instance = new IsGreaterThanConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool checkBool = false;

            if (value != null && targetType != null)
            {
                int intValue = (int)value;//남은 일수
                int compareToValue = Int32.Parse(parameter.ToString());

                checkBool = (intValue > compareToValue) && (intValue - compareToValue > 3);
            }

            return checkBool;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



}//namespace
