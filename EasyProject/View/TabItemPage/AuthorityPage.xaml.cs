using System;
using System.Collections.Generic;
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

namespace EasyProject.View
{
    /// <summary>
    /// AuthorityPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AuthorityPage : Page
    {
        public AuthorityPage()
        {
            InitializeComponent();
        }
        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            dataGrid.Items.Add(new Member("테스트1", "SADF1", "내과"));
            dataGrid.Items.Add(new Member("테스트2", "SADF2", "내과"));
            dataGrid.Items.Add(new Member("테스트3", "SADF3", "내과"));
            dataGrid.Items.Add(new Member("테스트4", "SADF4", "내과"));
            dataGrid.Items.Add(new Member("테스트5", "SADF5", "내과"));
            dataGrid.Items.Add(new Member("테스트6", "SADF6", "내과"));
            dataGrid.Items.Add(new Member("테스트7", "SADF7", "내과"));
            dataGrid.Items.Add(new Member("테스트8", "SADF8", "내과"));
            dataGrid.Items.Add(new Member("테스트9", "SADF9", "내과"));

        }
        private void dataGrid_Loaded2(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            dataGrid.Items.Add(new Member("테스트10", "SADF10", "내과0"));
            dataGrid.Items.Add(new Member("테스트20", "SADF20", "내과0"));
            dataGrid.Items.Add(new Member("테스트30", "SADF30", "내과0"));
            dataGrid.Items.Add(new Member("테스트40", "SADF40", "내과0"));
            dataGrid.Items.Add(new Member("테스트50", "SADF50", "내과0"));
            dataGrid.Items.Add(new Member("테스트60", "SADF60", "내과0"));
            dataGrid.Items.Add(new Member("테스트70", "SADF70", "내과0"));
            dataGrid.Items.Add(new Member("테스트80", "SADF80", "내과0"));
            dataGrid.Items.Add(new Member("테스트90", "SADF90", "내과0"));

        }
        public class Lists
        {
            public Lists()
            {
                var filteredList = MemberList;
            }
            public List<Member> MemberList { get; set; } = GetMembers();

            public static List<Member> GetMembers()
            {
                var list = new List<Member>();
                list.Add(new Member("테스트222", "SADF222", "내과"));
                list.Add(new Member("테스트322", "SADF322", "내과"));
                list.Add(new Member("테스트422", "SADF422", "내과"));
                list.Add(new Member("테스트522", "SADF522", "내과"));
                list.Add(new Member("테스트622", "SADF622", "내과"));
                list.Add(new Member("테스트722", "SADF722", "내과"));
                return list;
            }
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e) //로그인 버튼 클릭 시
        {

        }

        public class Member
        {


            public Member(string num, string name, string phoneNum)
            {
                memberNum = num;
                memberName = name;
                memberPhoneNum = phoneNum;
            }

            public string memberNum           //바인딩 Path와 일치하는 Property
            {
                get;
                set;
            }

            public string memberName
            {
                get;
                set;
            }

            public string memberPhoneNum
            {
                get;
                set;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("버튼 누름");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void moveBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void movelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void moverBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checked1(object sender, RoutedEventArgs e)
        {

        }
        private void checked2(object sender, RoutedEventArgs e)  //체크박스시 체크된 이름/부서/id 포함 왼쪽클릭시 체크박스에 체크된 데이터 오른쪽테이블로 옮김
        {

        }
    }

}
