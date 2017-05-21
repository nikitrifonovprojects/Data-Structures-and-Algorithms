using System;
using System.Collections.Generic;

namespace NT.Algorithms
{
    public class InsertionSort<T> where T : IComparable<T>
    {
        public void Insertion(T[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                T value = array[i];
                int j = i - 1;
                while (j >= 0 && array[j].CompareTo(value) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = value;
            }

        }

        public void InsertionRecursive(T[] array, int n)
        {
            if (n > 1)
            {
                InsertionRecursive(array, n - 1);
                T value = array[n];
                int j = n - 1;
                while (j >= 0 && array[j].CompareTo(value) > 0)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = value;
            }
        }

        public void InsertionSortList(IList<T> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                T value = list[i];
                int j = i - 1;
                while ((j >= 0) && (list[j].CompareTo(value) > 0))
                {
                    list[j + 1] = list[j];
                    j--;
                }

                list[j + 1] = value;
            }
        }
    }
}
