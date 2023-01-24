namespace PathFinder.util
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Combination
    {

        private int n = 0;
        private int k = 0;
        public int[] data = null;

        public Combination(int n, int k)
        {
            if (n < 0 || k < 0)
            {
                throw new Exception("Negative parameter in constructor");
            }

            this.n = n;
            this.k = k;
            this.data = new int[k];

            for (int i = 0; i < k; ++i)
            {
                this.data[i] = i;
            }
        }

        public Combination Successor()
        {
            if (this.data.Length == 0 ||
                this.data[0] == this.n - this.k)
            {
                return null;
            }

            Combination answer = new Combination(this.n, this.k);

            int i;
            for (i = 0; i < this.k; ++i)
            {
                answer.data[i] = this.data[i];
            }

            for (i = this.k - 1; i > 0 && answer.data[i] == this.n - this.k + i; --i) ;

            ++answer.data[i];

            for (int j = i; j < this.k - 1; ++j)
            {
                answer.data[j + 1] = answer.data[j] + 1;
            }

            return answer;
        }

        public string[] ApplyTo(string[] strarr)
        {
            if (strarr.Length != this.n)
            {
                throw new Exception("Bad array size");
            }

            string[] result = new string[this.k];

            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = strarr[this.data[i]];
            }

            return result;
        }

        public static int Choose(int n, int k)
        {
            if (n < 0 || k < 0)
            {
                throw new Exception("Invalid negative parameter in Choose()");
            }

            if (n < k)
            {
                return 0;
            }

            if (n == k)
            {
                return 1;
            }

            int delta, iMax;

            if (k < n - k)
            {
                delta = n - k;
                iMax = k;
            }
            else
            {
                delta = k;
                iMax = n - k;
            }

            int answer = delta + 1;

            for (int i = 2; i <= iMax; ++i)
            {
                checked { answer = (answer * (delta + i)) / i; }
            }

            return answer;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("{ ");

            for (int i = 0; i < this.k; ++i)
            {
                sb.AppendFormat("{0} ", this.data[i]);
            }

            sb.Append("}");

            return sb.ToString();
        }


        public static List<int[]> combination(int[] hh)
        {


            string[] s = null;

            for (int i = 0; i < hh.Length; i++)// list.Count
            {

                if (s == null)
                {
                    s = new string[hh[i]];
                    for (int n = 0; n < hh[i]; n++)
                    {
                        s[n] = "" + n;
                    }
                }
                else
                {

                    string[] t = new string[s.Length * hh[i]];
                    int c = 0;
                    for (int m = 0; m < s.Length; m++)
                    {

                        for (int n = 0; n < hh[i]; n++)
                        {
                            t[c++] = s[m] + "-" + n;
                        }
                    }
                    s = t;
                }
            }

            List<int[]> list = new List<int[]>();
            for (int i = 0; i < s.Length; i++)
            {
                string[] sArray = s[i].Split('-');
                int[] v = new int[sArray.Length]; ;
                for (int j = 0; j < sArray.Length; j++)
                {
                    v[j] = int.Parse(sArray[j]);

                }
                list.Add(v);
            }

            return list;
        }


        public  static void combination2()
        {
            List<List<int>> list = new List<List<int>>();
            List<int> a1 = new List<int>();
            a1.Add(0);
            a1.Add(1);

            List<int> a2 = new List<int>();
            a2.Add(0);
            a2.Add(1);
            a2.Add(2);

            List<int> a3 = new List<int>();
            a3.Add(0);
            List<int> a4 = new List<int>();
            a4.Add(0);
            a4.Add(1);
            a4.Add(2);
            a4.Add(3);
            List<int> a5 = new List<int>();
            a5.Add(0);

            list.Add(a1);
            list.Add(a2);
            list.Add(a3);
            list.Add(a4);
            list.Add(a5);

            List<int> li = new List<int>();
            string[] s = null;

            for (int i = 0; i < list.Count; i++)// list.Count
            {
                List<int> item = (List<int>)list[i];
                if (s == null)
                {
                    s = new string[item.Count];
                    for (int n = 0; n < item.Count; n++)
                    {
                        s[n] = "" + item[n];
                    }
                }
                else
                {

                    string[] t = new string[s.Length * item.Count];
                    int c = 0;
                    for (int m = 0; m < s.Length; m++)
                    {

                        for (int n = 0; n < item.Count; n++)
                        {
                            t[c++] = s[m] + "-" + item[n];
                        }
                    }
                    s = t;
                }
            }

         

        }



    }

}
