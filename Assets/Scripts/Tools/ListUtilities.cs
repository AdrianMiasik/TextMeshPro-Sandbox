using System;
using System.Collections.Generic;

namespace Text.Tools
{
	public class ListUtilities
	{

		/// <summary>
		/// Fisher yates shuffle for lists
		/// </summary>
		/// <param name="array"></param>
		public static void Shuffle<T>(List<T> list)
		{
			Random rng = new Random();
			int length = list.Count;

			while (length > 1)
			{
				int i = rng.Next(length--);
				T temp = list[length];
				list[length] = list[i];
				list[i] = temp;
			}
		}
	}
}