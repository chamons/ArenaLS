using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using ArenaLS.Utilities;

namespace ArenaLS.UI.Utilities
{
	public static class SizeExtensions
	{
		public static SKSize AsSKSize (this Size s)
		{
			return new SKSize (s.Width, s.Height);
		}
	}
}
