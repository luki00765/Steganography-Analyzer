using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Steganography_Analyzer
{
	class CompareHelper
	{
		public static int differentPixels;
		public static double psnr;
		public static WriteableBitmap ComapreImages(BitmapImage bmp1, BitmapImage bmp2)
		{
			int bmp1_Width = bmp1.PixelWidth;
			int bmp1_Height = bmp1.PixelHeight;
			int bmp2_Width = bmp2.PixelWidth;
			int bmp2_Height = bmp2.PixelHeight;

			int stride1 = bmp1_Width * 4;
			int stride2 = bmp2_Width * 4;

			int size1 = bmp1_Height * stride1;
			int size2 = bmp2_Height * stride2;

			byte[] pixels1 = new byte[size1];
			byte[] pixels2 = new byte[size2];

			bmp1.CopyPixels(pixels1, stride1, 0);
			bmp2.CopyPixels(pixels2, stride2, 0);

			byte[] pixelsResult = new byte[size1];

			differentPixels = 0;
			double mse = 0;
			int index = 0;
			for (int i = 0; i < pixelsResult.Length; )
			{
				if (i % 4 == 0)
				{
					index = i;
					mse += Math.Pow(pixels1[i] - pixels2[i], 2);
					mse += Math.Pow(pixels1[i + 1] - pixels2[i + 1], 2);
					mse += Math.Pow(pixels1[i + 2] - pixels2[i + 2], 2);
				}


				if (pixels1[i] == pixels2[i])
				{
					pixelsResult[i] = pixels1[i];
					//alpha
					if (i % 4 == 3) //DODAJ ALPHA
					{
						pixelsResult[i] = 85;
					}
					i++;
				}
				else
				{
					//red
					pixelsResult[index] = 0;
					pixelsResult[index + 1] = 0;
					pixelsResult[index + 2] = 255;
					pixelsResult[index + 3] = 255;
					differentPixels++;
					i = index + 4;
				}
			}

			mse /= (int)bmp1.Width * (int)bmp1.Height * 3;
			if (mse > 0)
			{
				psnr = 20 * Math.Log10(255) - 10 * Math.Log10(mse);
			}
			else if (differentPixels == 0)
			{
				psnr = 0;
			}
			else
			{
				psnr = 20 * Math.Log10(255);
			}

			WriteableBitmap writeBitmapResult = new WriteableBitmap(new FormatConvertedBitmap(bmp1, PixelFormats.Bgra32, null, 0));
			var rect = new Int32Rect(0, 0, bmp1_Width, bmp1_Height);
			writeBitmapResult.WritePixels(rect, pixelsResult, stride1, 0);
			return writeBitmapResult;
		}
	}
}
