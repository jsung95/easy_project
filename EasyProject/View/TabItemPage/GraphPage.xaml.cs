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


namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// GraphPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GraphPage : Page
    {
        public String userDept = null;
        public GraphPage()
        {
            InitializeComponent();

            deptName_ComboBox1.SelectedIndex = (int)App.nurse_dto.Dept_id - 1;



        }


    }
}
