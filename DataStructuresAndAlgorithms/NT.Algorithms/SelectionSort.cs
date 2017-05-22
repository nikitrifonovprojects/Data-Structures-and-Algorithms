namespace NT.Algorithms
{
    public class SelectionSort
    {
        public void Selection(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minElementIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minElementIndex])
                    {
                        minElementIndex = j;
                    }
                }

                if (array[i] != array[minElementIndex])
                {
                    int temp = array[i];
                    array[i] = array[minElementIndex];
                    array[minElementIndex] = temp;
                }
            }
        }
    }
}
