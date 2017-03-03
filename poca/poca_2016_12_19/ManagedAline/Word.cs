

using System;
using fval = System.Int32;
using FVec = System.Int32;
using fp = System.Int32;

namespace PPC.LASA.Phonetic
{

	public class FVec { public fval[] Value; }

	/// <summary>
	/// Summary description for Word.
	/// </summary>
	public class Word
	{

		public const int FSyl = 0;
		public const short syl = 100;
		public const short nsl = 0;
		public const int FPlace = 1;
		public const short glo = 10;
		public const short pha = 30;
		public const short uvu = 50;
		public const short vel = 60;
		public const short pal = 70;
		public const short pav = 75;
		public const short rfl = 80;
		public const short alv = 85;
		public const short dnt = 90;
		public const short lbd = 95;
		public const short blb = 100;
		public const int FStop  = 2;
		public const short lvl = 0;
		public const short mvl = 20;
		public const short hvl = 40;
		public const  short  vwl = hvl;
		public const short apr = 60;
		public const short frc = 80;
		public const short afr = 90;
		public const short stp = 100;
		public const int FVoice = 3;
		public const short vce = 100;
		public const short vls = 0;
		public const int FNasal = 4;
		public const short nas = 100;
		public const short nns = 0;
		public const int FRetro = 5;
		public const short ret = 100;
		public const short nrt = 0;
		public const int FLat  = 6;
		public const short lat = 100;
		public const short nlt = 0;
		public const int FAsp  = 7;
		public const short asp = 100;
		public const short nap = 0;
		public const int FLong = 8;
		public const short lng = 100;
		public const short sht = 0;
		public const int FHigh = 9;
		public const short hgh = 100;
		public const short mid = 50;
		public const short low = 0;
		public const int FBack = 10;
		public const short frt = 100;
		public const short cnt = 50;
		public const short bak = 0;
		public const int FRound = 11;
		public const short rnd = 100;
		public const short nrd = 0;
		public const int FDouble = 12;
		const int FT_LEN = 13;

		private string sText;
		private int[] ind;
		public int[][] phon = new int[Global.PLEN][];

		private int iLength;
		private int iOutlength;

		private char[] cExternMixedWord;

		private int[][] FCon = new int[26][];
		private void InitializeFeatureArray()
		{
			FCon[0] = new int[] { syl, vel, vwl, vce, 0, 0, 0, 0, 0, low, cnt };	// a
			FCon[1] = new int[] { nsl, blb, stp, vce };								// b
			FCon[2] = new int[] { nsl, alv, afr, vls };								// c
			FCon[3] = new int[] { nsl, alv, stp, vce };								// d
			FCon[4] = new int[] { syl, pal, vwl, vce, 0, 0, 0, 0, 0, mid, frt };	// e
			FCon[5] = new int[] { nsl, lbd, frc, vls };								// f
			FCon[6] = new int[] { nsl, vel, stp, vce };								// g
			FCon[7] = new int[] { nsl, glo, frc, vls };								// h
			FCon[8] = new int[] { syl, pal, vwl, vce, 0, 0, 0, 0, 0, hgh, frt };	// i
			FCon[9] = new int[] { nsl, alv, afr, vce };								// j
			FCon[10] = new int[] { nsl, vel, stp, vls };							// k
			FCon[11] = new int[] { nsl, alv, apr, vce, 0, 0, lat };					// l
			FCon[12] = new int[] { nsl, blb, stp, vce, nas };						// m
			FCon[13] = new int[] { nsl, alv, stp, vce, nas };						// n
			FCon[14] = new int[] { syl, vel, vwl, vce, 0, 0, 0, 0, 0, mid, bak, rnd };	// o
			FCon[15] = new int[] { nsl, blb, stp, vls };								// p
			FCon[16] = new int[] { nsl, glo, stp, vls };								// q
			FCon[17] = new int[] { nsl, rfl, apr, vce, 0, ret };						// r
			FCon[18] = new int[] { nsl, alv, frc, vls };								// s
			FCon[19] = new int[] { nsl, alv, stp, vls };								// t
			FCon[20] = new int[] { syl, vel, vwl, vce, 0, 0, 0, 0, 0, hgh, bak, rnd };	// u
			FCon[21] = new int[] { nsl, lbd, frc, vce };								// v
			FCon[22] = new int[] { nsl, vel, vwl, vce, 0, 0, 0, 0, 0, hgh, bak, rnd, blb }; // w
			FCon[23] = new int[] { nsl, vel, frc, vls };								// x
			FCon[24] = new int[] { nsl, pal, vwl, vce, 0, 0, 0, 0, 0, hgh, frt };		// y
			FCon[25] = new int[] { nsl, alv, frc, vce };								// z
		}



		public string Text
		{
			get{ return sText; }
			set{ sText = value; }
		}

		public int PhLen
		{
			get
			{
				return iLength;
			}
		}

		public Word( string sWord )
		{
			this.Text = sWord;

			InitializeFeatureArray();
			ApplyRedundancyRules();

			//phon = new int[,];
			iOutlength = sWord.Length;

			cExternMixedWord = new char[Global.ELEN];
			sWord.CopyTo( 0, cExternMixedWord, 0, iOutlength );

			FeatureConvert();
		}

		public void ApplyRedundancyRules()
		{
			for ( int i = 0; i < 26; i++ )
			 {
				 if ( (Words.vowel(FCon[i])) && (FCon[i].Length > FHigh) )
				 {
					 if ( FCon[i][FHigh] == hgh ) FCon[i][FStop] = hvl;
					 else if ( FCon[i][FHigh] == mid ) FCon[i][FStop] = mvl;
					 else if ( FCon[i][FHigh] == low ) FCon[i][FStop] = lvl;
					 //            else assert( false );
				 }

				 if ( (!Words.vowel(FCon[i])) && (FCon[i].Length > FHigh) )		// harmless
				 {
					 FCon[i][FHigh] = hgh;
				 }
			 }
		}


		public void FeatureConvert()
		{
			int j = -1;
			ind = new int[iOutlength + 1];

			for ( int seg = 0; seg < iOutlength; seg++ )
			{
				char c = cExternMixedWord[seg];
				if ( c >= Global.BASE_LOW )
				{
					j++;
					phon[j] = new int[FT_LEN];
					for ( int f = 0; f < FT_LEN; f++ )
					{
						if( FCon[ c - Global.BASE_LOW ].Length > f )
						{
							int val = FCon[ c - Global.BASE_LOW ][f];
							phon[j][f] = val;
						}
						else
							phon[j][f] = 0;
					}
					ind[j] = (short) seg;
				}
				else
				{
					Modify( phon[j], c );
				}
			}
			ind[j+1] = iOutlength;
			iLength = j+1;

		}



		// modifies feature matrix
		private void Modify( int[] p, char c )
		{
			switch ( c )
			{
				case 'A': p[FAsp] = asp;        // "Aspirated"
					break;
				case 'C': p[FBack] = cnt;       // "Central"
					break;
				case 'D': p[FPlace] = dnt;      // "Dental"
					break;
				case 'F': p[FBack] = frt;       // "Front"
					break;
				case 'H': p[FLong] = lng;       // "loHng"
					break;
				case 'L':                       // "Lax" (ignored)
					break;
				case 'N': p[FNasal] = nas;      // "Nasal"
					break;
				case 'P': p[FPlace] = pal;      // "Palatal"
					break;
				case 'S': p[FStop] = frc;       // "Spirant"
					break;
				case 'V': p[FPlace] = pav;      // "palato-alVeolar"
					break;
				default:
					break;
			}
		}

		public void Flush( short wi )
		{
			//assert( wi >= 0 && wi < len );
			int o1 = ind[wi];               // pointers to w.out
			int o2 = ind[wi+1];
			for ( int t = 0; t < Global.TAB; t++ )
			{
				//        if ( o1 != o2 )
				//          printf( "%c", out[o1++] );
				//			System::Console::WriteLine("%c", (System::String*)out[o1++]);

				//        else
				//            printf( " " );
				//			System::Console::WriteLine(" ");
			}
		}

	}
}
