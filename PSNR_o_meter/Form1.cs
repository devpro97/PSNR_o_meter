using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PSNR_o_meter.Core;

namespace PSNR_o_meter
{
    public partial class MainScreen : Form
    {
        Calculator calculator;
        PictureMeter metric;

        string[] allowedExt = { ".bmp", ".jpg", ".gif", ".jpeg", ".png", ".dib", ".jpe" };

        public MainScreen()
        {
            InitializeComponent();
            this.metric = new PSNRmetric();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            calculator = new Calculator(new Bitmap(pictureBox1.Image), new Bitmap(pictureBox2.Image));
            var pictureMetrics = calculator.CalculatePSRN(metric);
            foreach(var metric in pictureMetrics)
            {
                richTextBox1.Text += metric.ToString() + Environment.NewLine;
            }
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            string filename;
            Image image;
            if (GetPicture(out filename))
            {
                image = Image.FromFile(filename);
                pictureBox1.Image = image;
            }
        }
        private void PictureBox2_Click(object sender, EventArgs e)
        {
            string filename;
            Image image;
            if (GetPicture(out filename))
            {
                image = Image.FromFile(filename);
                pictureBox2.Image = image;
            }
        }

        #region worker metods
        private bool GetPicture(out string filename)
        {
            filename = string.Empty;
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "Image Files(*.BMP; *.JPG, .jpeg, *.GIF)| *.BMP; *.JPG; *.GIF; *.jpeg; *.png; *.dib; *.jpe | All files(*.*) | *.*",
                Multiselect = false
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filename = dialog.FileName;
                var extension = Path.GetExtension(filename).ToLower();
                return this.allowedExt.Contains<string>( extension );
            }
            return false;
        }

        private void ShowWarning(string warning, uint hexcolour = 0xFFFF6347) //Tomato color
        {
            tbWarnings.Visible = true;
            tbWarnings.BackColor = Color.FromArgb(unchecked((int)hexcolour));
            tbWarnings.Text = warning;
            this.Click += WarningShown;
        }

        private void WarningShown(object sender, EventArgs e)
        {
            tbWarnings.Visible = false;
            tbWarnings.Text = string.Empty;
            this.Click -= WarningShown;
        }
        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbWarnings_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}