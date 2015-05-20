using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace Steganography_Analyzer
{
	class AnimationHelper
	{
		public static AnimationTimeline OpacityFrom0to1(int seconds)
		{
			DoubleAnimation animation = new DoubleAnimation();
			animation.From = 0.0;
			animation.To = 1.0;
			animation.Duration = new Duration(TimeSpan.FromSeconds(seconds));

			return animation;
		}

		public static AnimationTimeline OpacityFrom1to0(int seconds)
		{
			DoubleAnimation animation = new DoubleAnimation();
			animation.From = 1.0;
			animation.To = 0.0;
			animation.Duration = new Duration(TimeSpan.FromSeconds(seconds));

			return animation;
		}
	}
}
