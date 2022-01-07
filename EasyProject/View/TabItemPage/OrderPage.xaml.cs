using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// OrderPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderPage : Page
    {
        private int index = 1;
        
        public OrderPage()
        {
            InitializeComponent();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            //id_TxtBox.Text = null;
            //dept_TxtBox = null;
            //phone_TxtBox.Text = null;
            request_TxtBox.Text = null;
            capacity_TxtBox.Text = null;
            amount_TxtBox.Text = null;
            company_TxtBox.Text = null;
            memo_TxtBox.Text = null;

        }

        //인쇄 버튼
        private void printBtn_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog().GetValueOrDefault(false))
            {
                printDialog.PrintVisual(NewPlaceOrder, "PlaceOrder");
            }

        }

        //pdf 버튼
        private void pdfBtn_Click(object sender, RoutedEventArgs e)
        {
            //이미지로 저장(스크린 샷)
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)NewPlaceOrder.ActualWidth, (int)NewPlaceOrder.ActualHeight, 74, 74, PixelFormats.Pbgra32);
            rtb.Render(NewPlaceOrder);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);

            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            string stampFileName = @"C:\Users\user\Desktop\"  + $"신규발주신청서{index}.png";
            image.Save(stampFileName);
           

            

            //sharpPDF이용해서 넣기
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            XImage im = XImage.FromFile(@"C:\Users\user\Desktop\" + $"신규발주신청서{index}.png");

            gfx.DrawImage(im, -150, 100, 700, 450);

    
            
            

            // Save the document...
            string filename = @"C:\Users\user\Desktop\" + $"신규발주신청서{index}.pdf";
            document.Save(filename);
            MessageBox.Show($"신규발주신청서{index}.pdf 생성");
            index++;
            Process.Start(filename);
            //var windows = new Window();

            //windows.ShowDialog();

        }


    }


}
