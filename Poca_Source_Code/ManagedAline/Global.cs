using System;


namespace PPC.LASA.Phonetic
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public sealed class Global
	{
		
		public const int MAXL = 200;        // max length of an input word was 24
		public const int LINELEN = 800;    // max input line length
		public const int MAXS = 2400;      // max input size (in lines)
		public const int BASE_UPP = 65;    // = ord('A')
		public const int BASE_LOW = 97;    // = ord('a')
		public const int OFFSET = 32;      // = ord('a') - ord('A')
		public const int NUL = -1;         // empty alignment
		public const int LIM = -2;         // border character (for local comparison)
		public const int DUB = -3;         // double linking (for compression)
		public const int TAB = 4;          // output space for one phoneme
		public const int PLEN = 202;        // max word phonemic length (+2) was 24
		public const int ELEN = 208;        // max word representation length was 30
		public const int NSEG = 26;        // number of recognized segments

		private static int[] salCoef = new int[] { 5, 40, 50, 10, 10, 10, 10, 5, 1, 5, 5, 5, 10 };
		public Global() { }
 

		private const int FACTOR = 100;             // for emulation of floats by ints
		public static int Conv( float f ) { return (int) f * FACTOR; }
		public static float DeConv( int k ) { return (float) k / FACTOR; }

		private static int Conv( int f )
		{
			return ( f * 100 );
		}

		public static int MaxScore
		{
			get{ return Conv(35); }
		}
		public static int SkipCost
		{
			get{ return Conv(10); }
		}

		public static int[] SalCoef
		{
			get{ return salCoef; }
		}

		public static int VwlDecr
		{
			get{ return Conv(10); }
		}

		public static float ScoreMargin
		{
			get{ return (float) 1; }
		}
	}
}
