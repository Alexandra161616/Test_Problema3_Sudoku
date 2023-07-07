using System;
using System.Collections.Generic;
using System.Linq;


namespace Test_Problema3
{
    class Sudoku_Solver
    {
        private char[] _nums = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        public void SolveSudoku(char[][] board)
        {
            if (board != null)
            {
                Solve(board);
            }
        }

        private bool Solve(char[][] board)
        {
            //Parcurgem toate spatiile din tabla

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == '.')
                    {
                        //daca gasim spatiu gol, incercam toate variantele de nr din nums si il alegem pe cel optim
                        //cel care nu se afla pe nicio linie si nicio coloana si nu se afla in patratul 3x3
                        foreach (var num in _nums)
                        {
                            //faci verificarea pt nr ales sa vedem daca e valid
                            if (_IsRowValid(board, i, num)
                                && _IsColumnValid(board, j, num)
                                && _IsBoxValid(board, i, j, num))
                            {
                                //adaugi nr ales si verifici tabla din nou
                                board[i][j] = num;
                                //Daca e valida tabla, am pus valoarea corecta
                                //Daca nu e valid, setam spatiul ca fiind gol si incercam din nou
                                if (Solve(board))
                                {
                                    return true;
                                }
                                else
                                {
                                    board[i][j] = '.';
                                }
                            }
                        }

                        return false;
                    }
                }
            }
            return true;
        }

        private bool _IsRowValid(char[][] board, int rowIndex, char num)
        {
            //Verificam daca un nr exista deja intr-o linie selectata
            var row = board[rowIndex];
            return !row.Any(x => x == num);
        }

        private bool _IsColumnValid(char[][] board, int columnIndex, char num)
        {
            //Verificam daca un nr exista deja intr-o coloana selectata
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i][columnIndex] == num)
                {
                    return false;
                }
            }
            return true;
        }

        private bool _IsBoxValid(char[][] board, int rowIndex, int columnIndex, char num)
        {
            //verifica in ce patrat fin tabla te afli
            var rowRange = _GetRangeForBox(rowIndex);
            var colRange = _GetRangeForBox(columnIndex);
            //verifica daca nr deja exista in patratul 3x3 ales
            for (int i = rowRange.Lower; i <= rowRange.Upper; i++)
            {
                for (int j = colRange.Lower; j <= colRange.Upper; j++)
                {
                    if (board[i][j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //Preia coordonatele pt patratul 3x3 in care te afli
        private (int Lower, int Upper) _GetRangeForBox(int i)
        {
            if (i < 3)
            {
                return (0, 2);
            }
            if (i < 6)
            {
                return (3, 5);
            }
            if (i < 9)
            {
                return (6, 8);
            }
            return default;
        }
      

        public static IEnumerable<(int Output, char[][] Input)> Input
        {
            get
            {
                return new List<(int Output, char[][] Input)>()
                {
                    (110,
                    new char[][]{
                        new char[]{'5','3','.','.','7','.','.','.','.'},
                        new char[]{'6','.','.','1','9','5','.','.','.'},
                        new char[]{'.','9','8','.','.','.','.','6','.'},
                        new char[]{'8','.','.','.','6','.','.','.','3'},
                        new char[]{'4','.','.','8','.','3','.','.','1'},
                        new char[]{'7','.','.','.','2','.','.','.','6'},
                        new char[]{'.','6','.','.','.','.','2','8','.'},
                        new char[]{'.','.','.','4','1','9','.','.','5'},
                        new char[]{'.','.','.','.','8','.','.','7','9'} })
                };
            }
        }
    }
    public class Program 
    {
        static void Main(string[] args)
        {
            var input = Sudoku_Solver.Input.First().Input;
            Sudoku_Solver solver = new Sudoku_Solver();
            solver.SolveSudoku(input);
            PrintSudoku(input);
            Console.ReadLine();
        }

        static void PrintSudoku(char[][] board)
        {
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    Console.Write(board[i][j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

 

}
