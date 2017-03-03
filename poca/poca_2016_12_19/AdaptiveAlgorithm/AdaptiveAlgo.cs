using System;
using System.IO;
using System.Reflection;
using System.Collections;
using AliasToOrthographic = AdaptiveAlgorithm.OrthoGraphic;

namespace AdaptiveAlgorithm
{
    /// <summary>
    /// Summary description for AdaptiveAlgorithm.
    /// </summary>
    public class OrthoGraphic
    {
        private System.Collections.Specialized.HybridDictionary h = new System.Collections.Specialized.HybridDictionary();
        private double c_score = 0.75;
        //3.2 private double p_score = 0.5;
        private double p_score = 0.0; //4.0
        public bool debug = true;
        public OrthoGraphic()
        {
            initBiList();
        }

        public double score(string word1, string word2)
        {
            int len1 = word1.Length;
            int len2 = word2.Length;
            int minL = Math.Min(len1, len2); //4.0
            double avgL = (len1 + len2) / 2.0;  //4.0 ver 4     
            double Bscore = Math.Min(initMatch(word1, word2), 3);  //4.0

            double Escore = Math.Min(initMatch(reverse(word1), reverse(word2)), 1);   //4.0

            double Ascore = adaptive(word1, word2);

            //3.2 double wscore = (retval + retval) / (len1 + len2 + 2);
            //3.2  return wscore;
            //double wAscore = (Bscore + Escore + Ascore) / (minL + 3);  //4.0
            double wAscore = (Bscore + Escore + Ascore) / (avgL + 3);  //4.0 ver 4


            double Uscore = (matchUni(word1, word2) + matchUni(word2, word1)) / 2;  //4.0

            double Lscore = lcsq(word1, word2);  //4.0

            double wLUscore = (Lscore + Uscore) / (len1 + len2); //4.0


            double Sscore = lcsb(word1, word2);  //4.0

           // double wSscore = Sscore / (minL + 1);  //4.0 ver 1
            double wSscore = (Sscore + Sscore) / (len1 + len2 + 2); //4.0 ver 2


            double wFscore = Math.Max(wAscore, Math.Max(wLUscore, wSscore));  //4.0

            //4.0 ver 3
            if ((wFscore == 1) && (word1 != word2))
            {
                wAscore = (Bscore + Escore + Ascore) / (minL + 4);
                wFscore = Math.Max(wAscore, Math.Max(wLUscore, wSscore));
            }

            return wFscore;
        }

        private void initBiList()
        {
            string bi;
            
            StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("AdaptiveAlgorithm.confuse_data.txt"));
            while (sr.Peek() > -1)
            {
                bi = sr.ReadLine();



                if (h.Contains(bi.Substring(0, 2) + " " + bi.Substring(3, 2))) { }
                else {
                    h.Add(bi.Substring(0, 2) + " " + bi.Substring(3, 2), 1);

                }

                if (h.Contains(bi.Substring(3, 2) + " " + bi.Substring(0, 2))) { }
                else {
                   h.Add(bi.Substring(3,2) + " " + bi.Substring(0, 2),1);
                }
            }
            sr.Close();
        }
        //4.0 initMatch
        private double initMatch(string word1, string word2)
        {
            int lenE = word1.Length;
            int lenF = word2.Length;
            int i = 0;

            while (++i <= Math.Min(lenE, lenF))
            {
                if (word1.Substring(0, i) != word2.Substring(0, i)) { break; }
            }

            return i - 1;
        }

        //4.0	longest common subsequence
        public double lcsq(string word1, string word2)
        {
            int lenE = word1.Length;
            int lenF = word2.Length;
            int MaxLen = Math.Max(lenE, lenF);
            int[] S = new int[MaxLen];

            for (int l = 0; l < lenE; l++)
            {
                S[l] = lenF;
            }

            for (int i = 0; i < lenE; i++)
            {
                for (int j = lenF - 1; j >= 0; j--)
                {
 
                    if (word1.Substring(i, 1) == word2.Substring(j, 1))
                    {
                        int l = 0;
                        while (j > S[l])
                        {
 
                            l++;
                        }
 
                        S[l] = j;
                    }
                }
            }
            for (int x = 0; x < 9; x++ )
            {
 
            }
 
            for (int l = 0; l < lenE; l++)
            {
                if (S[l] == lenF) {
 
                    return l; }
            }

            return lenE;
        }


        //4.0 unigram match 
        public double matchUni(string word1, string word2)
        {
            int lenE = word1.Length;
            int lenF = word2.Length;
            Hashtable memE = new Hashtable();
            int matchF = 0;

            for (int i = 0; i < lenE; i++)
            {
                string uni = word1.Substring(i, 1);
 
                if (memE.Contains(uni))
                {
                    int old = (int)memE[uni];
                    memE[uni] = old + 1;
                }
                else { memE.Add(uni, 1); }
            }

            for (int j = 0; j < lenF; j++)
            {
                string uni = word2.Substring(j, 1);
 
                if    (memE.ContainsKey(uni))
                {
                    matchF++;
                    // memE{uni}--;
                }
            }
 
            return matchF;
        }



        //4.0 longest common substring
        public double lcsb(string word1, string word2)
        {
            int lenE = word1.Length;
            int lenF = word2.Length;
            int[] S1 = new int[lenF + 2];
            int z = 0;
            int prev = 1; // not zero!

            for (int j = 0; j < lenF; j++)
            {
                S1[j] = 0;
            }

            for (int i = 0; i < lenE; i++)
            {
                for (int j = 0; j < lenF; j++)
                {
                    int curr = S1[j];

                    if (word1.Substring(i, 1) == word2.Substring(j, 1))
                    {
                        S1[j] = prev + 1;
                        if (S1[j] > z)
                        { z = S1[j]; }
                    }
                    else {
                        S1[j] = 0;
                    }

                    prev = curr;
                }

                prev = 0;
            }

            return z;
        }

        ///4.0 reverse
        public static string reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        //4.0 end reverse
        // begin simil
        private double simil(string biE, string biF)
        {
            if (biE.Equals(biF))
                return 1;
            if (h.Contains(biE + " " + biF))
            {
                //if (h[biE + " " + biF].Equals(1))
                    return c_score;
            }

            biE = System.Text.RegularExpressions.Regex.Replace(biE, "#", "=");
            string a1 = biE.Substring(0, 1);
            string a2 = biE.Substring(1, 1);
            string b1 = biF.Substring(0, 1);
            string b2 = biF.Substring(1, 1);
            if (a1.Equals(b1) || a2.Equals(b2))
                return p_score;
            else
                return 0;

        } // end simil;

 
        // new adaptive similarity measure
        public double adaptive(string searchword, string compareword)
        {
            int lenE = searchword.Length;
            int lenF = compareword.Length;
            double[] S = new double[lenF];

            for (int j = 0; j < lenF; j++)
            {
                S[j] = 0;
            }

            for (int i = 1; i < lenE; i++)
            {
                double prev = 0;
                //   double curr = 0;
                string biE = searchword.Substring(i - 1, 2);
 
                for (int j = 1; j < lenF; j++)
                {
                    double curr = S[j];
                    string biF = compareword.Substring(j - 1, 2);
 
                    double ident = simil(biE, biF);
 
                    double diag = prev + ident;
                    if (diag > curr)
                    {
                        if (diag > S[j - 1])
                        {
                            S[j] = diag;
                        }
                        else {
                            S[j] = S[j - 1];
                        }


                    }
                    else
                    {
                        if (curr > S[j - 1])
                        {
                            S[j] = curr;
                        }
                        else
                        {
                            S[j] = S[j - 1];
                        }
                    }
                    prev = curr;
                }
            } // 
                return S[lenF - 1];
            
        }
    }
}
 