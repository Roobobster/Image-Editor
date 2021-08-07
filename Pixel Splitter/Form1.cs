using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Splitter
{
    public partial class Form1 : Form
    {
        ImageEditor imageSplitter;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            

        }

        private void Grey_Click(object sender, EventArgs e)
        {

        }


        private void UseNewImage()
        {
            OpenFileDialog fileFinder = new OpenFileDialog();
            fileFinder.ShowDialog();
            Image testImage = Image.FromFile(fileFinder.FileName);
            pbOriginal.Image = testImage;
            imageSplitter = new ImageEditor(testImage);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool withColour = false;
            if (ckbWithColour.Checked)
            {
                withColour = true;
            }
            UseNewImage();

            Bitmap redImage = imageSplitter.CreateRedPixelImage();
            pbRed.Image = redImage;
            Application.DoEvents();

            Bitmap blueImage = imageSplitter.CreateBluePixelImage();
            pbBlue.Image = blueImage;
            Application.DoEvents();

            Bitmap greenImage = imageSplitter.CreateGreenPixelImage();
            pbGreen.Image = greenImage;
            Application.DoEvents();

            Bitmap greyImage = imageSplitter.CreateGreyPixelImage();
            pbGrey.Image = greyImage;
            Application.DoEvents();


            // float.Parse(txtErrorRatio1.Text)
            Bitmap contrastImageOne = imageSplitter.CreateHorizontalContrastPixelImage(int.Parse(txtContrast1.Text), withColour, float.Parse(txtErrorRatio1.Text));
            contrastImageOne = imageSplitter.CreateHorizontalContrastPixelImage(int.Parse(txtContrast1.Text), withColour, float.Parse(txtErrorRatio1.Text));
            //contrastImageOne = imageSplitter.CreatePrimaryRGBImage();
            pbContrastOne.Image = contrastImageOne;
            Application.DoEvents();

            imageSplitter = new ImageEditor(contrastImageOne);

            //imageSplitter = new PixelImage(redImage);
            Bitmap contrastImageTwo = imageSplitter.CreateVeritcalContrastPixelImage(int.Parse(txtContrast2.Text), withColour, float.Parse(txtErrorRatio2.Text));
            contrastImageTwo = imageSplitter.CreateHighContrastPixelImage(2,true, 0.5f);
            pbContrastTwo.Image = contrastImageTwo;
            Application.DoEvents();


            //imageSplitter = new PixelImage(blueImage);
            Bitmap contrastImageThree = imageSplitter.CreateHighContrastPixelImage(int.Parse(txtContrast3.Text), withColour, float.Parse(txtErrorRatio3.Text));
            contrastImageThree = imageSplitter.CreateHighContrastPixelImage(2, true, 0.2f);
            pbContrastThree.Image = contrastImageThree;
            Application.DoEvents();


            //imageSplitter = new PixelImage(greenImage);
            Bitmap contrastImageFour = imageSplitter.CreateHighContrastPixelImage(int.Parse(txtContrast4.Text), withColour, float.Parse(txtErrorRatio4.Text));
            contrastImageFour = imageSplitter.CreateHighContrastPixelImage(2, true, 0.1f);
            pbContrastFour.Image = contrastImageFour;
            Application.DoEvents();



            //imageSplitter = new PixelImage(greyImage);
            Bitmap contrastImageFive = imageSplitter.CreateHighContrastPixelImage(int.Parse(txtContrast5.Text), withColour, float.Parse(txtErrorRatio5.Text));
            contrastImageFour = imageSplitter.CreateHighContrastPixelImage(1, true, 0);
            pbContrastFive.Image = contrastImageFive;
            Application.DoEvents();


        }

        private void txtContrast1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
