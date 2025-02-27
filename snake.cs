using System;
using System.Text;
using System.Threading;

namespace Snake
{
    internal class Program
    {
        static string difficulty = string.Empty;
        static int hight = 0;
        static int width = 0;
        static int score = 0;
        static int snakeLength = 3;
        static int[] snakeX = new int[100];
        static int[] snakeY = new int[100];
        static int appleX, appleY;
        static bool gameOver = false;
        static int speed = 200;
        static ConsoleKey currentDirection = ConsoleKey.RightArrow;
        static ConsoleKey lastDirection = ConsoleKey.RightArrow; 

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White; 
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.WriteLine("Hello and welcome to the snake game! Press enter to continue:");
            string nothing = Console.ReadLine();
            Console.WriteLine("Do you want to see the controls and rules? Type Yes or No");
            string rules = Console.ReadLine().ToLower();
            if (rules == "yes")
            {
                Controls();
                Console.WriteLine();
                Console.WriteLine();
            }
            Snake();
            Border();
            Position();
            while (!gameOver)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.LeftArrow || key == ConsoleKey.RightArrow ||
                        key == ConsoleKey.UpArrow || key == ConsoleKey.DownArrow)
                    {
                
                        if ((key == ConsoleKey.LeftArrow && currentDirection != ConsoleKey.RightArrow) ||
                            (key == ConsoleKey.RightArrow && currentDirection != ConsoleKey.LeftArrow) ||
                            (key == ConsoleKey.UpArrow && currentDirection != ConsoleKey.DownArrow) ||
                            (key == ConsoleKey.DownArrow && currentDirection != ConsoleKey.UpArrow))
                        {
                            lastDirection = currentDirection;
                            currentDirection = key;
                        }
                    }
                }
                MoveSnake(currentDirection);
                DrawSnake();
                BorderRepair();
                Console.SetCursorPosition(0, hight + 2);
                Console.WriteLine("Your score: " + score);
                Thread.Sleep(speed);
            }
            Console.WriteLine("Final score is: " + score);
        }

        static void Controls()
        {
            Console.WriteLine("Rules:");
            Console.WriteLine();
            Console.WriteLine("You are controlling a snake. It grows by 1 length each time you eat an apple.");
            Console.WriteLine("If you touch yourself or the borders you lose!");
            Console.WriteLine("The player wins if the snake reaches maximum length (different for each difficulty)!");
            Console.WriteLine();
            Console.WriteLine("Game controls");
            Console.WriteLine();
            Console.WriteLine("Movement:");
            Console.WriteLine("Left: ◄");
            Console.WriteLine("Right: ►");
            Console.WriteLine("Up: ▲");
            Console.WriteLine("Down: ▼");
        }

        static void Border()
        {
            Console.WriteLine("Select difficulty:");
            Console.WriteLine("Map sizes: easy 10, medium 20, hard 30.");
            difficulty = Console.ReadLine().ToLower();
            Console.Clear();
            switch (difficulty)
            {
                case "easy":
                    hight = 25;
                    width = 25;
                    speed = 200;
                    break;
                case "medium":
                    hight = 25;
                    width = 25;
                    speed = 100;
                    break;
                case "hard":
                    hight = 25;
                    width = 25;
                    speed = 50;
                    break;
            }
            Console.SetCursorPosition(0, 0);
            for (int xpos = 0; xpos < width; xpos++)
            {
                Console.Write('■' + " ");
            }
            for (int ypos = 0; ypos < hight; ypos++)
            {
                Console.WriteLine('■');
            }
            for (int zpos = 0; zpos < hight; zpos++)
            {
                Console.SetCursorPosition(2 * width, 0 + zpos);
                Console.Write('■');
            }
            Console.SetCursorPosition(0, hight);
            for (int xpos = 0; xpos < width; xpos++)
            {
                Console.Write('■' + " ");
            }
            Console.Write('■');
        }

        static void Position()
        {
            int parameterY = 0;
            int parameterX = 0;
            switch (difficulty)
            {
                case "easy":
                    parameterY = 25;
                    parameterX = 50;
                    break;
                case "medium":
                    parameterY = 25;
                    parameterX = 50;
                    break;
                case "hard":
                    parameterY = 25;
                    parameterX = 50;
                    break;
            }
            char apple = 'ó';
            Random random = new Random();
            appleX = random.Next(1, parameterX);
            appleY = random.Next(1, parameterY);
            Console.SetCursorPosition(appleX, appleY);
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine(apple);
            Console.SetCursorPosition(0, parameterY);
            Console.Write('■');
            Console.ForegroundColor = ConsoleColor.White; 
        }

        static void Snake()
        {
            for (int i = 0; i < snakeLength; i++)
            {
                snakeX[i] = 10 - i;
                snakeY[i] = 10;
            }
        }

        static void DrawSnake()
        {
            Console.ForegroundColor = ConsoleColor.Green; 
            for (int i = 0; i < snakeLength; i++)
            {
                Console.SetCursorPosition(snakeX[i], snakeY[i]);
                Console.Write('O');
            }
            Console.ForegroundColor = ConsoleColor.White; 
        }

        static void MoveSnake(ConsoleKey key)
        {
            for (int i = 1; i < snakeLength; i++)
            {
                if (snakeX[i] == snakeX[0] && snakeY[i] == snakeY[0])
                {
                    gameOver = true;
                    return;
                }
            }

            Console.SetCursorPosition(snakeX[snakeLength - 1], snakeY[snakeLength - 1]);
            Console.Write(' ');

            for (int i = snakeLength - 1; i > 0; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    snakeX[0]--;
                    break;
                case ConsoleKey.RightArrow:
                    snakeX[0]++;
                    break;
                case ConsoleKey.UpArrow:
                    snakeY[0]--;
                    break;
                case ConsoleKey.DownArrow:
                    snakeY[0]++;
                    break;
            }

            if (snakeX[0] <= 0 || snakeX[0] >= width * 2 || snakeY[0] <= 0 || snakeY[0] >= hight)
            {
                gameOver = true;
            }

            if (snakeX[0] == appleX && snakeY[0] == appleY)
            {
                score++;
                snakeLength++;
                Position();
            }
        }

        static void BorderRepair()
        {
            Console.SetCursorPosition(0, 0);
            for (int xpos = 0; xpos < width; xpos++)
            {
                Console.Write('■' + " ");
            }
            for (int ypos = 0; ypos < hight; ypos++)
            {
                Console.WriteLine('■');
            }
            for (int zpos = 0; zpos < hight; zpos++)
            {
                Console.SetCursorPosition(2 * width, 0 + zpos);
                Console.Write('■');
            }
            Console.SetCursorPosition(0, hight);
            for (int xpos = 0; xpos < width; xpos++)
            {
                Console.Write('■' + " ");
            }
            Console.Write('■');
        }
    }
}
