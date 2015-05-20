using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Brush = System.Windows.Media.Brushes;

namespace Steganography_Analyzer
{
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : MetroWindow
	{
		/// <summary>
		/// Position to lock for change size in horizontal way (x)
		/// </summary>
		public static readonly DependencyProperty LocationLockHorizontalProperty = DependencyProperty.Register("LocationLockHorizontal", typeof(AlignmentX), typeof(Window));
		/// <summary>
		/// Position to lock for change size in vertical way (y)
		/// </summary>
		public static readonly DependencyProperty LocationLockVerticalProperty = DependencyProperty.Register("LocationLockVertical", typeof(AlignmentY), typeof(Window));
		/// <summary>
		/// Location to lock for change size.
		/// </summary>
		public AlignmentX LocationLockHorizontal
		{
			get { return (AlignmentX)GetValue(LocationLockHorizontalProperty); }
			set { SetValue(LocationLockHorizontalProperty, value); }
		}
		/// <summary>
		/// Location to lock for change size.
		/// </summary>
		public AlignmentY LocationLockVertical
		{
			get { return (AlignmentY)GetValue(LocationLockVerticalProperty); }
			set { SetValue(LocationLockVerticalProperty, value); }
		}

		public void AnimateResize(double changeWidth = 0d, double changeHeight = 0d, double durationMilisec = 200.0)
		{
			Storyboard sb = new Storyboard();

			DoubleAnimation dah;

			if (changeHeight != 0.0)
			{
				dah = new DoubleAnimation();
				dah.From = this.ActualHeight;
				dah.To = this.ActualHeight + changeHeight;
				dah.Duration = new Duration(TimeSpan.FromMilliseconds(durationMilisec));
				dah.AccelerationRatio = 0.4;
				dah.DecelerationRatio = 0.6;
				Storyboard.SetTarget(dah, this);
				Storyboard.SetTargetProperty(dah, new PropertyPath(Window.HeightProperty));
				sb.Children.Add(dah); // this.BeginAnimation(Window.HeightProperty, dah);
			}
			sb.Begin();
		}


		BitmapImage bmp1;
		BitmapImage bmp2;
		WriteableBitmap wbm;

		public MainWindow()
		{
			InitializeComponent();
			Image1.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/noImage.jpg"));
			Image2.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/noImage.jpg"));
			LocationLockHorizontal = AlignmentX.Center; // fix center point when animate resizing
			LocationLockVertical = AlignmentY.Bottom; // lock bottom corner of application
			prompt.Text = "Prompt: Firstly you should load Images";
			prompt.Foreground = Brush.DarkRed;
		}

		private void CompareImages(object sender, RoutedEventArgs e)
		{
			if (bmp1 != null && bmp2 != null)
			{
				prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom1to0(1));

				if (bmp1.PixelWidth == bmp2.PixelWidth && bmp1.PixelHeight == bmp2.PixelHeight)
				{
					this.AnimateResize(0, 250.0, 1000.0);
					Compare.IsEnabled = false;
					wbm = CompareHelper.ComapreImages(bmp1, bmp2);
					DifferentImage.Source = ConvertWriteableBitmapToBitmapImage(wbm);
					DifferentPixels.Text = "Different Pixels: " + CompareHelper.differentPixels;
					PSNR.Text = "PSNR: " + CompareHelper.psnr;
					Clear.Visibility = System.Windows.Visibility.Visible;

					// załaduj textblocka
					DifferentPixels.Visibility = System.Windows.Visibility.Visible;
					DifferentPixels.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
					// załaduj PSNR
					PSNR.Visibility = System.Windows.Visibility.Visible;
					PSNR.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
					// załaduj Clear button
					SaveImage.Visibility = System.Windows.Visibility.Visible;
					SaveImage.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
					// załaduj Obraz z różnymi pikselami
					BorderToDifferentImage.Visibility = System.Windows.Visibility.Visible;
					BorderToDifferentImage.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
					// prompt
					prompt.Foreground = Brush.DarkGreen;
					prompt.Text = "Prompt: Comparing SUCCESSFUL!";
					prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
				}
				else
				{
					prompt.Foreground = Brush.DarkRed;
					prompt.Text = "Prompt: Images are different size";
					prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
				}
			}
			else
			{
				if (bmp1 == null)
				{
					prompt.Foreground = Brush.DarkRed;
					prompt.Text = "Prompt: You should load the original Image";
					prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
				}
				else
				{
					prompt.Foreground = Brush.DarkRed;
					prompt.Text = "Prompt: You should load the target Image";
					prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
				}
			}
		}

		private void LoadImage1(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp;*.jpeg;*.png;*.jpg;)|*.bmp;*.jpeg;*.png;*jpg;";
			dlg.Title = "Open Image File";
			Nullable<bool> result = dlg.ShowDialog();

			if (result == true)
			{
				var animationLoad = AnimationHelper.OpacityFrom1to0(2);
				animationLoad.Completed += animationLoad_Completed;
				if (bmp1 != null) // zdarzenie opisujące sytuację w której mieliśmy wczytany obraz i teraz wczytujemy nowy. Wyczyść wszystko i wstaw nowy.
				{
					ClearAll(sender, e);
					prompt.Text = "";
					string path = dlg.FileName;
					bmp1 = new BitmapImage(new Uri(path));
					Image1.Source = bmp1;
				}
				else
				{
					string path = dlg.FileName;
					bmp1 = new BitmapImage(new Uri(path));
					Image1.Source = bmp1;
					prompt.BeginAnimation(OpacityProperty, animationLoad);
				}

			}
		}

		void animationLoad_Completed(object sender, EventArgs e)
		{
			prompt.Text = "";
		}

		private void LoadImage2(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Images (*.bmp;*.jpeg;*.png;*.jpg;)|*.bmp;*.jpeg;*.png;*jpg;";
			dlg.Title = "Open Image File";
			Nullable<bool> result = dlg.ShowDialog();

			if (result == true)
			{
				var animationLoad = AnimationHelper.OpacityFrom1to0(2);
				animationLoad.Completed += animationLoad_Completed;
				if (bmp2 != null)
				{
					ClearAll(sender, e);
					prompt.Text = "";
					string path = dlg.FileName;
					bmp2 = new BitmapImage(new Uri(path));
					Image2.Source = bmp2;
				}
				else
				{
					string path = dlg.FileName;
					bmp2 = new BitmapImage(new Uri(path));
					Image2.Source = bmp2;
					prompt.BeginAnimation(OpacityProperty, animationLoad);
				}
			}
		}

		private void AboutApp(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Praca magisterska. Uniwersytet Gdański 2015");
		}

		private void CloseApp(object sender, RoutedEventArgs e)
		{
			App.Current.Shutdown();
		}

		public BitmapImage ConvertWriteableBitmapToBitmapImage(WriteableBitmap wbm)
		{
			BitmapImage bmp = new BitmapImage();
			using (MemoryStream stream = new MemoryStream())
			{
				PngBitmapEncoder encoder = new PngBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(wbm));
				encoder.Save(stream);
				bmp.BeginInit();
				bmp.CacheOption = BitmapCacheOption.OnLoad;
				bmp.StreamSource = stream;
				bmp.EndInit();
				bmp.Freeze();
			}
			return bmp;
		}

		private void SaveImageBtn(object sender, RoutedEventArgs e)
		{
			prompt.Text = "";
			prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom1to0(1));

			if (DifferentImage.Source != null)
			{
				SaveFileDialog dlg = new SaveFileDialog();
				dlg.Filter = "Png Image|*.png";
				dlg.Title = "Save Image File";
				dlg.DefaultExt = "png";
				Nullable<bool> result = dlg.ShowDialog();

				if (result == true)
				{
					FileStream fs = new FileStream(dlg.FileName, FileMode.OpenOrCreate);
					try
					{
						PngBitmapEncoder encoder = new PngBitmapEncoder();
						encoder.Frames.Add(BitmapFrame.Create(wbm));
						encoder.Save(fs);

						prompt.Foreground = Brush.DarkGreen;
						prompt.Text = "Prompt: The Image has been saved";
						prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
					}
					catch (Exception ex)
					{
						prompt.Foreground = Brush.DarkRed;
						prompt.Text = "Prompt: " + ex.Message;
						prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
					}
					fs.Close();
				}
			}
			else
			{
				prompt.Foreground = Brush.DarkRed;
				prompt.Text = "Prompt: You should load the Image before saving";
				prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));
			}
		}

		private void ClearAll(object sender, RoutedEventArgs e)
		{
			prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom1to0(1));

			if (Compare.IsEnabled == false)
			{
				this.AnimateResize(0, -250.0, 1000.0);
				DifferentPixels.Visibility = System.Windows.Visibility.Collapsed;
				DifferentPixels.Opacity = 0.0;
				DifferentPixels.Text = "";
				PSNR.Visibility = System.Windows.Visibility.Collapsed;
				PSNR.Opacity = 0.0;
				PSNR.Text = "";
				SaveImage.Visibility = System.Windows.Visibility.Collapsed;
				SaveImage.Opacity = 0.0;
				Clear.Visibility = System.Windows.Visibility.Collapsed;
				bmp1 = null;
				bmp2 = null;
				Image1.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/noImage.jpg"));
				Image2.Source = new BitmapImage(new Uri(@"pack://application:,,,/Images/noImage.jpg"));
				Compare.IsEnabled = true;

				var animation = AnimationHelper.OpacityFrom1to0(1);
				animation.Completed += animation_Completed; //jeśli animacja obrazka się skończy -> zwiń obraz i wyzeruj źródło
				BorderToDifferentImage.BeginAnimation(OpacityProperty, animation);
			}
		}

		private void animation_Completed(object sender, EventArgs e)
		{
			BorderToDifferentImage.Visibility = System.Windows.Visibility.Collapsed;
			DifferentImage.Source = null;
			prompt.Foreground = Brush.DarkGreen;
			prompt.Text = "Prompt: Cleaning completed";
			prompt.BeginAnimation(OpacityProperty, AnimationHelper.OpacityFrom0to1(2));

		}
	}
}
