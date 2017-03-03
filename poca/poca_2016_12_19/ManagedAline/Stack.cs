using System;

namespace PPC.LASA.Phonetic
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Stack
	{
		const int MAXL = 200;        // max length of an input word was 24
		const int LINELEN = 800;    // max input line length
		const int MAXS = 2400;      // max input size (in lines)
		const int BASE_UPP = 65;    // = ord('A')
		const int BASE_LOW = 97;    // = ord('a')
		const int OFFSET = 32;      // = ord('a') - ord('A')
		const int NUL = -1;         // empty alignment
		const int LIM = -2;         // border character (for local comparison)
		const int DUB = -3;         // double linking (for compression)
		const int TAB = 4;          // output space for one phoneme
		const int PLEN = 202;        // max word phonemic length (+2) was 24
		const int ELEN = 208;        // max word representation length was 30
		const int NSEG = 26;        // number of recognized segments
		
		private int[,] stack;
		private int top;


		public Stack()
		{
			stack = new int[2, MAXL];
		}


		
//		void clear() { top = 0; }
//		void push( short i1, short i2 = NUL );
//		void pop( short k = 1 ) { top -= k; }


		public void Show()
		{
			for ( short row = 0; row <= 1; row ++ )
			{
				for ( short ind = 0; ind < top; ind++ )
				{
					
				}
			}
		}

		public void Push( int i1, int i2 )
		{   
			//assert( top >= 0 && top < MAXL );
			if ( top >= 0 && top < MAXL )
			{
				stack[0, top] = i1;
				stack[1, top] = i2;
				top++;
			}
		}

		public void Push( int i )
		{   
			//assert( top >= 0 && top < MAXL );
			if ( top >= 0 && top < MAXL )
			{
				stack[0, top] = i;
				stack[1, top] = -1;
				top++;
			}
		}
		
		public int[,] GetStack
		{
			get{ return stack; }
		}

		public int Top
		{
			get{ return top; }
		}

		public void Clear() 
		{ 
			top = 0; 
		}
		
		public void Pop( ) 
		{ 
			top -= 1; 
		}
		
		public void Pop( int k ) 
		{ 
			top -= k; 
		}

		public void ShowAlign( Word w, int row )
		{
			int len = w.PhLen;

			for ( int ind = 0; ind < top; ind++ )
			{
				int i = stack[row, ind];		// pointer to w.ind
//				w.Flush( len - i );
			}
		}

	}
}
