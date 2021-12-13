using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Win32;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Controls;
using System.Windows.Data;

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// FileUploadPageFunction.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FileUploadPageFunction : PageFunction<String>
    {
        private OpenFileDialog openFileDialog;
        public FileUploadPageFunction(OpenFileDialog openFileDialog)
        {
            InitializeComponent();

            this.openFileDialog = openFileDialog;
            SetFileNameTxtBlock();
            SetDataGrid();

            fileUploadBtn.Click += fileUploadBtn_Click;
        }

        private string GetFileName(OpenFileDialog openFileDialog)
        {
            return System.IO.Path.GetFileName(openFileDialog.FileName);
        }
        private void SetFileNameTxtBlock()
        {
           fileNameTxtbox.Text = GetFileName(this.openFileDialog);
        }
        private void SetDataGrid()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            try
            {
                string str;
                int rCnt = 0; // 열 갯수
                int cCnt = 0; // 행 갯수

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(this.openFileDialog.FileName);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1); // 첫번째 시트를 가져 옴.

                range = xlWorkSheet.UsedRange; // 가져 온 시트의 데이터 범위 값

                for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                    {
                        str = range.Cells[rCnt, cCnt].Text.ToString();
                        MessageBox.Show(str);
                    }
                }

                xlWorkBook.Close(true, null, null);
                xlApp.Quit();

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void fileUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            //이전 페이지로 돌아가기 (PageFunction 객체 생성한 페이지)
            OnReturn(new ReturnEventArgs<string>()); 
        }

    }
}
