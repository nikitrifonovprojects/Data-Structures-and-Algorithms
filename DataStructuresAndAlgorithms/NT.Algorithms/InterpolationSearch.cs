namespace NT.Algorithms
{
    public class InterpolationSearch
    {
        public int InterpolationArraySearch(int[] array, int value)
        {
            return Interpolation(array, array.Length, value);
        }

        private int Interpolation(int[] array, int length, int value)
        {
            int low = 0;
            int middle = -1;
            int high = length - 1;

            while (low <= high)
            {
                middle = (int)(low + (((double)(high - low) / (array[high] - array[low])) * (value - array[low])));

                if (array[middle] == value)
                {
                    return middle;
                }

                if (array[middle] < value)
                {
                    low = middle + 1;
                }
                else
                {
                    high = middle - 1;
                }
            }

            return -1;
        }
    }
}
