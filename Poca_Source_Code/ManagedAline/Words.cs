using System;
 
namespace PPC.LASA.Phonetic
{
	/// <summary>
	/// Summary description for Words.
	/// </summary>
	public sealed class Words
	{		
		const int FSyl = 0;
		const int FPlace = 1;
		const int FStop  = 2;
		const int FVoice = 3;
		const int FNasal = 4;
		const int FRetro = 5;
		const int FLat  = 6;
		const int FAsp  = 7;
		const int FLong = 8;
		const int FHigh = 9;
		const int FBack = 10;
		const int FRound = 11;
		const int FDouble = 12;


		public Words() { }

		


		// compute score for substitution
		public static int SigmaSub( Word wA, int iA, Word wB, int iB )
		{
			int[] pA = wA.phon[wA.PhLen-iA];
			int[] pB = wB.phon[wB.PhLen-iB];

			int score = Global.MaxScore - Words.LadDist( pA, pB );

			if ( vowel( pA ) )
				score -= Global.VwlDecr;
			if ( vowel( pB ) )
				score -= Global.VwlDecr;

			return score;
		}

		public static int SigmaSkip( Word w, int i )
		{
			// Old c++ code. Always returned -1000.
			//			FP p = w.phon[w.PhLen-i];
			//			return -gP::SkipCost;

			return -Global.SkipCost;
		}

		// true if segment is a vowel
		public static bool vowel( int[] p )
		{
			return ( p[FStop] <= Word.vwl );
		}



		// compute difference wrt specific feature
		private static int Consider( int[] p1, int[] p2, int iFeatureType )
		{
			int diff = Math.Abs( p1[iFeatureType] - p2[iFeatureType] );
            
			return diff * Global.SalCoef[iFeatureType];
		}


		// deal with double articulations
		private static int places( int[] p1, int[] p2 )
		{
			int pd = Math.Abs( p1[FPlace] - p2[FPlace] );

			if ( p1[FDouble] != 0 )
				pd = Math.Min( pd,  Math.Abs( p1[FDouble] - p2[FPlace] ) );

			if ( p2[FDouble] != 0 )
				pd = Math.Min( pd,  Math.Abs( p1[FPlace] - p2[FDouble] ) );

			if ( p1[FDouble]!= 0 && p2[FDouble] != 0 )
				pd = Math.Min( pd,  Math.Abs( p1[FDouble] - p2[FDouble] ) );

			return pd * Global.SalCoef[FPlace];
		}


		// compute distance between two feature vectors
		public static int LadDist( int[] p1, int[] p2 )
		{
			int dist = 0;

			if ( vowel( p1) && vowel(p2) )
			{
				dist += Consider( p1, p2, FSyl );
				dist += Consider( p1, p2, FNasal );
				dist += Consider( p1, p2, FRetro );
				dist += Consider( p1, p2, FHigh );
				dist += Consider( p1, p2, FBack );
				dist += Consider( p1, p2, FRound );
				dist += Consider( p1, p2, FLong );
			}
			else
			{
				dist += Consider( p1, p2, FSyl );
				dist += Consider( p1, p2, FStop );
				dist += Consider( p1, p2, FVoice );
				dist += Consider( p1, p2, FNasal );
				dist += Consider( p1, p2, FRetro );
				dist += Consider( p1, p2, FLat );
				dist += Consider( p1, p2, FAsp );
				dist += places( p1, p2 );		// was: Consider(FPlace);
			}

			return dist;
		}



	}
}
