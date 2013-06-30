/*
 * Created by SharpDevelop.
 * User: Leon
 * Date: 29/06/2013
 * Time: 18:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace vzWordyHoster
{
	/// <summary>
	/// Utils.
	/// </summary>
	public static class MiscUtils
	{
		public static void FisherYatesShuffle<T>(this IList<T> items) {
			// Shuffles a list in a very random way.
			// Use it as an extension method. Example usage: alphasAsList.FisherYatesShuffle();
		    var randomGenerator = new RNGCryptoServiceProvider();
		    var itemIndex = items.Count;
			
		    while (itemIndex > 1) {
			//Generate a new random number using RNGCryptoServiceProvider as Random() produces less-than-random results
		        var box = new byte[1];
		        do {
					randomGenerator.GetBytes(box);
				} while (!(box[0] < itemIndex * (Byte.MaxValue / itemIndex)));
				
		        var randomIndex = (box[0] % itemIndex);
		        itemIndex--;
				
				//Swap the indexed and random positions
		        T value = items[randomIndex];
		        items[randomIndex] = items[itemIndex];
		        items[itemIndex] = value;
		    }
		}//FisherYatesShuffle
	}//class MiscUtils
}
