using Admixer.App;
using NUnit.Framework;

namespace Admixet.Tests
{
    public class MatrixTests
    {
        [Test]
        public void Game_Update_Deletes_Sequences_From_Matrix()
        {
            int[,] startMatrix = new int[,]
            {
                {1, 2, 3, 0, 1, 2, 3, 1, 3},
                {1, 1, 1, 1, 1, 0, 3, 3, 1},
                {1, 2, 3, 0, 2, 2, 3, 1, 2},
                {1, 2, 3, 0, 1, 2, 2, 0, 0},
                {2, 0, 1, 2, 1, 1, 2, 3, 2},
                {3, 2, 3, 1, 3, 2, 1, 2, 2},
                {0, 0, 3, 0, 1, 3, 0, 1, 2},
                {0, 0, 1, 2, 2, 2, 3, 1, 3},
                {2, 0, 3, 0, 1, 2, 0, 1, 0}
            };
            int[,] resultMatrix = new int[,]
            {
                {7, 7, 7, 7, 7, 7, 7, 7, 7},
                {7, 7, 3, 7, 7, 2, 7, 7, 7},
                {7, 7, 3, 0, 1, 0, 7, 7, 7},
                {7, 7, 3, 0, 2, 2, 2, 1, 3},
                {2, 2, 1, 0, 1, 2, 2, 3, 1},
                {3, 2, 3, 2, 1, 1, 1, 1, 2},
                {0, 2, 3, 1, 3, 2, 0, 0, 0},
                {0, 0, 1, 0, 1, 3, 3, 3, 3},
                {2, 2, 3, 0, 1, 2, 0, 2, 0}
            };
            Game game = new(startMatrix);
            Game.DisplayMatrix(game.Matrix);
            game.Update();
            Game.DisplayMatrix(game.Indexes);
            Game.DisplayMatrix(game.Matrix);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (game.Matrix[i, j] != resultMatrix[i, j])
                        if (resultMatrix[i, j] != 7)
                            Assert.Fail();
                }
            }
            Assert.Pass();
        }
    }
}