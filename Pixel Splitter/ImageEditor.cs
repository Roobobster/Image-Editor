using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pixel_Splitter
{
    class ImageEditor
    {
        Bitmap ImageBitMap { get; set; }
        int[,] RedPixelArray { get; set; }
        int[,] BluePixelArray { get; set; }
        int[,] GreenPixelArray { get; set; }

        int[,] AlphaPixelArray { get; set; }


        //Every time a pixel image splitter is created it initialises the pixel arrays
        public ImageEditor(Image image)
        {
            ImageBitMap = new Bitmap(image);
            int width = ImageBitMap.Width;
            int height = ImageBitMap.Height;

	    //Fundementals of the image into pixel arrays
            RedPixelArray = new int[width, height];
            BluePixelArray = new int[width, height];
            GreenPixelArray = new int[width, height];
            AlphaPixelArray = new int[width, height];

            //Loops for every pixel in the image from the x valuse to the y values
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //Sets the image pixels RGB values into the array that is equivalant to the image pixels
                    //Red Pixels
                    RedPixelArray[x, y] = ImageBitMap.GetPixel(x, y).R;
                    //Blue Pixels
                    BluePixelArray[x, y] = ImageBitMap.GetPixel(x, y).B;
                    //Green Pixels
                    GreenPixelArray[x, y] = ImageBitMap.GetPixel(x, y).G;
                    //Alpha Pixels
                    AlphaPixelArray[x, y] = ImageBitMap.GetPixel(x, y).A;
                }
            }

        }

        #region RGBA Colours Of Image.
        //Turns an image into a red image equivalant which changes the image into it's red pixel values only.
        public Bitmap CreateRedPixelImage()
        {
            Bitmap redImage = new Bitmap(ImageBitMap);
            for (int x = 0; x < RedPixelArray.GetLength(0); x++)
            {
                for (int y = 0; y < RedPixelArray.GetLength(1); y++)
                {
                    Color red = Color.FromArgb(AlphaPixelArray[x,y], RedPixelArray[x, y], 0, 0);
                    redImage.SetPixel(x, y, red);
                }
            }

            return redImage;
        }

        //Turns an image into a blue image equivalant which changes the image into it's blue pixel values only.
        public Bitmap CreateBluePixelImage()
        {
            Bitmap blueImage = new Bitmap(ImageBitMap);
            for (int x = 0; x < BluePixelArray.GetLength(0); x++)
            {
                for (int y = 0; y < BluePixelArray.GetLength(1); y++)
                {
                    Color blue = Color.FromArgb(AlphaPixelArray[x, y], 0, 0, BluePixelArray[x, y]);
                    blueImage.SetPixel(x, y, blue);
                }
            }

            return blueImage;
        }

        //Turns an image into the green image equivalant which changes the image into it's green pixel values only
        public Bitmap CreateGreenPixelImage()
        {
            Bitmap greenImage = new Bitmap(ImageBitMap);
            for (int x = 0; x < GreenPixelArray.GetLength(0); x++)
            {
                for (int y = 0; y < GreenPixelArray.GetLength(1); y++)
                {
                    Color green = Color.FromArgb(AlphaPixelArray[x, y], 0, GreenPixelArray[x, y], 0);
                    greenImage.SetPixel(x, y, green);
                }
            }

            return greenImage;
        }

        //Turns the image into a aplha equivalnt image which represents the alpha of an image
        public Bitmap CreateAlphaPixelImage()
        {
            Bitmap alphaImage = new Bitmap(ImageBitMap);
            for (int x = 0; x < GreenPixelArray.GetLength(0); x++)
            {
                for (int y = 0; y < GreenPixelArray.GetLength(1); y++)
                {
                    Color alpha = Color.FromArgb(255, AlphaPixelArray[x, y], AlphaPixelArray[x, y], AlphaPixelArray[x, y]);
                    alphaImage.SetPixel(x, y, alpha);
                }
            }

            return alphaImage;
        }

        #endregion

        #region Contrast Images

        //Turns an image into a grey scale version of the image
        public Bitmap CreateGreyPixelImage()
        {
            Bitmap greyImage = new Bitmap(ImageBitMap);
            for (int x = 0; x < GreenPixelArray.GetLength(0); x++)
            {
                for (int y = 0; y < GreenPixelArray.GetLength(1); y++)
                {
                    int averageColour = (RedPixelArray[x, y] + GreenPixelArray[x, y] + BluePixelArray[x, y]) / 3;
                    Color grey = Color.FromArgb(AlphaPixelArray[x, y], averageColour, averageColour, averageColour);
                    greyImage.SetPixel(x, y, grey);
                }
            }

            return greyImage;
        }

        //Contrast From Top To Bottom
        //Causes white gaps at the bottom of the image due to the way this comparrisions are made.
        public Bitmap CreateVeritcalContrastPixelImage(int comparisions, bool withColour, float errorPercentage)
        {
            Bitmap contrastImage = new Bitmap(ImageBitMap);

            int[] averageColoursBelow = new int[comparisions];
            //loops for width
            for (int x = 0; x < GreenPixelArray.GetLength(0); x++)
            {
                //Loops for height
                for (int y = 0; y < GreenPixelArray.GetLength(1); y++)
                {
                    int averageColour = (RedPixelArray[x, y] + GreenPixelArray[x, y] + BluePixelArray[x, y])  / 3;
                   
                    if (y >= GreenPixelArray.GetLength(1) - comparisions)
                    {
                        for (int i = 0; i < comparisions; i++)
                        {
                            averageColoursBelow[i] = 0;
                        }                       

                    }
                    else
                    {
                        for (int i = 1; i <= comparisions; i++)
                        {
                            averageColoursBelow[i-1] = (RedPixelArray[x, y + i] + GreenPixelArray[x, y + i] + BluePixelArray[x, y + i]) / 3;
                        }                 

                    }


                    Color contrast = Color.Red;
                    int counter = 0;
                    for (int i = 0; i < comparisions; i++)
                    {
                        if (averageColour > averageColoursBelow[i])
                        {
                            contrast = Color.FromArgb(AlphaPixelArray[x, y], averageColour, averageColour, averageColour);
                            if (withColour)
                            {
                                contrast = Color.FromArgb(AlphaPixelArray[x, y], RedPixelArray[x, y], GreenPixelArray[x, y], BluePixelArray[x, y]);
                            }
                        }
                        else
                        {
                                counter++;

                                if (counter >= comparisions * errorPercentage)
                                {
                                    contrast = Color.FromArgb(AlphaPixelArray[x, y], 0, 0, 0);
                                    break;
                                }                       
                        }

                    }

                    contrastImage.SetPixel(x, y, contrast);
                }
            }

            return contrastImage;
        }


        //Creates a contrast image that is horizontal based 
        public Bitmap CreateHorizontalContrastPixelImage(int comparisions, bool withColour, float errorPercentage)
        {
            Bitmap contrastImage = new Bitmap(ImageBitMap);

            int[] averageColoursBelow = new int[comparisions];
            //loops for width
            for (int y = 0; y < GreenPixelArray.GetLength(1); y++)
            {
                //Loops for height
                for (int x = 0; x < GreenPixelArray.GetLength(0); x++)
                {
                    int averageColour = (RedPixelArray[x, y] + GreenPixelArray[x, y] + BluePixelArray[x, y]) / 3;

                    if (x >= GreenPixelArray.GetLength(0) - comparisions)
                    {
                        for (int i = 0; i < comparisions; i++)
                        {
                            averageColoursBelow[i] = 0;
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= comparisions; i++)
                        {
                            averageColoursBelow[i - 1] = (RedPixelArray[x + i, y] + GreenPixelArray[x + i, y ] + BluePixelArray[x + i, y ]) / 3;
                        }
                    }

                    Color contrast = Color.Red;
                    int counter = 0;
                    for (int i = 0; i < comparisions; i++)
                    {
                        if (averageColour > averageColoursBelow[i])
                        {
                            contrast = Color.FromArgb(AlphaPixelArray[x, y], averageColour, averageColour, averageColour);
                            if (withColour)
                            {
                                contrast = Color.FromArgb(AlphaPixelArray[x, y], RedPixelArray[x, y], GreenPixelArray[x, y], BluePixelArray[x, y]);
                            }
                        }
                        else
                        {
                            counter++;

                            if (counter >= comparisions * errorPercentage)
                            {
                                contrast = Color.FromArgb(AlphaPixelArray[x, y], 0, 0, 0);
                                break;
                            }

                        }

                    }
                    contrastImage.SetPixel(x, y, contrast);
                }
            }

            return contrastImage;
        }


        //Creates a pixel image that is a image showing the contrast difference in a circle around every pixel and then it only shows
        //the pixels that are the brightest in the surrounding area and essentially it displays the contrast in the image.
        public Bitmap CreateHighContrastPixelImage(int compares, bool withColour, float errorPercentage)
        {
            
            int comparisions = compares * 8;
            Bitmap contrastImage = new Bitmap(ImageBitMap);
            
            int diameter = compares * 2 + 1;
            int middleX = diameter / 2;
            int[,] averageColoursAround= new int[diameter, diameter];
            //loops for width
            for (int x = 0; x < GreenPixelArray.GetLength(0); x++)
            {
                //Loops for height
                for (int y = 0; y < GreenPixelArray.GetLength(1); y++)
                {
                    int averageColour = (RedPixelArray[x, y] + GreenPixelArray[x, y] + BluePixelArray[x, y]) / 3;
                    //Loops for every pixel around the selected pixel
                    for (int inx = 0; inx < diameter; inx++)
                    {
                        for (int iny = 0; iny < diameter; iny++)
                        {
                            int aroundXIndex = x - compares + inx;
                            int aroundYIndex = y - compares + iny;
                            if (aroundXIndex < 0 || aroundYIndex < 0 || aroundXIndex >= RedPixelArray.GetLength(0) || aroundYIndex >= RedPixelArray.GetLength(1))
                            {
                                averageColoursAround[inx, iny] = 0;
                            }
                            else
                            {
                                
                                averageColoursAround[inx, iny] = (RedPixelArray[aroundXIndex, aroundYIndex] + GreenPixelArray[aroundXIndex, aroundYIndex] + BluePixelArray[aroundXIndex, aroundYIndex]) / 3;
                            }
                            
                        }
                    }

                    Color contrast = Color.Red;
                    int counter = 0;
                    //Reloops fro every pixel around the selected pixel to check if for the contrast difference.
                    for (int inx = 0; inx < diameter; inx++)
                    {
                        for (int iny = 0; iny < diameter; iny++)
                        {
                            contrast = Color.FromArgb(AlphaPixelArray[x, y], averageColour, averageColour, averageColour);
                            if (withColour)
                            {
                                contrast = Color.FromArgb(AlphaPixelArray[x, y], RedPixelArray[x, y], GreenPixelArray[x, y], BluePixelArray[x, y]);
                            }
                           
                            if (averageColoursAround[inx, iny] >= averageColour)
                            {
                                counter++;

                                if (counter >= diameter * diameter * errorPercentage)
                                {
                                    contrast = Color.FromArgb(AlphaPixelArray[x, y], 0, 0, 0);
                                    inx = diameter;
                                    break;
                                }

                            }                  
                        }
                    }                               
                    contrastImage.SetPixel(x, y, contrast);
                }
            }

            return contrastImage;
        }
        #endregion

        #region RGB Primary
        public Bitmap CreatePrimaryRGBImage()
        {
            Bitmap primaryRGBImage = new Bitmap(ImageBitMap);

            for (int x  = 0;   x < BluePixelArray.GetLength(0); x++)
            {
                for (int y = 0; y < BluePixelArray.GetLength(1); y++)
                {
                    if (BluePixelArray[x,y] > RedPixelArray[x,y])
                    {
                        if (BluePixelArray[x, y] > GreenPixelArray[x, y])
                        {
                            primaryRGBImage.SetPixel(x, y, Color.FromArgb(0, BluePixelArray[x, y], 0));
                        }                    
                        
                    }

                    if (RedPixelArray[x, y] > BluePixelArray[x, y])
                    {
                        if (RedPixelArray[x, y] > GreenPixelArray[x, y])
                        {
                            primaryRGBImage.SetPixel(x, y, Color.FromArgb(RedPixelArray[x,y], 0, 0));
                        }

                    }

                    if (GreenPixelArray[x, y] > RedPixelArray[x, y])
                    {
                        if (GreenPixelArray[x, y] > BluePixelArray[x, y])
                        {
                            primaryRGBImage.SetPixel(x, y, Color.FromArgb(0, 0, GreenPixelArray[x,y]));
                        }

                    }
                }
            }

            return primaryRGBImage;
        }
        #endregion

        #region Distortion

        //Creates a horiztontally blurred image from the inputted image.
        public Bitmap CreateHorizontalBlurImage(int blurAmount)
        {
            Bitmap blurImage = new Bitmap(ImageBitMap);
            int averageColour = 0;
            for (int y = 0; y < BluePixelArray.GetLength(1); y++)
            {
                for (int x = 0; x < BluePixelArray.GetLength(0); x++)
                {
                    averageColour += (RedPixelArray[x, y] + GreenPixelArray[x, y] + BluePixelArray[x, y]) / 3;
                    for (int i = 1; i <= blurAmount; i++)
                    {
                        if (BluePixelArray.GetLength(0) > x + i)
                        {
                            averageColour += (RedPixelArray[x + i, y] + GreenPixelArray[x + i , y] + BluePixelArray[x + i, y]) / 3;
                        }
                    }
                    averageColour /= blurAmount + 1;
                    blurImage.SetPixel(x, y, Color.FromArgb(averageColour, averageColour, averageColour));
                    averageColour = 0;
                }
            }
            return blurImage;

        }
        
        //Creates a vertically blurred image from the inputted image
        public Bitmap CreateVerticalBlurImage(int blurAmount)
        {
            Bitmap blurImage = new Bitmap(ImageBitMap);
            int averageColour = 0;
            for (int x = 0; x < BluePixelArray.GetLength(0); x++)
            {
                for (int y = 0; y < BluePixelArray.GetLength(1); y++)
                {
                    averageColour += (RedPixelArray[x, y] + GreenPixelArray[x, y] + BluePixelArray[x, y]) / 3;
                    for (int i = 1; i <= blurAmount; i++)
                    {
                        if (BluePixelArray.GetLength(1) > y + i)
                        {
                            averageColour += (RedPixelArray[x, y + i] + GreenPixelArray[x, y+ i] + BluePixelArray[x , y + i]) / 3;
                        }
                    }
                    averageColour /= blurAmount + 1;
                    blurImage.SetPixel(x, y, Color.FromArgb(averageColour, averageColour, averageColour));
                    averageColour = 0;
                }
            }

            return blurImage;
        }

        //Creates a circle blurred image around every pixel to produce a blurred image that isn't based on a single direction.
        public Bitmap CreateCircleBlurImage(int blurAmount)
        {
            Bitmap blurImage = new Bitmap(ImageBitMap);
            int averageColour ;
            //Loops for every x pixel
            for (int x = 0; x < BluePixelArray.GetLength(0); x++)
            {
                //Loops for every y pixel
                for (int y = 0; y < BluePixelArray.GetLength(1); y++)
                {
                    averageColour = 0;
                    
                    //Loops around a pixel for x
                    for (int inx = 0; inx < (blurAmount * 2 + 1); inx++)
                    {
                        int xCheck = (x - 1) + inx;
                       
                        if (xCheck >= 0 && xCheck < BluePixelArray.GetLength(0))
                        {
                            //Loops around pixel for y
                            for (int iny = 0; iny < (blurAmount * 2 + 1); iny++)
                            {

                                int yCheck = (y - 1) + iny;
                                if (yCheck >= 0 && yCheck < BluePixelArray.GetLength(0))
                                {
                                    averageColour += (RedPixelArray[xCheck, yCheck] + GreenPixelArray[xCheck, yCheck] + BluePixelArray[xCheck, yCheck]) / 3;
                                    
                                }
                                
                            }
                            
                        }     

                    }
                    //1 3x3
                    //2 5x5
                    //3 7x7
                    //4 9x9
                    //5 11x11
                    //6 13x13
                    averageColour /= (blurAmount *2 + 1) * (blurAmount * 2 + 1);
                    blurImage.SetPixel(x, y, Color.FromArgb(AlphaPixelArray[x, y], averageColour, averageColour, averageColour));
                }
            }

            return blurImage;
        }
        #endregion

        #region Transformations

        public Bitmap CreateRescaledImage(float scaleRatio)
        {

            return null;
        }
        #endregion
    }

}
