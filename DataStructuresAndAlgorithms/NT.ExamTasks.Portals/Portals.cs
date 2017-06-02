using System;
using System.Linq;

namespace NT.ExamTasks.Portals
{
    public class Portals
    {
        private static int[,] labyrinth;
        private static int maxPower;

        static void Main()
        {
            int[] startingPosition = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int startRow = startingPosition[0];
            int startCol = startingPosition[1];
            int[] dimensions = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int rows = dimensions[0];
            int cols = dimensions[1];
            labyrinth = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] currentRow = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < currentRow.Length; j++)
                {
                    if (currentRow[j] != "#")
                    {
                        labyrinth[i, j] = int.Parse(currentRow[j]);
                    }
                    else
                    {
                        labyrinth[i, j] = -1;
                    }
                }
            }

            SearchAllPosiblePaths(new Position { Row = startRow, Col = startCol, Power = 0, CurrentState = labyrinth });
            Console.WriteLine(maxPower);
        }

        public static void SearchAllPosiblePaths(Position currentPosition)
        {
            if (currentPosition.Power > maxPower)
            {
                maxPower = currentPosition.Power;
            }

            labyrinth = currentPosition.CurrentState;
            var currentPowerLevel = labyrinth[currentPosition.Row, currentPosition.Col];
            if (currentPowerLevel > 0)
            {
                var nextPowerLevel = currentPosition.Power + currentPowerLevel;
                var up = currentPosition.Row - currentPowerLevel;
                if (up >= 0 && labyrinth[up, currentPosition.Col] != -1)
                {
                    labyrinth[currentPosition.Row, currentPosition.Col] = 0;
                    SearchAllPosiblePaths(new Position { Row = up, Col = currentPosition.Col, Power = nextPowerLevel, CurrentState = GetState(labyrinth) });
                    labyrinth[currentPosition.Row, currentPosition.Col] = currentPowerLevel;
                }

                var down = currentPosition.Row + currentPowerLevel;
                if (down < labyrinth.GetLength((0)) && labyrinth[down, currentPosition.Col] != -1)
                {
                    labyrinth[currentPosition.Row, currentPosition.Col] = 0;
                    SearchAllPosiblePaths(new Position { Row = down, Col = currentPosition.Col, Power = nextPowerLevel, CurrentState = GetState(labyrinth) });
                    labyrinth[currentPosition.Row, currentPosition.Col] = currentPowerLevel;
                }

                var left = currentPosition.Col - currentPowerLevel;
                if (left >= 0 && labyrinth[currentPosition.Row, left] != -1)
                {
                    labyrinth[currentPosition.Row, currentPosition.Col] = 0;
                    SearchAllPosiblePaths(new Position { Row = currentPosition.Row, Col = left, Power = nextPowerLevel, CurrentState = GetState(labyrinth) });
                    labyrinth[currentPosition.Row, currentPosition.Col] = currentPowerLevel;
                }

                var right = currentPosition.Col + currentPowerLevel;
                if (right < labyrinth.GetLength(1) && labyrinth[currentPosition.Row, right] != -1)
                {
                    labyrinth[currentPosition.Row, currentPosition.Col] = 0;
                    SearchAllPosiblePaths(new Position { Row = currentPosition.Row, Col = right, Power = nextPowerLevel, CurrentState = GetState(labyrinth) });
                    labyrinth[currentPosition.Row, currentPosition.Col] = currentPowerLevel;
                }
            }
        }

        public static int[,] GetState(int[,] state)
        {
            var newArray = new int[state.GetLength((0)), state.GetLength(1)];
            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    newArray[i, j] = state[i, j];
                }
            }

            return newArray;
        }

        public class Position
        {
            public int Row { get; set; }

            public int Col { get; set; }

            public int Power { get; set; }

            public int[,] CurrentState { get; set; }
        }
    }
}
