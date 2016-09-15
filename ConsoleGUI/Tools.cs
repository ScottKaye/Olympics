using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGUI
{
	public static class Tools
	{
		public static T Clamp<T>(this T val, T min)
			where T : IComparable<T> =>
				val.CompareTo(min) < 0 ? min
					: val;

		public static T Clamp<T>(this T val, T min, T max)
			where T : IComparable<T> =>
				val.CompareTo(min) < 0 ? min
					: val.CompareTo(max) > 0 ? max
					: val;

		public static T Wrap<T>(this T val, T min, T max)
			where T : IComparable<T> =>
				val.CompareTo(min) < 0 ? max
					: val.CompareTo(max) > 0 ? min
					: val;
	}
}
