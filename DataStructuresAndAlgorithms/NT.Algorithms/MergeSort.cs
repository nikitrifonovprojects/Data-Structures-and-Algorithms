using System;

namespace NT.Algorithms
{
    public class MergeSort<T> where T : IComparable<T>
    {
        public void MergeSortArray(T[] array)
        {
            Sort(array, 0, array.Length);
        }

        private void Sort(T[] array, int low, int high)
        {
            int n = high - low;
            if (n <= 1)
            {
                return;
            }

            int mid = low + n / 2;

            Sort(array, low, mid);
            Sort(array, mid, high);

            T[] aux = new T[n];
            int i = low, j = mid;
            for (int k = 0; k < n; k++)
            {
                if (i == mid)
                {
                    aux[k] = array[j++];
                }
                else if (j == high)
                {
                    aux[k] = array[i++];
                }
                else if (array[j].CompareTo(array[i]) < 0)
                {
                    aux[k] = array[j++];
                }
                else
                {
                    aux[k] = array[i++];
                }
            }

            for (int f = 0; f < n; f++)
            {
                array[low + f] = aux[f];
            }
        }
    }
}
