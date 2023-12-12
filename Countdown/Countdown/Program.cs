﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Countdown
{
    internal class Program
    {
        static int tempvaluetime = 0;
        static int gameTime = 0;
        static int gameLive = 5;
        static int gameScore = 0;
        static Random random = new Random();
        static string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static string[] numbersDecreasing = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static string[] listUnTouch = { "#", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        static int hareketSifir = 0;
        static bool isTrue2 = true;
        static int randomdir = 0;

        static int randomy = 0;
        static int randomx = 0;
        static string[,] gameField = new string[23, 53];

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //gameBeginner();
            ConsoleKeyInfo cki;


            //oyun alanının oluşması
            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    gameField[0, j] = "#";
                    gameField[22, j] = "#";
                    gameField[i, 0] = "#";
                    gameField[i, 52] = "#";
                    if (i > 0 && i < 22 && 0 < j && j < 52)
                        gameField[i, j] = " ";
                }
            }


            innitialWalls();


            while (true)
            {

                for (int i = 0; i < 70;)
                {
                    int number = random.Next(0, 10);
                    int numbery = random.Next(1, 52);
                    int numberx = random.Next(1, 22);
                    string tempvalue = gameField[numberx, numbery];
                    if (!numbers.Contains(tempvalue) && gameField[numberx, numbery] != "#")
                    {
                        gameField[numberx, numbery] = number.ToString();

                        i++;
                    }
                    else continue;

                }
                break;
            }
            //playerin oluşması
            int cursorx = 0;
            int cursory = 0;
            while (true)
            {
                cursorx = random.Next(1, 52);
                cursory = random.Next(1, 22);
                string tempvalue = gameField[cursory, cursorx];
                if (gameField[cursory, cursorx] != "#" && !numbers.Contains(tempvalue))
                {

                    break;
                }
            }


            for (int i = 0; i < gameField.GetLength(0); i++)
            {
                for (int j = 0; j < gameField.GetLength(1); j++)
                {
                    Console.Write(gameField[i, j]);
                }
                Console.WriteLine();
            }
            string[,] zeroPosition = new string[22, 52];
            string[,] playerPosition = new string[22, 52];
            while (1 <= gameLive)
            {


                Console.SetCursorPosition(59, 1);
                Console.WriteLine($"Time : {gameTime}");

                Console.SetCursorPosition(59, 3);
                Console.WriteLine($"Live : {gameLive}");

                Console.SetCursorPosition(59, 5);
                Console.WriteLine($"Score : {gameScore}");
                bool push = false; ;
                int counter = 1;
                int temp = 0;
                bool smash = false;
                while (Console.KeyAvailable)
                {

                    // true: there is a key in keyboard buffer
                    cki = Console.ReadKey(true);       // true: do not write character 

                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 51)
                    {
                        string currentCellValue = gameField[cursory, cursorx + 1];

                        string currentCellValueright = gameField[cursory, cursorx + 2];

                        // Eğer currentCellValue listNumber içinde yoksa işlemi yap
                        if (!listUnTouch.Contains(currentCellValue))
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(" ");
                            gameField[cursory, cursorx] = " ";
                            cursorx++;
                            gameField[cursory, cursorx] = "X";
                        }

                        else if (numbers.Contains(currentCellValue))
                        {

                            while (gameField[cursory, cursorx + counter] != " " && gameField[cursory, cursorx + counter] != "#")
                            {

                                counter++;

                            }
                            //bir sayı varsa sağ itilebilir
                            if (counter == 2) push = true;
                            // Duvar kontrolü eklenmiş olan for döngüsü
                            for (int i = counter - 1; i > 1; i--)
                            {
                                //iki iki kontrol ederek sondan azalan veya eşitse itilebilir
                                if (int.TryParse(gameField[cursory, cursorx + i], out temp) &&
                                    int.Parse(gameField[cursory, cursorx + i]) <= int.Parse(gameField[cursory, cursorx + i - 1]))
                                {
                                    push = true;
                                }
                                //diğer durumlarda itilemez ve break atar.
                                else
                                {

                                    push = false;
                                    break;


                                }

                            }
                            //smash kontrolü
                            if (counter == 2) smash = false;
                            else if (counter > 2) smash = true;


                            //dizinin sağı boşsa ve azalansa
                            if (gameField[cursory, cursorx + counter] == " " && push)
                            {
                                // Sondan başlayarak tüm sayıları sağa kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory, cursorx + i - 1], out temp))
                                    {
                                        gameField[cursory, cursorx + i - 1] = " ";
                                        gameField[cursory, cursorx + i] = temp.ToString();
                                        Console.SetCursorPosition(cursorx + i - 1, cursory);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx + i, cursory);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursorx++;
                                gameField[cursory, cursorx] = "X";
                            }
                            else if (gameField[cursory, cursorx + counter] == "#" && smash && push)
                            {
                                // Sondan başlayarak tüm sayıları sağa kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory, cursorx + i - 2], out temp))
                                    {
                                        gameField[cursory, cursorx + i - 2] = " ";
                                        gameField[cursory, cursorx + i - 1] = temp.ToString();
                                        Console.SetCursorPosition(cursorx + i - 2, cursory);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx + i - 1, cursory);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursorx++;
                                gameField[cursory, cursorx] = "X";
                            }
                        }



                    }


                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 1)
                    {
                        string currentCellValue = gameField[cursory, cursorx - 1];

                        // Eğer currentCellValue listNumber içinde yoksa işlemi yap
                        if (!listUnTouch.Contains(currentCellValue))
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(" ");
                            gameField[cursory, cursorx] = " ";
                            cursorx--;
                            gameField[cursory, cursorx] = "X";



                        }
                        else if (numbers.Contains(currentCellValue))
                        {

                            while (gameField[cursory, cursorx - counter] != " " && gameField[cursory, cursorx - counter] != "#")
                            {

                                counter++;

                            }
                            //bir sayı varsa sağ itilebilir
                            if (counter == 2) push = true;
                            // Duvar kontrolü eklenmiş olan for döngüsü
                            for (int i = counter - 1; i > 1; i--)
                            {
                                //iki iki kontrol ederek sondan azalan veya eşitse itilebilir
                                if (int.TryParse(gameField[cursory, cursorx - i], out temp) &&
                                    int.Parse(gameField[cursory, cursorx - i]) <= int.Parse(gameField[cursory, cursorx - i + 1]))
                                {
                                    push = true;
                                }
                                //diğer durumlarda itilemez ve break atar.
                                else
                                {

                                    push = false;
                                    break;


                                }

                            }
                            //smash kontrolü
                            if (counter == 2) smash = false;
                            else if (counter > 2) smash = true;


                            //dizinin sağı boşsa ve azalansa
                            if (gameField[cursory, cursorx - counter] == " " && push)
                            {
                                // Sondan başlayarak tüm sayıları sola kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory, cursorx - i + 1], out temp))
                                    {
                                        gameField[cursory, cursorx - i + 1] = " ";
                                        gameField[cursory, cursorx - i] = temp.ToString();
                                        Console.SetCursorPosition(cursorx - i + 1, cursory);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx - i, cursory);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursorx--;
                                gameField[cursory, cursorx] = "X";
                            }
                            else if (gameField[cursory, cursorx - counter] == "#" && smash && push)
                            {
                                // Sondan başlayarak tüm sayıları sola kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory, cursorx - i + 2], out temp))
                                    {
                                        gameField[cursory, cursorx - i + 2] = " ";
                                        gameField[cursory, cursorx - i + 1] = temp.ToString();
                                        Console.SetCursorPosition(cursorx - i + 2, cursory);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx - i + 1, cursory);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursorx--;
                                gameField[cursory, cursorx] = "X";
                            }
                        }
                    }



                    if (cki.Key == ConsoleKey.UpArrow && cursory > 1)
                    {
                        string currentCellValue = gameField[cursory - 1, cursorx];

                        // Eğer currentCellValue listNumber içinde yoksa işlemi yap
                        if (!listUnTouch.Contains(currentCellValue))
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(" ");
                            gameField[cursory, cursorx] = " ";
                            cursory--;
                            gameField[cursory, cursorx] = "X";


                        }
                        else if (numbers.Contains(currentCellValue))
                        {

                            while (gameField[cursory - counter, cursorx] != " " && gameField[cursory - counter, cursorx] != "#")
                            {

                                counter++;

                            }
                            //bir sayı varsa yukarı itilebilir
                            if (counter == 2) push = true;
                            // Duvar kontrolü eklenmiş olan for döngüsü
                            for (int i = counter - 1; i > 1; i--)
                            {
                                //iki iki kontrol ederek sondan azalan veya eşitse itilebilir
                                if (int.TryParse(gameField[cursory - i, cursorx], out temp) &&
                                    int.Parse(gameField[cursory - i, cursorx]) <= int.Parse(gameField[cursory - i + 1, cursorx]))
                                {
                                    push = true;
                                }
                                //diğer durumlarda itilemez ve break atar.
                                else
                                {

                                    push = false;
                                    break;


                                }

                            }
                            //smash kontrolü
                            if (counter == 2) smash = false;
                            else if (counter > 2) smash = true;


                            //dizinin sağı boşsa ve azalansa
                            if (gameField[cursory - counter, cursorx] == " " && push)
                            {
                                // Sondan başlayarak tüm sayıları sağa kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory - i + 1, cursorx], out temp))
                                    {
                                        gameField[cursory - i + 1, cursorx] = " ";
                                        gameField[cursory - i, cursorx] = temp.ToString();
                                        Console.SetCursorPosition(cursorx, cursory - i + 1);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx, cursory - i);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursory--;
                                gameField[cursory, cursorx] = "X";
                            }
                            else if (gameField[cursory - counter, cursorx] == "#" && smash && push)
                            {
                                // Sondan başlayarak tüm sayıları sağa kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory - i + 2, cursorx], out temp))
                                    {
                                        gameField[cursory - i + 2, cursorx] = " ";
                                        gameField[cursory - i + 1, cursorx] = temp.ToString();
                                        Console.SetCursorPosition(cursorx, cursory - i + 2);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx, cursory - i + 1);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursory--;
                                gameField[cursory, cursorx] = "X";
                            }
                        }



                    }


                    if (cki.Key == ConsoleKey.DownArrow && cursory < 21)
                    {
                        string currentCellValue = gameField[cursory + 1, cursorx];

                        // Eğer currentCellValue listNumber içinde yoksa işlemi yap
                        if (!listUnTouch.Contains(currentCellValue))
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(" ");
                            gameField[cursory, cursorx] = " ";
                            cursory++;
                            gameField[cursory, cursorx] = "X";

                        }
                        else if (numbers.Contains(currentCellValue))
                        {

                            while (gameField[cursory + counter, cursorx] != " " && gameField[cursory + counter, cursorx] != "#")
                            {

                                counter++;

                            }
                            //bir sayı varsa yukarı itilebilir
                            if (counter == 2) push = true;
                            // Duvar kontrolü eklenmiş olan for döngüsü
                            for (int i = counter - 1; i > 1; i--)
                            {
                                //iki iki kontrol ederek sondan azalan veya eşitse itilebilir
                                if (int.TryParse(gameField[cursory + i, cursorx], out temp) &&
                                    int.Parse(gameField[cursory + i, cursorx]) <= int.Parse(gameField[cursory + i - 1, cursorx]))
                                {
                                    push = true;
                                }
                                //diğer durumlarda itilemez ve break atar.
                                else
                                {

                                    push = false;
                                    break;


                                }

                            }
                            //smash kontrolü
                            if (counter == 2) smash = false;
                            else if (counter > 2) smash = true;


                            //dizinin sağı boşsa ve azalansa
                            if (gameField[cursory + counter, cursorx] == " " && push)
                            {
                                // Sondan başlayarak tüm sayıları sağa kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory + i - 1, cursorx], out temp))
                                    {
                                        gameField[cursory + i - 1, cursorx] = " ";
                                        gameField[cursory + i, cursorx] = temp.ToString();
                                        Console.SetCursorPosition(cursorx, cursory + i - 1);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx, cursory + i);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursory++;
                                gameField[cursory, cursorx] = "X";
                            }
                            else if (gameField[cursory + counter, cursorx] == "#" && smash && push)
                            {
                                // Sondan başlayarak tüm sayıları sağa kaydırma
                                for (int i = counter; i > 1; i--)
                                {
                                    if (int.TryParse(gameField[cursory + i - 2, cursorx], out temp))
                                    {
                                        gameField[cursory + i - 2, cursorx] = " ";
                                        gameField[cursory + i - 1, cursorx] = temp.ToString();
                                        Console.SetCursorPosition(cursorx, cursory + i - 2);
                                        Console.WriteLine(" ");
                                        Console.SetCursorPosition(cursorx, cursory + i - 1);
                                        Console.WriteLine(temp);
                                    }
                                }

                                Console.SetCursorPosition(cursorx, cursory);
                                Console.WriteLine(" ");
                                gameField[cursory, cursorx] = " ";
                                cursory++;
                                gameField[cursory, cursorx] = "X";
                            }
                        }








                    }



                }


                tempvaluetime++;
                if (tempvaluetime % 20 == 0)
                {

                    gameTime++;
                }
                if (tempvaluetime % 300 == 0)
                {
                    for (int i = 1; i < 22; i++)
                    {
                        for (int j = 1; j < 52; j++)
                        {

                            string tempvalue = gameField[i, j];
                            if (tempvalue == "1")
                            {
                                if (random.Next(1, 101) <= 3)

                                    gameField[i, j] = "0";



                            }
                            else if (numbersDecreasing.Contains(tempvalue))
                            {


                                int temppvalue = int.Parse(tempvalue) - 1;
                                gameField[i, j] = temppvalue.ToString();

                            }
                            else if (gameField[i, j] == "X") continue;
                            Console.SetCursorPosition(j, i);
                            Console.Write(gameField[i, j]);
                        }
                    }
                }
                bool[,] hasMoved = new bool[22, 52];
                if (tempvaluetime % 20 == 0)
                {
                    for (int i = 1; i < 22; i++)
                    {
                        for (int j = 1; j < 52; j++)
                        {
                            if (gameField[i, j] == "0" && !hasMoved[i, j])
                            {

                                hareketSifir = random.Next(0, 4);
                                Console.SetCursorPosition(j, i);
                                string tempvalue = gameField[i, j];
                                Console.ResetColor();


                                switch (hareketSifir)
                                {

                                    case 0:
                                        {
                                            if (gameField[i, j + 1] == " " || gameField[i, j + 1] == "X")
                                            {
                                                gameField[i, j] = " ";
                                                Console.WriteLine(" ");

                                                j++;
                                                if (gameField[i, j] == "X")
                                                {
                                                    Console.SetCursorPosition(j, i);
                                                    Console.WriteLine(" ");
                                                    gameLive--;
                                                    while (true)
                                                    {
                                                        cursorx = random.Next(1, 52);
                                                        cursory = random.Next(1, 22);
                                                        tempvalue = gameField[cursory, cursorx];
                                                        if (gameField[cursory, cursorx] != "#" && !numbers.Contains(tempvalue))
                                                        {

                                                            break;
                                                        }
                                                    }


                                                }

                                                gameField[i, j] = "0";
                                                hasMoved[i, j] = true;



                                            }
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (gameField[i - 1, j] == " " || gameField[i - 1, j] == "X")
                                            {
                                                Console.WriteLine(" ");
                                                gameField[i, j] = " ";
                                                i--;
                                                if (gameField[i, j] == "X")
                                                {
                                                    Console.SetCursorPosition(j, i);
                                                    Console.WriteLine(" ");
                                                    gameLive--;
                                                    while (true)
                                                    {
                                                        cursorx = random.Next(1, 52);
                                                        cursory = random.Next(1, 22);
                                                        tempvalue = gameField[cursory, cursorx];
                                                        if (gameField[cursory, cursorx] != "#" && !numbers.Contains(tempvalue))
                                                        {

                                                            break;
                                                        }
                                                    }


                                                }

                                                gameField[i, j] = "0";
                                                hasMoved[i, j] = true;


                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (gameField[i, j - 1] == " " || gameField[i, j - 1] == "X")
                                            {
                                                Console.WriteLine(" ");
                                                gameField[i, j] = " ";
                                                j--;
                                                if (gameField[i, j] == "X")
                                                {
                                                    Console.SetCursorPosition(j, i);
                                                    Console.WriteLine(" ");
                                                    gameLive--;
                                                    while (true)
                                                    {
                                                        cursorx = random.Next(1, 52);
                                                        cursory = random.Next(1, 22);
                                                        tempvalue = gameField[cursory, cursorx];
                                                        if (gameField[cursory, cursorx] != "#" && !numbers.Contains(tempvalue))
                                                        {

                                                            break;
                                                        }
                                                    }


                                                }

                                                gameField[i, j] = "0";
                                                hasMoved[i, j] = true;


                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (gameField[i + 1, j] == " " || gameField[i + 1, j] == "X")
                                            {
                                                Console.WriteLine(" ");
                                                gameField[i, j] = " ";
                                                i++;
                                                if (gameField[i, j] == "X")
                                                {

                                                    Console.SetCursorPosition(j, i);
                                                    Console.WriteLine(" ");
                                                    gameLive--;
                                                    while (true)
                                                    {
                                                        cursorx = random.Next(1, 52);
                                                        cursory = random.Next(1, 22);
                                                        tempvalue = gameField[cursory, cursorx];
                                                        if (gameField[cursory, cursorx] != "#" && !numbers.Contains(tempvalue))
                                                        {

                                                            break;
                                                        }
                                                    }


                                                }

                                                gameField[i, j] = "0";
                                                hasMoved[i, j] = true;

                                            }
                                            break;
                                        }
                                }
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.SetCursorPosition(j, i);
                                Console.Write(gameField[i, j]);

                            }
                        }
                    }
                }


                Console.ResetColor();







                Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position)

                Console.WriteLine("X");

                Thread.Sleep(50);     // sleep 50 ms













            }

        }

        static void gameBeginner()
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("▄████████  ▄██████▄  ███    █▄  ███▄▄▄▄       ███     ████████▄   ▄██████▄   ▄█     █▄  ███▄▄▄▄   ");
            Console.WriteLine("███    ███ ███    ███ ███    ███ ███▀▀▀██▄ ▀█████████▄ ███   ▀███ ███    ███ ███     ███ ███▀▀▀██▄ ");
            Console.WriteLine("███    █▀  ███    ███ ███    ███ ███   ███    ▀███▀▀██ ███    ███ ███    ███ ███     ███ ███   ███ ");
            Console.WriteLine("███        ███    ███ ███    ███ ███   ███     ███   ▀ ███    ███ ███    ███ ███     ███ ███   ███ ");
            Console.WriteLine("███        ███    ███ ███    ███ ███   ███     ███     ███    ███ ███    ███ ███     ███ ███   ███");
            Console.WriteLine("███    █▄  ███    ███ ███    ███ ███   ███     ███     ███    ███ ███    ███ ███     ███ ███   ███ ");
            Console.WriteLine("███    ███ ███    ███ ███    ███ ███   ███     ███     ███   ▄███ ███    ███ ███ ▄█▄ ███ ███   ███");
            Console.WriteLine("████████▀   ▀██████▀  ████████▀   ▀█   █▀     ▄████▀   ████████▀   ▀██████▀   ▀███▀███▀   ▀█   █▀  ");

            Console.SetCursorPosition(37, 9);
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.WriteLine("Herhangi bir tuşa basınız...");

            Console.ReadKey();

            Console.Clear();
            Console.SetCursorPosition(49, 15);
            Console.Write("Oyun Yükleniyor ");

            for (int i = 0; i < 7; i++)
            {
                Console.Write("■");
                Thread.Sleep(100); // Her noktadan sonra kısa bir bekleme
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(33, 6);
            Console.WriteLine("                      :::!~!!!!!:.");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 7);
            Console.WriteLine("                  .xUHWH!! !!?M88WHX:.");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 8);
            Console.WriteLine("                .X*#M@$!!  !X!M$$$$$$WWx:.");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 9);
            Console.WriteLine("               :!!!!!!?H! :!$!$$$$$$$$$$8X:");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 10);
            Console.WriteLine("              !!~  ~:~!! :~!$!#$$$$$$$$$$8X: ");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 11);
            Console.WriteLine("             :!~::!H!<   ~.U$X!?R$$$$$$$$MM!");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 12);
            Console.WriteLine("             ~!~!!!!~~ .:XW$$$U!!?$$$$$$RMM! ");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 13);
            Console.WriteLine("              !:~~~ .:!M'T#$$$$WX??#MRRMMM!");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 14);
            Console.WriteLine("              ~?WuxiW*`   `'#$$$$8!!!!??!!!");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 15);
            Console.WriteLine("             :X- M$$$$       `'T#$T~!8$WUXU~");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 16);
            Console.WriteLine("            :%`  ~#$$$m:        ~!~ ?$$$$$$");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 17);
            Console.WriteLine("          :!`.-   ~T$$$$8xx.  .xWW- ~''##*'");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 18);
            Console.WriteLine(".....   -~~:<` !    ~?T#$$@@W@*?$$      /`");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 19);
            Console.WriteLine("W$@@M!!! .!~~ !!     .:XUW$W!~ `'~:    :");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 20);
            Console.WriteLine("#'~~`.:x%`!!  !H:   !WM$$$$Ti.: .!WUn+!`");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 21);
            Console.WriteLine(":::~:!!`:X~ .: ?H.!u '$$$B$$$!W:U!T$$M~");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 22);
            Console.WriteLine(".~~   :X@!.-~   ?@WTWo('*$$$W$TH$! `");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 23);
            Console.WriteLine("Wi.~!X$?!-~    : ?$$$B$Wu('**|RM!");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 24);
            Console.WriteLine("$R@i.~~ !     :   ~$$$$$B$$en:``");
            Thread.Sleep(150);
            Console.SetCursorPosition(33, 25);
            Console.WriteLine("?MXT@Wx.~    :     ~'##*$$$$M~");
            Thread.Sleep(1700);



            Console.ResetColor();

            Console.Clear();
        }

        static void innitialWalls()
        {
            for (int i = 0; i < 3; i++)
            {
                bool isTrue = false;

                while (!isTrue)
                {

                    randomdir = random.Next(1, 3);
                    isTrue2 = true;
                    if (randomdir == 1)
                    {
                        randomy = random.Next(2, 41);
                        randomx = random.Next(2, 21);
                    }
                    else
                    {
                        randomy = random.Next(2, 51);
                        randomx = random.Next(2, 11);
                    }

                    if (randomdir == 1)
                    {
                        for (int k = randomx - 1; k <= randomx + 1; k++)
                        {
                            for (int l = randomy - 1; l <= randomy + 12; l++)
                            {
                                if (gameField[k, l] == "#")
                                {
                                    isTrue2 = false;

                                }

                            }

                        }
                        if (isTrue2)
                        {
                            for (int j = 0; j < 11; j++)
                            {

                                gameField[randomx, randomy + j] = "#";



                                isTrue = true;


                            }
                        }


                    }
                    else if (randomdir == 2)
                    {
                        for (int k = randomy - 1; k <= randomy + 1; k++)
                        {
                            for (int l = randomx - 1; l <= randomx + 12; l++)
                            {
                                if (gameField[l, k] == "#")
                                    isTrue2 = false;

                            }

                        }
                        if (isTrue2)
                        {
                            for (int j = 0; j < 11; j++)
                            {

                                gameField[randomx + j, randomy] = "#";

                                isTrue = true;





                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                bool isTrue = false;

                while (!isTrue)
                {

                    randomdir = random.Next(1, 3);
                    isTrue2 = true;
                    if (randomdir == 1)
                    {
                        randomy = random.Next(2, 45);
                        randomx = random.Next(2, 21);
                    }
                    else
                    {
                        randomy = random.Next(2, 51);
                        randomx = random.Next(2, 15);
                    }

                    if (randomdir == 1)
                    {
                        for (int k = randomx - 1; k <= randomx + 1; k++)
                        {
                            for (int l = randomy - 1; l <= randomy + 8; l++)
                            {
                                if (gameField[k, l] == "#")
                                {
                                    isTrue2 = false;

                                }

                            }

                        }
                        if (isTrue2)
                        {
                            for (int j = 0; j < 7; j++)
                            {

                                gameField[randomx, randomy + j] = "#";



                                isTrue = true;


                            }
                        }


                    }
                    else if (randomdir == 2)
                    {
                        for (int k = randomy - 1; k <= randomy + 1; k++)
                        {
                            for (int l = randomx - 1; l <= randomx + 8; l++)
                            {
                                if (gameField[l, k] == "#")
                                    isTrue2 = false;

                            }

                        }
                        if (isTrue2)
                        {
                            for (int j = 0; j < 7; j++)
                            {

                                gameField[randomx + j, randomy] = "#";

                                isTrue = true;





                            }
                        }
                    }
                }
            }
            for (int i = 0; i < 20; i++)
            {
                bool isTrue = false;

                while (!isTrue)
                {

                    randomdir = random.Next(1, 3);
                    isTrue2 = true;
                    if (randomdir == 1)
                    {
                        randomy = random.Next(2, 48);
                        randomx = random.Next(2, 21);
                    }
                    else
                    {
                        randomy = random.Next(2, 51);
                        randomx = random.Next(2, 19);
                    }

                    if (randomdir == 1)
                    {
                        for (int k = randomx - 1; k <= randomx + 1; k++)
                        {
                            for (int l = randomy - 1; l <= randomy + 4; l++)
                            {
                                if (gameField[k, l] == "#")
                                {
                                    isTrue2 = false;

                                }

                            }

                        }
                        if (isTrue2)
                        {
                            for (int j = 0; j < 3; j++)
                            {

                                gameField[randomx, randomy + j] = "#";



                                isTrue = true;


                            }
                        }


                    }
                    else if (randomdir == 2)
                    {
                        for (int k = randomy - 1; k <= randomy + 1; k++)
                        {
                            for (int l = randomx - 1; l <= randomx + 4; l++)
                            {
                                if (gameField[l, k] == "#")
                                    isTrue2 = false;

                            }

                        }
                        if (isTrue2)
                        {
                            for (int j = 0; j < 3; j++)
                            {

                                gameField[randomx + j, randomy] = "#";

                                isTrue = true;





                            }
                        }
                    }
                }
            }
        }
















    }
}




