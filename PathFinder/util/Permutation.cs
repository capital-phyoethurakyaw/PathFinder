namespace PathFinder.util
{
    class Permutation
    {
        public int[] data = null;
        private int order = 0;

        public Permutation(int n)
        {
            this.data = new int[n];
            for (int i = 0; i < n; ++i)
            {
                this.data[i] = i;
            }

            this.order = n;
        }

        public Permutation Successor()
        {
            Permutation result = new Permutation(this.order);

            int left, right;
            for (int k = 0; k < result.order; ++k)  // Step #0 - copy current data into result
            {
                result.data[k] = this.data[k];
            }

            left = result.order - 2;  // Step #1 - Find left value 
            while ((result.data[left] > result.data[left + 1]) && (left >= 1))
            {
                --left;
            }

            if ((left == 0) && (this.data[left] > this.data[left + 1]))
            {
                return null;
            }

            right = result.order - 1;  // Step #2 - find right; first value > left
            while (result.data[left] > result.data[right])
            {
                --right;
            }

            int temp = result.data[left];  // Step #3 - swap [left] and [right]
            result.data[left] = result.data[right];
            result.data[right] = temp;


            int i = left + 1;              // Step #4 - order the tail
            int j = result.order - 1;

            while (i < j)
            {
                temp = result.data[i];
                result.data[i++] = result.data[j];
                result.data[j--] = temp;
            }

            return result;
        }

        internal static long Choose(int length)
        {
            long answer = 1;

            for (int i = 1; i <= length; i++)
            {
                checked { answer = answer * i; }
            }

            return answer;
        }

        public string[] ApplyTo(string[] arr)
        {
            if (arr.Length != this.order)
            {
                return null;
            }

            string[] result = new string[arr.Length];
            for (int i = 0; i < result.Length; ++i)
            {
                result[i] = arr[this.data[i]];
            }

            return result;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append("( ");
            for (int i = 0; i < this.order; ++i)
            {
                sb.Append(this.data[i].ToString() + " ");
            }
            sb.Append(")");

            return sb.ToString();
        }

    }
}
