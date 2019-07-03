using MatrixTestTask.Utils;

namespace MatrixTestTask.Models
{
    public static class MatrixModel
    {
        public static int[,] GetInitialMatrix()
        {
            var result = new int[Settings.MatrixSize, Settings.MatrixSize];

            for (int i = 0; i < Settings.MatrixSize; i++)
            {
                for (int j = 0; j < Settings.MatrixSize; j++)
                {
                    result[i, j] = j + i * Settings.MatrixSize;
                }
            }

            return result;
        }

        public static int[,] RotateMatrix(int[,] source)
        {
            var result = new int[Settings.MatrixSize, Settings.MatrixSize];

            for (int i = 0; i < Settings.MatrixSize; i++)
            {
                for (int j = 0; j < Settings.MatrixSize; j++)
                {
                    result[i, j] = source[Settings.MatrixSize - j - 1, i];
                }
            }

            return result;
        }
    }
}
