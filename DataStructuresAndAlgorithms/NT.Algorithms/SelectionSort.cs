namespace NT.Algorithms
{
    public class SelectionSort
    {
        public void Selection(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minElement = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minElement])
                    {
                        minElement = j;
                    }
                }

                int temp = array[i];
                array[i] = array[minElement];
                array[minElement] = temp;
            }
        }
    }
}
