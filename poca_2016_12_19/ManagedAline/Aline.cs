using System;
using System.Data;

namespace PPC.LASA.Phonetic
{
	/// <summary>
	/// This class provides an interface to the aline algorithm. Allows for single word comparison or multiple word 
	/// comparison. This class also creates only one instance of the search word during the lifetime of the object
	/// unless the search word changed.
	/// </summary>
	public class Aline
	{
		private Word wSearchWord;
		private Word wCompareWord;
		private Wagner wagner = new Wagner();
		private decimal dMaxValue;
		private decimal dMaxFactor;


		/// <summary>
		/// Default constructor. Does nothing.
		/// </summary>
		public Aline(){}

		/// <summary>
		/// Contructor that sets and initializes the searchWord object. Calls the appropriate functions and properties
		/// to find MaxValue and MaxFactor for the word.
		/// </summary>
		/// <param name="searchWord">Word to phetically compare all values to.</param>
		public Aline( string searchWord )
		{
			SearchWord = searchWord;
		}

		/// <summary>
		/// Public property the sets and returns the search word used for this instance.
		/// </summary>
		public string SearchWord
		{
			get
			{ 
				return wSearchWord.Text; 
			}
			set
			{ 
				wSearchWord = new Word( value );
				SetMaxValue();	// Call function to get result of search word compared to itself and set MaxValue and MaxFactor.
			}
		}


		/// <summary>
		/// Function to ensure that set search word object is set prior to calling phonetic algorithm.
		/// </summary>
		/// <remarks>
		/// If default contructor is used, search word object is not necessarily set.
		/// </remarks>
		private void VerifySearchWord()
		{
			if( wSearchWord == null )
				throw new ApplicationException( "A search word must be set before calling this function." );
		}



		/// <summary>
		/// This function sets the MaxValue from the search word compared to itself. This result is then
		/// used to determine match percentage for all subsequent phonetic comparisons.
		/// </summary>
		private void SetMaxValue( )
		{
			MaxValue = GetPhoneticMatch( wSearchWord.Text );
		}

		/// <summary>
		/// Public property to set and return MaxFactor value found from comparing the search word to itself.
		/// </summary>
		public decimal MaxFactor
		{
			get{ return dMaxFactor; }
			set{ dMaxFactor = value; }
		}

		/// <summary>
		/// Public property to set and return MaxValue found from comparing the search word to itself.
		/// </summary>
		public decimal MaxValue
		{
			get{ return dMaxValue; }
			set
			{ 
				dMaxValue = value; 
				MaxFactor = (dMaxValue / (wSearchWord.Text.Length * dMaxValue));
			}
		}

		/// <summary>
		/// This function uses the MaxValue and MaxFactor values to determine the percentage match 
		/// based on the result passed to the function.
		/// </summary>
		/// <param name="iResult">Raw result from phonetic algorithm.</param>
		/// <returns>Phonetic match percentage.</returns>
		public double GetPercentage( int iResult )
		{
			decimal Denomenator = Math.Max(wSearchWord.Text.Length, wCompareWord.Text.Length ) * MaxValue;
			decimal CurrentRowValue = (iResult / Denomenator) / MaxFactor;
			double FinalValue = System.Math.Round((double)(CurrentRowValue * 100));
			
			return FinalValue;
		}



		/// <summary>
		/// This function takes in the dataset of words and returns a dataset with match percentages
		/// based on the search word. Currently not implemented.
		/// </summary>
		/// <param name="dsCompareWords">Dataset with normalized word column.</param>
		/// <returns>Dataset with results that have over a 65 percent match.</returns>
		public DataSet GetPhoneticMatches( DataSet dsCompareWords )
		{
			VerifySearchWord();			// Determine whether search word has been set.

			DataSet dsResults = new DataSet("Results");		// Create new dataset with table named results.

			return dsResults;
		}


		/// <summary>
		/// This function calls the phonetic aline algorithm and returns the result of the SearchWord compared
		/// to the CompareWord. Made private since Word values must set prior to calling this function and it 
		/// omits checking whether this is true prior to calling algorithm.
		/// </summary>
		/// <returns>Raw integer result from phonetic match.</returns>
		private int GetPhoneticMatch()
		{
			wagner.Align( wSearchWord, wCompareWord );
			return wagner.GetFinalScore;
		}


		/// <summary>
		/// Set the CompareWord and then call GetPhoneticMatch to compute match value.
		/// </summary>
		/// <param name="compareWord"></param>
		/// <returns></returns>
		public int GetPhoneticMatch( string compareWord )
		{
//			VerifySearchWord();
			wCompareWord = new Word( compareWord );

			return GetPhoneticMatch();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchWord"></param>
		/// <param name="compareWord"></param>
		/// <returns></returns>
		public int GetPhoneticMatch( string searchWord, string compareWord )
		{
			SearchWord = searchWord;
			wCompareWord = new Word( compareWord );

			return GetPhoneticMatch();
		}
	}
}
