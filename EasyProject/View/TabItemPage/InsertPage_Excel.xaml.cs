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

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(dragEnterHandler);
            this.DragLeave += new DragEventHandler(dragDropHandler);

        }
        void dragEnterHandler(object sender, DragEventArgs e){
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
                e.Effects = DragDropEffects.Copy;
        }

        void dragDropHandler(object sender, DragEventArgs e){
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) file_TxtBox.Text = file;
        }
    }
}
