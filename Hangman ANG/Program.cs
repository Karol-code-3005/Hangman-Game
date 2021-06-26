using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hangman_z_neta
{
    class Program
    {
        static int zycie;
        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            string[] ListCandC = File.ReadAllLines(@"C:\Users\karol\Desktop\C#\countries_and_capitals.txt");
            Random random = new Random();
            int losowyIndex = random.Next(0, ListCandC.Length); 
            string wybraneCandC = ListCandC[losowyIndex]; 
            wybraneCandC = wybraneCandC.ToUpper();      
            Console.WriteLine(wybraneCandC); 

            string kraj = wybraneCandC.Substring(0, wybraneCandC.IndexOf("|") - 1);                  
            string stolica = wybraneCandC.Substring(wybraneCandC.IndexOf("|") + 2);            

            stopwatch.Start();
            Console.Title = ("hangman");
            string haslo = stolica;
            List<string> zgadnieteLitery = new List<string>();
            List<string> strzaly = new List<string>();
            zycie = 5;
            Console.WriteLine("Welcome to Hangman Game!");
            Console.WriteLine("Your task is to guess name of the capital which has {0} characters.", haslo.Length);
            Console.WriteLine("You have {0} health points!", zycie);
            //Ryzyko(stolica,zycie);
            Console.WriteLine("If you want to take a chance and guess the answer press [R], if not press enter.\nIf You guess the whole answer incorrectly You can lose two health points!");
            string ryzyko = Console.ReadLine();
            ryzyko = ryzyko.ToUpper();
            if (ryzyko == "R")
            {
                Console.WriteLine("You are brave! Enter your answer:");
                string odpowiedz = Console.ReadLine();
                odpowiedz = odpowiedz.ToUpper();
                if (odpowiedz == stolica)
                {
                    stopwatch.Stop();
                    Console.WriteLine("Well done! You guessed the password! You are the winner!");
                    zycie += 2;
                    Console.WriteLine("Your score {0}.", zycie);
                    Console.WriteLine("You guessed the answer by {0} letters.", strzaly.Count);
                    Console.WriteLine("The whole game took You {0} seconds", stopwatch.Elapsed);
                    Console.WriteLine("Enter Your name: ");
                    string name = Console.ReadLine();
                    int attempts = strzaly.Count;
                    DateTime date = DateTime.Now;
                    TimeSpan czas = stopwatch.Elapsed;
                    File.AppendAllText(@"C:\Users\karol\Desktop\C#\Scores.txt", name + "|" + zycie + "|" + czas + "|" + date + "|" + attempts + "|" + stolica + Environment.NewLine);
                    Console.WriteLine("Do You want to play one more time? [YES lub NO]");
                    string decyzja = Console.ReadLine();
                    decyzja = decyzja.ToUpper();
                    if (decyzja == "YES")
                    {
                        Main();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong answer! :(");
                    zycie -= 2;                                    
                    Console.WriteLine("You have {0} healt points left!", zycie);
                    Console.WriteLine("Enter the letter!");
                    Literki(haslo, zgadnieteLitery);
                }
            }
            else
            {
                Console.WriteLine("Enter the letter!");
                Literki(haslo, zgadnieteLitery);
            }

            while (zycie > 0)
            {
                string input = Console.ReadLine();
                input = input.ToUpper();
                strzaly.Add(input);
                Console.WriteLine("The letters you've already given: " + String.Join(", ", strzaly));
                if (zgadnieteLitery.Contains(input))
                {
                    Console.WriteLine("Hey! You have already given [{0}]!", input);
                    Console.WriteLine("Try other letter!");                          
                    continue;
                }
                zgadnieteLitery.Add(input);
                if(Slowo(haslo,zgadnieteLitery))
                {
                    stopwatch.Stop();
                    Console.WriteLine(haslo);
                    Console.WriteLine("Well done! You guessed the password! You are the winner!");
                    Console.WriteLine("Your score {0}.", zycie);
                    Staty();
                    Zapis();                  
                    Console.WriteLine("Do You want to play one more time? [YES lub NO]");
                    string decyzja = Console.ReadLine();
                    decyzja = decyzja.ToUpper();
                    if (decyzja == "YES")
                    {                        
                        Main();
                        Ryzyko(stolica);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else if (haslo.Contains(input))
                {
                    Console.WriteLine("Good shot!");                   
                    string literki = Literki(haslo, zgadnieteLitery);
                    Console.WriteLine(literki);
                    Podp();
                    Ryzyko(stolica);
                }
                else
                {
                    Console.WriteLine("Wrong! Try again!");
                    zycie -= 1;
                    Odj();
                    Podp();
                    Ryzyko(stolica);                   
                }

                void Odj()
                {                  
                    if (zycie <= 0)
                    {
                        stopwatch.Stop();                    
                        Console.WriteLine("GAME OVER! \nYou have no health points! :( \nThe answer is [{0}].", haslo);
                        Staty();
                        Zapis();
                        Console.WriteLine("Do You want to play one more time? [YES lub NO]");
                        string decyzja = Console.ReadLine();
                        decyzja = decyzja.ToUpper();
                        if (decyzja == "YES")
                        {
                            Main();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You have {0} healt points left!", zycie);                       
                        Literki(haslo, zgadnieteLitery);
                    }
                }
               
                void Ryzyko(string stolica)
                {
                    Console.WriteLine("If you want to take a chance and guess the answer press [R], if not press enter.\nIf You guess the whole answer incorrectly You can lose two health points!");
                    string ryzyko = Console.ReadLine();
                    ryzyko = ryzyko.ToUpper();
                    if (ryzyko == "R")
                    {
                        Console.WriteLine("You are brave! Enter your answer:");
                        string odpowiedz = Console.ReadLine();
                        odpowiedz = odpowiedz.ToUpper();
                        if (odpowiedz == stolica)
                        {
                            stopwatch.Stop();
                            Console.WriteLine("Well done! You guessed the password! You are the winner!");
                            zycie += 2;
                            Console.WriteLine("Your score {0}.", zycie);                        
                            Staty();
                            Zapis();
                            Console.WriteLine("Do You want to play one more time? [YES lub NO]");
                            string decyzja = Console.ReadLine();
                            decyzja = decyzja.ToUpper();
                            if (decyzja == "YES")
                            {
                                Main();
                            }
                            else
                            {
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong answer :(");
                            zycie -= 2;
                            Odj();
                            Podp();
                            Ryzyko(stolica);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Enter the letter!");
                        Literki(haslo, zgadnieteLitery);
                    }
                }

                void Podp()
                {
                    if (zycie == 1)
                    {
                        Console.WriteLine("Do You want a hint? [YES / NO]");
                        string odp = Console.ReadLine();
                        odp = odp.ToUpper();
                        if (odp == "YES")
                        {
                            Console.WriteLine("Your answer is the capital of {0}.", kraj);
                        }
                    }
                }

                void Zapis()
                {
                    Console.WriteLine("Enter Your name: ");
                    string name = Console.ReadLine();
                    int attempts = strzaly.Count;
                    DateTime date = DateTime.Now;
                    TimeSpan czas = stopwatch.Elapsed;
                    File.AppendAllText(@"C:\Users\karol\Desktop\C#\Scores.txt", name + "|" + zycie + "|" + czas + "|" + date + "|" + attempts + "|" + stolica + Environment.NewLine);
                }

                void Staty()
                {
                    Console.WriteLine("You guessed the answer by {0}, letters.", strzaly.Count);
                    Console.WriteLine("The whole game took You {0} seconds", stopwatch.Elapsed);
                }
            }
            Console.ReadKey();
            Environment.Exit(0);
        }

        static bool Slowo(string haslo, List<string> zgadnieteLiterki)
        {
            bool slowo = false;
            for (int i = 0; i < haslo.Length; i++)
            {
                string c = Convert.ToString(haslo[i]);
                if (zgadnieteLiterki.Contains(c))
                {
                    slowo = true;
                }
                else
                {
                    return slowo = false;
                }
            }
            return slowo;
        }

        static string Literki(string haslo, List<string> zgadnieteLiterki)
        {
            string poprawne = "";
            for (int i = 0; i < haslo.Length; i++)
            {
                string c = Convert.ToString(haslo[i]);
                if (zgadnieteLiterki.Contains(c))
                {
                    poprawne += c;
                }
                else
                {
                    poprawne += "_";
                }
            }
            return poprawne;
        }
    }
}