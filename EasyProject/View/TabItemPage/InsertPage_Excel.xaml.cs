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
using System.IO;
using Microsoft.Win32;

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// InsertPage_Excel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InsertPage_Excel : Page
    {
        public InsertPage_Excel()
        {
            InitializeComponent();

            fileUploadBtn.Click += fileUploadBtn_Click;

            //this.DragEnter += new DragEventHandler(dragEnterHandler);
            //this.DragLeave += new DragEventHandler(dragDropHandler);

        }
        void dragEnterHandler(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
                e.Effects = DragDropEffects.Copy;
        }

        void dragDropHandler(object sender, DragEventArgs e){
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                //FileUploadPageFunction uploadPFunction = new FileUploadPageFunction(file);
                //NavigationService.Navigate(uploadPFunction);
            }
        }

        private void fileUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "엑셀 파일 (*.xls)|*.xls|엑셀 파일 (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == true)
            {

                //MessageBox.Show(System.IO.Path.GetFullPath(openFileDialog.FileName));
                FileUploadPageFunction uploadPFunction = new FileUploadPageFunction(openFileDialog);
                NavigationService.Navigate(uploadPFunction);
            }
        }
        private void fileReader()
        {
            
        }

    }
}
