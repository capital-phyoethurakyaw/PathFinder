namespace PathFinder.analysis
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Dijkstra
    {

        public static double[] analysis(int start, int end, double[,] data)
        {
            int n = data.GetLength(0);
            int i, j, k = 0;
            double min;
            int[] v = new int[n];
            double[] distance = new double[n];
            int[] via = new int[n];
            for (j = 0; j < n; j++)
            {
                v[j] = 0;
                distance[j] = double.MaxValue;
            }

            distance[start] = 0;
            for (i = 0; i < n; i++)
            {
                min = double.MaxValue;
                for (j = 0; j < n; j++)
                {
                    if (v[j] == 0 && distance[j] < min)
                    {
                        k = j;
                        min = distance[j];
                    }
                }

                v[k] = 1;
                if (min == double.MaxValue)
                {

                    break;
                }
                for (j = 0; j < n; j++)
                {
                    if (distance[j] > distance[k] + data[k, j])
                    {
                        distance[j] = distance[k] + data[k, j];
                        via[j] = k;
                    }
                }
            }
            int path_cnt = 0;
            int[] path = new int[1000000];
            k = end;
            try
            {
                while (true)
                {
                    path[path_cnt++] = k;
                    if (k == start) break;
                    k = via[k];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("analysis" + ex.ToString());
                double[] result2 = null;
                return result2;
            }
            double[] result = new double[path_cnt + 1];
            int count = 0;
            for (i = path_cnt - 1; i > 0; i--, count++)
            {
                result[count] = path[i];
            }
            result[count] = path[i];
            result[result.Length - 1] = distance[end];
            return result;
        }
        

    }
}
