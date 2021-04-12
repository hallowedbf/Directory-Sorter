using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Directory_Sorter
{
    class MainClass
    {
        const string version = "1.0.0";
        static void Main(string[] args)
        {
            Sorting.Initialize();
            Console.WriteLine($"Directory-Sorter v{version}");
            Console.WriteLine("___________________________________________");
            IntroQuery();
        }

        public static void IntroQuery()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Directory-Sorter!");
            Console.WriteLine("Choose what you would like to do.");
            Console.WriteLine("1. Sort | 2. Undo last sort | 3. Exit");
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    {
                        DirectoryQuery();
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        Sorting.UndoLastSort();
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        Environment.Exit(0);
                        break;
                    }
                default:
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input.");
                        IntroQuery();
                        break;
                    }
            }
        }

        public static void UndoQuery()
        {
            Console.WriteLine();
            Console.WriteLine("Are you sure that you want to undo the last sort?");
            Console.WriteLine("This program only remembers the last sort done during this session.");
            Console.WriteLine("1. Yes | 2. No");
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    {
                        if(Sorting.movedFiles.Count != 0)
                        {
                            Sorting.UndoLastSort();
                        }
                        else
                        {
                            Console.WriteLine("There has been no sorting done this session.");
                            IntroQuery();
                        }
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        IntroQuery();
                        break;
                    }
                default:
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input.");
                        UndoQuery();
                        break;
                    }
            }
        }

        public static void UndoLostWarning()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Are you sure you want to start sorting again?");
            Console.WriteLine("This will overwrite the program's memory of your last sort, meaning you can NOT undo the last sort that was done.");
            Console.WriteLine("This does not matter if you're fine with the last sort.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Yes | 2. Cancel");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    {
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        IntroQuery();
                        break;
                    }
                default:
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input.");
                        UndoLostWarning();
                        break;
                    }
            }
        }

        public static void DirectoryQuery()
        {
            if (Sorting.movedFiles.Count != 0)
            {
                UndoLostWarning();
            }
            Console.WriteLine();
            Console.WriteLine("Choose a directory to sort.");
            Console.WriteLine("You will get to choose how you want the directory sorted before anything is moved.");
            Console.WriteLine("1. Desktop | 2. Downloads | 3. Documents | 4. Manually enter full directory path");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    {
                        SortQuery(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                        break;
                    }
                case ConsoleKey.D2:
                    {
                        SortQuery(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads");
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        SortQuery(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                        break;
                    }
                case ConsoleKey.D4:
                    {
                        Console.WriteLine();
                        Console.WriteLine("Enter a valid folder path.");
                        SortQuery(Console.ReadLine());
                        break;
                    }
                default:
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input.");
                        DirectoryQuery();
                        break;
                    }
            }
        }

        public static bool RecursiveQuery()
        {
            Console.WriteLine();
            Console.WriteLine($"Would you like the sorting to be recursive?");
            Console.WriteLine("This will make the program look through all sub-folders inside of the main folder you are wanting to organize.");
            Console.WriteLine();
            Console.WriteLine("1. Yes | 2. No");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    {
                        return true;
                    }
                case ConsoleKey.D2:
                    {
                        return false;
                    }
            }
            return false;
        }

        public static void SortQuery(string directoryPath)
        {
            if(!Sorting.IsValidPath(directoryPath))
            {
                Console.WriteLine();
                Console.WriteLine($"This is not a valid path: {directoryPath}");
                Console.WriteLine("Try checking for any typos.");
                DirectoryQuery();
            }
            else
            {
                if(!Directory.Exists(directoryPath))
                {
                    Console.WriteLine();
                    Console.WriteLine($"This directory does not exist: {directoryPath}");
                    Console.WriteLine("Try checking for any typos.");
                    DirectoryQuery();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"Directory found: {directoryPath}");
                    Console.WriteLine();
                    Console.WriteLine("What kind of sorting do you want done?");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("1. Default");
                    Console.WriteLine("For each file in this directory, this will check the file's file extension (.jpg, .mp3, .zip, etc) and attempt to");
                    Console.WriteLine("recognize it from a list of different file types I've hard-coded into the program.");
                    Console.WriteLine();
                    Console.WriteLine("It will take the files it recognizes and try to put them in common sense places like, pictures will go into your");
                    Console.WriteLine("Pictures folder, text files will go into your Documents folder, etc.");
                    Console.WriteLine();
                    Console.WriteLine("If it sees a file that it recognizes and/or doesn't know where to put it, it will ask you where it put files of that");
                    Console.WriteLine("type from then on. It will also do this if it comes across a file it does not recognize.");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("2. Isolate");
                    Console.WriteLine("For each file in this directory, this will check the file's file extension (.jpg, .mp3, .zip, etc) and make a");
                    Console.WriteLine("folder only for files with that exact file extension.");
                    Console.WriteLine();
                    Console.WriteLine("This can be useful for isolating all of the different files you have so you can move specific files you are");
                    Console.WriteLine("looking for out of a large mess.");
                    Console.WriteLine();
                    Console.WriteLine("If you have many different file types in this directory, this will make many different folders to look through");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("3. Go back");
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            {
                                Sorting.StartDefaultSort(directoryPath, RecursiveQuery());
                                break;
                            }
                        case ConsoleKey.D2:
                            {
                                Sorting.StartIsolateSort(directoryPath, RecursiveQuery());
                                break;
                            }
                        case ConsoleKey.D3:
                            {
                                DirectoryQuery();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine();
                                Console.WriteLine("Invalid input.");
                                SortQuery(directoryPath);
                                break;
                            }
                    }
                }
            }
        }
    }
}
