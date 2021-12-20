using System;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Win32;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Controls;
using System.Windows.Data;
using EasyProject.ViewModel;
using EasyProject.Model;
using System.Collections.Generic;

namespace EasyProject.View.TabItemPage
{
    /// <summary>
    /// FileUploadPageFunction.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FileUploadPageFunction : PageFunction<String>
    {
        private OpenFileDialog openFileDialog;
        private List<ProductShowModel> excelProductList;

        public FileUploadPageFunction(OpenFileDialog openFileDialog)
        {
            InitializeComponent();
            this.DataContext = new ProductViewModel();

            var bindingParameter = new Binding("OpenFileDialog");
            bindingParameter.Source = openFileDialog.FileName;
            HiddenControl.Visibility = Visibility.Hidden;
            HiddenControl.SetBinding(TextBlock.TextProperty, bindingParameter);

            excelProductList = new List<ProductShowModel>();
            fileUploadBtn.Click += fileUploadBtn_Click;

            this.openFileDialog = openFileDialog;
            SetFileNameTxtBlock();

            ExcelReader();
        }

        private string GetFileName(OpenFileDialog openFileDialog)
        {
            return System.IO.Path.GetFileName(openFileDialog.FileName);
        }
        private void SetFileNameTxtBlock()
        {
            fileNameTxtbox.Text = GetFileName(this.openFileDialog);
        }


        private void ExcelReader()
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            try
            {
                int rCnt = 0; // 열 갯수
                int cCnt = 0; // 행 갯수

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Open(this.openFileDialog.FileName);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1); // 첫번째 시트를 가져 옴.

                range = xlWorkSheet.UsedRange; // 가져 온 시트의 데이터 범위 값

                for (rCnt = 2; rCnt <= range.Rows.Count; rCnt++)
                {
                    var product = new ProductShowModel();
                    for (cCnt = 1; cCnt <= range.Columns.Count; cCnt++)
                    {
                        // 열과 행에 해당하는 데이터를 문자열로 반환
                        //str = (string)(range.Cells[rCnt, cCnt] as Excel.Range).Value2;
                        product = SetProductObject(ref product, ref range, rCnt, cCnt);
                        
                        //MessageBox.Show(product.Prod_code+".."+product.Prod_name+
                        //   ".." + product.Category_name+".."+product.Prod_expire
                        //   + ".." + product.Prod_price + ".." + product.Prod_total);  
                    }
                    excelProductList.Add(product);
                    MessageBox.Show(excelProductList.Count+"개");

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
        private ProductShowModel SetProductObject(ref ProductShowModel Product, ref Excel.Range range, int rCnt, int cCnt)
        {
            
            string headerText = range.Cells[1, cCnt].Text.ToString();

            switch (headerText)
            {
                case "제품코드":
                    Product.Prod_code = (string)GetCellText(range, rCnt, 1);
                    break;

                case "제품명":
                    Product.Prod_name = (string)GetCellText(range, rCnt, 2);
                    break;

                case "품목/종류":
                    Product.Category_name = (string)GetCellText(range, rCnt, 3);
                    break;

                case "유통기한":
                    Product.Prod_expire = Convert.ToDateTime(GetCellText(range, rCnt, 4));
                    break;

                case "가격":
                    Product.Prod_price = Int32.Parse(GetCellText(range, rCnt, 5));
                    break;

                case "수량":
                    Product.Prod_total = Int32.Parse(GetCellText(range, rCnt, 6));
                    break;

            }
            return Product;
        }
        private string GetCellText(Excel.Range range, int rCnt, int cCnt)
        {
            string cellText = range.Cells[rCnt, cCnt].Text.ToString();
            return cellText;
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
