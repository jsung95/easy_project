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
using Oracle.ManagedDataAccess.Client;

namespace EasyProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            dbConnection();
          
        }
        private void dbConnection()
        {
            // Oracle 접속 변수 설정
            var user = "ADMIN";
            var password = "Oracle12345!";
            var ds = "db202112031025_high";

            var connectionString = $"User Id={user};Password={password};Data Source={ds};";

            // Oracle 접속
            OracleConnection conn = new OracleConnection(connectionString);

            Console.WriteLine("DB 연결 시작");
            conn.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM emp WHERE empno >= :num";
            cmd.Parameters.Add(new OracleParameter("num", 5000));

            OracleDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string ename = rdr["ENAME"] as string;

                Console.WriteLine($"{ename}");
            }

            Console.WriteLine("DB 연결 해제");
            conn.Close();

        }
    }
}
