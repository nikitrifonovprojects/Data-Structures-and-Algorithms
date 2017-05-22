namespace NT.Algorithms
{
    public class BinarySearch
    {
        public int BinarySearchArray(int[] array, int value)
        {
            int min = 0;
            int max = array.Length - 1;
            int middle = (min + max) / 2;

            while (array[middle] != value)
            {
                if (min > max)
                {
                    return - 1;
                }

                if (array[middle] < value)
                {
                    min = middle++;
                }
                else if (array[middle] > value)
                {
                    max = middle--;
                }
            }

            return middle;
        }

        public int BinarySearchRecursive(int[] array, int value)
        {
            return Recursive(array, value, 0, array.Length - 1);
        }

        private int Recursive(int[] array, int value, int min , int max)
        {
            if (min > max)
            {
                return - 1;
            }
            else
            {
                int middle = (min + max) / 2;
                if (value == array[middle])
                {
                    return middle;
                }
                else if (array[middle] > value)
                {
                    return Recursive(array, value, min, middle - 1);
                }
                else
                {
                    return Recursive(array, value, middle + 1, max);
                }
            }
        }
    }
}
