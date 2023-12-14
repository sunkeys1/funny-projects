using System.Runtime.InteropServices;

namespace ConsoleLife
{
    internal class Program
    {
        private int resolution;
        private GameLogic gameLogic;
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int maximize = 3;
        static void Main(string[] args)
        {
            //Console.WriteLine("Density can be between 2-30\nThan less density than more life");
            //Console.Write("Enter density: ");
            //int dens = Convert.ToInt32(Console.ReadLine());
            Console.ReadKey();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(GetConsoleWindow(), maximize);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            var gameLogic = new GameLogic
            (
                rows: 62,
                cols: 315,
                density: 15
                //
                // 62
                // 315
                // 16 size text
                //
                // 125
                // 625
                // 8 size text
            );

            while (true)
            {
                Console.Title = gameLogic.currentGen.ToString();
                var gameField = gameLogic.GetCurrentGen();

                for(int y = 0; y < gameField.GetLength(1); y++)
                {
                    var strs = new char[gameField.GetLength(0)];
                    for(int x = 0; x < gameField.GetLength(0); x++)
                    {
                        if (gameField[x, y])
                        {
                            strs[x] = '*';
                        }
                        else
                        {
                            strs[x] = ' ';
                        }
                    }
                    Console.WriteLine(strs);
                }
                Console.SetCursorPosition(0, 0);
                gameLogic.Generations();
            }

        }
    }
}