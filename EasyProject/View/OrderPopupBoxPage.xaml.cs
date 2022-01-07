﻿using PdfSharp.Drawing;
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
    /// OrderPopupBoxPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class OrderPopupBoxPage : Page
    {
        private int index = 1;

        public OrderPopupBoxPage()
        {
            InitializeComponent();

            
        }
        public void PrintBtn(object e, RoutedEventArgs arg)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog().GetValueOrDefault(false))
            {
                printDialog.PrintVisual(this, this.Title);
            }
        }

        //초기화버튼
        public void resetBtn_Click(object e, RoutedEventArgs arg)
        {
            capacity_TxtBox.Text = null;
            amount_TxtBox.Text = null;
            company_TxtBox.Text=null;
            memo_TxtBox.Text= null;
        }

        private void pdfBtn_Click(object sender, RoutedEventArgs e)
        {
            //이미지로 저장(스크린 샷)
            RenderTargetBitmap rtb = new RenderTargetBitmap((int)PlaceOrder.ActualWidth, (int)PlaceOrder.ActualHeight, 74, 74, PixelFormats.Pbgra32);
            rtb.Render(PlaceOrder);
            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));
            MemoryStream stream = new MemoryStream();
            png.Save(stream);

            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            string stampFileName = @"C:\Users\user\Desktop\" + $"발주신청서{index}.png";
            image.Save(stampFileName);




            //sharpPDF이용해서 넣기
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            XImage im = XImage.FromFile(@"C:\Users\user\Desktop\" + $"발주신청서{index}.png");

            gfx.DrawImage(im, 20, 100, 700, 450);




            // Save the document...
            string filename = @"C:\Users\user\Desktop\" + $"발주신청서{index}.pdf";
            document.Save(filename);
            MessageBox.Show($"발주신청서{index}.pdf 생성");
            index++;
            Process.Start(filename);
        }
    }

}