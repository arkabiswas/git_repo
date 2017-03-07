using System;
using System.IO;
using System.Reflection;
using System.Collections;

namespace AdaptiveAlgorithm
{ 
	/// <summary>
	/// Summary description for AdaptiveAlgorithm.
	/// </summary>
	public class OrthoGraphic
	{
		private System.Collections.Specialized.HybridDictionary h = new System.Collections.Specialized.HybridDictionary();
		private double c_score= 0.75;
		private double p_score= 0.5;

		public OrthoGraphic()
		{
			initBiList();
		}

		public double score(string word1, string word2)
		{
			int len1 = word1.Length;
			int len2 = word2.Length;
			double retval = adaptive(word1, word2);
			double wscore = (retval + retval)/(len1+len2+2);

			return wscore;
		}

		private void initBiList()
		{
			string bi;
			string[] pieces;
			StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("AdaptiveAlgorithm.confuse_data.txt"));
			while(sr.Peek() > -1)
			{
				bi = sr.ReadLine();
				pieces = bi.Split(new Char[] {' '});
				h.Add(pieces[0]+" "+pieces[1], 1);
			}
			sr.Close();
		}

		private double simil(string biE, string biF)
		{
			if (biE.Equals(biF))
				return 1;
			if(h.Contains(biE+" "+biF)) 
			{
				if(h[biE+" "+biF].Equals(1))
					return c_score;
			}

			biE = System.Text.RegularExpressions.Regex.Replace(biE, "#", "=");
			string a1 = biE.Substring(0,1);
			string a2 = biE.Substring(1,1);
			string b1 = biF.Substring(0,1);
			string b2 = biF.Substring(1,1);
			if(a1.Equals(b1) || a2.Equals(b2))
				return p_score;
			else
				return 0;

		} // end simil;

		private double adaptive(string searchword, string compareword)
		{
			int lenE  = searchword.Length;
			int lenF =  compareword.Length;
			double curr = 0;
			double prev = 0;
			double[] s = new double[lenF + 2];
			
			for (int iter = 0; iter <= lenF; iter++)
				s[iter] = 0; 

			for (int i = 1; i <= lenE +1; i++)
			{
				string biE;
				prev = 0;

				if ((i > 1) && (i <= lenE))
				{
					biE = searchword.Substring(i - 2, 2);
				} 
				else if (i > lenE) 
				{
					biE = searchword.Substring(i - 2, 1) + "#";
				}
				else
				{
					biE = "#" + searchword.Substring(i - 1, 1);
				}

				for( int j =1; j <= lenF+1; j++)
				{
					curr = s[j];
					string biF;
					if((j>1)&&(j<=lenF))
					{
						biF = compareword.Substring(j-2,2);
					}
					else if(j>lenF)
					{
						biF = compareword.Substring(j-2,1)+"#";
					}
					else
					{
						biF = "#" + compareword.Substring(j-1,1);
					}
					
					double ident = simil(biE, biF);
					double diag = prev + ident;
					if(diag>curr)
					{
						if(diag>s[j-1])
							s[j] = diag;
						else
							s[j] = s[j-1];
					}
					else
					{
						if(curr>s[j-1])
							s[j]=curr;
						else
							s[j]=s[j-1];
					}
					prev = curr;
				}
			}
			return s[lenF+1];						
		}// end adaptive;
	}
}
