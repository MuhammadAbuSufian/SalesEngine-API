using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;


namespace Brcode.WinForm
{
    public partial class GenerteBrcode : Form
    {
        public GenerteBrcode()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {

            string barcode = textBox1.Text;

            Bitmap bitmap = new Bitmap(barcode.Length * 40, 150);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Font font = new Font("IDAutomationHC39M", 20);
                PointF point = new PointF(2f, 2f);
                SolidBrush black = new SolidBrush(Color.Black);
                SolidBrush white  = new SolidBrush(Color.White);

                graphics.FillRectangle(white, 0, 0, bitmap.Width, bitmap.Height);
                graphics.DrawString("*"+ barcode + "*", font, black, point);
            }            

            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                pictureBox1.Image = bitmap;
                pictureBox1.Height = bitmap.Height;
                pictureBox1.Width = bitmap.Width;
            }

            bitmap.Save(@"D:/" + DateTime.Now.Ticks + ".png", ImageFormat.Png);
        }
    }
}
