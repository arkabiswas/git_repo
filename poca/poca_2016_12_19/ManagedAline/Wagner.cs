using System;

namespace PPC.LASA.Phonetic
{
	/// <summary>
	/// Summary description for Wagner.
	/// </summary>
	public class Wagner
	{
		const int NoScore = -99999;
		const int MAXL = 200;			// max length of an input word was 24

		bool  FallThru;					// for ONE_ONLY mode
		int	  finalScore;
		int   DpScore;                  // score computed by the DP routine
		float AcptScore;				// minimal acceptable score

		Stack Trace = new Stack();		// trace i.e. unambiguous alignment
		Stack Out = new Stack();		// alignment found by DP routine
		Stack Cost = new Stack();		// cost of individual operations


		private int[,] S = new int[MAXL,MAXL];            // score matrix


		public Wagner()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		
		// wrapper function for the score matrix
		private int Score( int i, int j )
		{
			return (int) ( ( i >= 0 ) && ( j >= 0 ) ? S[i,j] : NoScore );
		}


		private int Maximum( int m1, int m2, int m3, int m4, int m5 )
		{
			return Math.Max( m1, Math.Max( m2, Math.Max( m3, Math.Max( m4, m5 ) ) ) );
		}


		private int Similarity( Word wA, Word wB )
		{
			int lenA = wA.PhLen;
			int lenB = wB.PhLen;

			int sgmax = 0;			// not meaningful for global case
			int m1, m2, m3, m4 = NoScore, m5 = NoScore, lmax;
			int i;

			S[0,0] = 0;

			for ( i = 1; i <= lenA; i++ )
				S[i,0] = S[i-1,0] + Words.SigmaSkip( wA, i );


			for ( int j = 1; j <= lenB; j++ )
				S[0,j] = S[0,j-1] + Words.SigmaSkip( wB, j );


			for ( i = 1; i <= lenA; i++ )
			{
				for ( int j = 1; j <= lenB; j++ )
				{
					m1 = Score(i-1,j) + Words.SigmaSkip( wA, i );
					m2 = Score(i,j-1) + Words.SigmaSkip( wB, j );
					m3 = Score(i-1,j-1) + Words.SigmaSub( wA, i, wB, j );

					lmax = Maximum( m1, m2, m3, m4, m5 );

					S[i,j] = lmax;

					if ( lmax > sgmax )
							sgmax = lmax;
				}
			}

			return sgmax;
		}



		
		// filtering alignments that violate alternating-skips rule of Covington;
		// note that this is a rather expensive solution
		private bool Allowed( Stack w )
		{
			for ( int i = 1; i < w.Top; i++ )
			{
				if ( w.GetStack[0,i] == Global.NUL && w.GetStack[1,i-1] == Global.NUL )
					return false;
			}
			return true;
		}

		private void Show( int score )
		{
			finalScore = (int) Global.DeConv(score);
		}

		

		// get alignment score
		public int GetFinalScore
		{
			get{ return finalScore;	}
		}


		// algorithm Y
		// made recursive in order to find all maximal solutions (not just one)
		private void Alignment( Word wA, Word wB, int i, int j, int T )
		{

			if ( i == 0 && j == 0 )
			{
				//assert( Score(i,j) == 0 );
				if ( Allowed( Out ) && !FallThru )
				{
					Out.Push( Global.LIM, Global.LIM );
					// padding the remaining string
					for ( int i1 = i; i1 > 0; i1-- )
						Out.Push( i1, Global.NUL );
					for ( int j1 = j; j1 > 0; j1-- )
						Out.Push( Global.NUL, j1 );

					Out.Pop(i+j+1);

					int score = T + (i+j) * Words.SigmaSkip( wA, 0 );

					Show( score );

				}
			}
			else
			{
				if( i != 0  && j != 0)
				{

					int subSc = Words.SigmaSub( wA, i, wB, j );
					if ( Score(i-1,j-1) + subSc + T >= AcptScore )
					{
						Cost.Push( subSc );
						Out.Push( i, j );
						Trace.Push( i, j );
						Alignment( wA, wB, i-1, j-1, T + subSc );
						Trace.Pop();
						Out.Pop();
						Cost.Pop();
					}
				}


				int insSc = Words.SigmaSkip( wB, j );
				if ( ( i == 0 ) || ( Score(i,j-1) + insSc + T >= AcptScore ) )
				{
					Cost.Push( insSc );
					Out.Push( Global.NUL, j );
					Alignment( wA, wB, i, j-1, T + insSc );
					Out.Pop();
					Cost.Pop();
				}


				int delSc = Words.SigmaSkip( wA, j );
				if ( ( j == 0 ) || ( Score(i-1,j) + delSc + T >= AcptScore ) )
				{
					Cost.Push( delSc );
					Out.Push( i, Global.NUL );
					Alignment( wA, wB, i-1, j, T + delSc );
					Out.Pop();
					Cost.Pop();
				}

			}
		}

		// find all optimal alignments
		public void Align( Word wA, Word wB )
		{
			int lenA = wA.PhLen;
			int lenB = wB.PhLen;



			Cost.Clear();
			Trace.Clear();
			Out.Clear();
			FallThru = false;

			int sgmax = Similarity( wA, wB );


			DpScore = Score( lenA, lenB );	// sgmax is ignored in global case
			
			AcptScore = DpScore * Global.ScoreMargin;


			int i = lenA;
			int j = lenB;
//			if ( i < lenA || j < lenB )		// corner start point only
//				continue;
			if ( S[i,j] >= AcptScore ) 
			{
				for ( int j1 = lenB; j1 > j; j1-- )
					Out.Push( Global.NUL, j1 );
				for ( int i1 = lenA; i1 > i; i1-- )
					Out.Push( i1, Global.NUL );

				Out.Push( Global.LIM, Global.LIM );
				Alignment( wA, wB, i, j, 0 );
				Out.Pop(lenA-i+lenB-j+1);
				if ( FallThru ) return;
			}
//
//			for ( int i = 0; i <= lenA; i++ )
//			{
//				for ( int j = 0; j <= lenB; j++ )
//				{
//					if ( i < lenA || j < lenB )		// corner start point only
//						continue;
//					if ( S[i,j] >= AcptScore ) 
//					{
//						for ( int j1 = lenB; j1 > j; j1-- )
//							Out.Push( Global.NUL, j1 );
//						for ( int i1 = lenA; i1 > i; i1-- )
//							Out.Push( i1, Global.NUL );
//
//						Out.Push( Global.LIM, Global.LIM );
//						Alignment( wA, wB, i, j, 0 );
//						Out.Pop(lenA-i+lenB-j+1);
//						if ( FallThru ) return;
//					}
//				}
//			}
		}
		

	}

}
