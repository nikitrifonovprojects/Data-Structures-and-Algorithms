namespace NT.Algorithms
{
    public class QuickSort
    {
        public void QuickSortArray(int[] array)
        {
            Sort(array, 0 , array.Length - 1);
        }

        private void Sort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int q = Partition(array, left, right);
                Sort(array, left, q - 1);
                Sort(array, q + 1, right);
            }
        }

        private int Partition(int[] data, int left, int right)
        {
            int pivot = data[right];
            int temp;
            int i = left;

            for (int j = left; j < right; ++j)
            {
                if (data[j] <= pivot)
                {
                    if (i != j)
                    {
                        temp = data[j];
                        data[j] = data[i];
                        data[i] = temp;
                    }
                    
                    i++;
                }
            }

            data[right] = data[i];
            data[i] = pivot;

            return i;
        }
    }
}
