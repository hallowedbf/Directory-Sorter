using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Directory_Sorter
{
    class Sorting
    {
        static int filesFound = 0;
        static int filesMoved = 0;
        static int filesIgnored = 0;
        static int errors = 0;
        public static bool verbose = true;
        public static bool recursive = false;

        public static List<string> createdDirectories = new List<string>();
        public static Dictionary<string, string> movedFiles = new Dictionary<string, string>();
        public static List<string> recursiveFolderPaths = new List<string>();
        public static string initalDirectory = "";

        static string myDocuments;
        static string myPictures;
        static string myMusic;
        static string myVideos;
        static string desktop;

        static List<string> imageFileTypes;
        static List<string> audioFileTypes;
        static List<string> videoFileTypes;
        static List<string> shortcutFileTypes;
        static List<string> textFileTypes;

        //this is where we will store where the user decides they want specific file types to go
        //key is file type, value is where that file type should go
        public static Dictionary<string, string> fileTypeLocations = new Dictionary<string, string>();

        public static void Initialize()
        {
            Console.Clear();
            Console.WriteLine("Initializing...");

            myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            myPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            myMusic = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            myVideos = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
            desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            imageFileTypes = new List<string>();
            audioFileTypes = new List<string>();
            videoFileTypes = new List<string>();
            shortcutFileTypes = new List<string>();
            textFileTypes = new List<string>();

            #region Image Files
            imageFileTypes.Add(".jpg");
            imageFileTypes.Add(".jpeg");
            imageFileTypes.Add(".png");
            imageFileTypes.Add(".bmp");
            imageFileTypes.Add(".gif");
            imageFileTypes.Add(".jfif");
            imageFileTypes.Add(".webp");
            imageFileTypes.Add(".ppm");
            imageFileTypes.Add(".pgm");
            imageFileTypes.Add(".pbm");
            imageFileTypes.Add(".tiff");
            imageFileTypes.Add(".pdn");
            imageFileTypes.Add(".psd");
            imageFileTypes.Add(".cpt");
            imageFileTypes.Add(".kra");
            imageFileTypes.Add(".psp");
            imageFileTypes.Add(".tga");
            #endregion

            #region Audio Files
            audioFileTypes.Add(".aac");
            audioFileTypes.Add(".act");
            audioFileTypes.Add(".aiff");
            audioFileTypes.Add(".flac");
            audioFileTypes.Add(".m4a");
            audioFileTypes.Add(".mp3");
            audioFileTypes.Add(".mpc");
            audioFileTypes.Add(".nmf");
            audioFileTypes.Add(".ogg");
            audioFileTypes.Add(".oga");
            audioFileTypes.Add(".mogg");
            audioFileTypes.Add(".opus");
            audioFileTypes.Add(".org");
            audioFileTypes.Add(".wav");
            audioFileTypes.Add(".wma");
            audioFileTypes.Add(".mid");
            audioFileTypes.Add(".midi");
            #endregion

            #region Video Files
            videoFileTypes.Add(".webm");
            videoFileTypes.Add(".mkv");
            videoFileTypes.Add(".flv");
            videoFileTypes.Add(".ogv");
            videoFileTypes.Add(".gifv");
            videoFileTypes.Add(".avi");
            videoFileTypes.Add(".mov");
            videoFileTypes.Add(".qt");
            videoFileTypes.Add(".wmv");
            videoFileTypes.Add(".amv");
            videoFileTypes.Add(".mpg");
            videoFileTypes.Add(".mp2");
            videoFileTypes.Add(".mpeg");
            videoFileTypes.Add(".mpe");
            videoFileTypes.Add(".mpv");
            videoFileTypes.Add(".m2v");
            videoFileTypes.Add(".m4v");
            videoFileTypes.Add(".3gp");
            videoFileTypes.Add(".3g2");
            videoFileTypes.Add(".f4v");
            videoFileTypes.Add(".f4p");
            videoFileTypes.Add(".f4a");
            videoFileTypes.Add(".f4b");
            #endregion

            #region Shortcut Files
            shortcutFileTypes.Add(".lnk");
            shortcutFileTypes.Add(".url");
            #endregion

            #region Text Files
            textFileTypes.Add(".txt");
            textFileTypes.Add(".doc");
            textFileTypes.Add(".asc");
            textFileTypes.Add(".docx");
            #endregion

            Console.Clear();
        }

        public static void DefaultSortInitialize()
        {
            for (int i = 0; i < imageFileTypes.Count; i++)
            {
                fileTypeLocations.Add(imageFileTypes[i], myPictures);
            }

            for (int i = 0; i < audioFileTypes.Count; i++)
            {
                fileTypeLocations.Add(audioFileTypes[i], myMusic);
            }

            for (int i = 0; i < videoFileTypes.Count; i++)
            {
                fileTypeLocations.Add(videoFileTypes[i], myVideos);
            }

            for (int i = 0; i < shortcutFileTypes.Count; i++)
            {
                fileTypeLocations.Add(shortcutFileTypes[i], desktop);
            }

            for (int i = 0; i < textFileTypes.Count; i++)
            {
                fileTypeLocations.Add(textFileTypes[i], myDocuments);
            }
        }

        public static void DefaultSort(string directoryPath)
        {
            foreach (string filePath in Directory.EnumerateFiles(directoryPath))
            {
                filesFound++;
                string fileExtension = Path.GetExtension(filePath);

                if (verbose == true)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Found file: {Path.GetFileName(filePath)} in {Path.GetDirectoryName(filePath)}");
                }

                string pathToMoveTo = null;
                if (fileTypeLocations.ContainsKey(fileExtension))
                {
                    fileTypeLocations.TryGetValue(fileExtension, out pathToMoveTo);
                    if (pathToMoveTo != "DO NOT MOVE")
                    {
                        MoveFile(filePath, pathToMoveTo);
                    }
                    else
                    {
                        filesIgnored++;
                    }
                }
                else
                {
                    StoreFileTypeQuery(filePath);
                }

            }
        }

        public static void RecursiveFolderSearch(string directoryPath)
        {
            foreach (string directory in Directory.EnumerateDirectories(directoryPath))
            {
                recursiveFolderPaths.Add(directory);
                RecursiveFolderSearch(directory);
            }
        }

        public static void StartDefaultSort(string directoryPath, bool recursive)
        {
            DefaultSortInitialize();

            movedFiles.Clear();
            createdDirectories.Clear();

            initalDirectory = directoryPath;

            DefaultSort(directoryPath);

            if (recursive == true)
            {
                RecursiveFolderSearch(directoryPath);
                foreach(string path in recursiveFolderPaths)
                {
                    DefaultSort(path);
                }
            }

            SortingFinished();
        }

        public static void IsolateSort(string directoryPath)
        {
            foreach (string filePath in Directory.EnumerateFiles(directoryPath))
            {
                filesFound++;
                string fileExtension = Path.GetExtension(filePath);
                string destination = $@"{directoryPath}\{fileExtension} Folder";

                if(recursive == true)
                {
                    destination = $@"{initalDirectory}\{fileExtension} Folder";
                }

                if (verbose == true)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Found file: {Path.GetFileName(filePath)}");
                }

                if (Directory.Exists(destination))
                {
                    MoveFile(filePath, destination);
                }
                else
                {
                    Directory.CreateDirectory(destination);
                    createdDirectories.Add(destination);
                    MoveFile(filePath, destination);
                }
            }
        }

        public static void StartIsolateSort(string directoryPath, bool recursive)
        {
            movedFiles.Clear();
            createdDirectories.Clear();

            initalDirectory = directoryPath;

            IsolateSort(directoryPath);

            if(recursive == true)
            {
                RecursiveFolderSearch(directoryPath);
                foreach(string path in recursiveFolderPaths)
                {
                    IsolateSort(path);
                }
            }
            SortingFinished();
        }

        public static void CustomSort()
        {

        }

        public static void UndoSort()
        {
            string originalFilePath;
            foreach(string movedFile in movedFiles.Keys)
            {
                movedFiles.TryGetValue(movedFile, out originalFilePath);
                if(originalFilePath != null)
                {
                    File.Move(movedFile, originalFilePath);
                }
            }
        }

        public static void StoreFileTypeQuery(string filePath)
        {
            Console.WriteLine();
            Console.WriteLine($"Not sure where to put files of this type: {Path.GetExtension(filePath)} what would you like to do?");
            Console.WriteLine($"1. Specify a folder to store it into. | 2. Ignore this particular file. | 3. Ignore ALL files that end with {Path.GetExtension(filePath)}");
            Console.WriteLine();
            switch(Console.ReadKey().Key)
            {
                case ConsoleKey.D1:
                    {
                        Console.WriteLine($"Enter a full path to the directory you want to move all {Path.GetExtension(filePath)} files to from here on.");
                        Console.WriteLine();
                        string path = Console.ReadLine();
                        if (Directory.Exists(path))
                        {
                            fileTypeLocations.Add(Path.GetExtension(filePath), path);
                            MoveFile(filePath, path);
                        }
                        else
                        {
                            DirectoryNotFound(filePath, path);
                        }
                        break;
                    }

                case ConsoleKey.D2:
                    {
                        filesIgnored++;
                        break;
                    }
                case ConsoleKey.D3:
                    {
                        filesIgnored++;
                        fileTypeLocations.Add(Path.GetExtension(filePath), "DO NOT MOVE");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("That's not a 1, 2, or a 3.");
                        StoreFileTypeQuery(filePath);
                        break;
                    }
            }
        }

        public static void DirectoryNotFound(string filePath, string directory)
        {
            if(IsValidPath(directory))
            {
                Console.WriteLine($"{directory} is not an existing directory.");
                Console.WriteLine("Would you like to create this directory?");
                Console.WriteLine();
                Console.WriteLine("1. Yes | 2. No");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        {
                            try
                            {
                                Directory.CreateDirectory(directory);
                                MoveFile(filePath, directory);
                            }
                            catch(Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e.Message);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("Press any key to retry.");
                                Console.ReadKey();
                                DirectoryNotFound(filePath, directory);
                            }
                            break;
                        }
                    case ConsoleKey.D2:
                        {
                            StoreFileTypeQuery(filePath);
                            break;
                        }
                    default:
                        {
                            DirectoryNotFound(filePath, directory);
                            break;
                        }

                }
            }
            else
            {
                Console.WriteLine("That is not a valid path.");
                Console.WriteLine();
                StoreFileTypeQuery(filePath);
            }
        }

        public static bool MoveFile(string fileToMove, string destinationDirectory)
        {
            try
            {
                if(verbose == true)
                {
                    Console.WriteLine($"Moving {Path.GetFullPath(fileToMove)} to {destinationDirectory}");
                }
                movedFiles.Add(Path.GetFullPath(fileToMove), destinationDirectory + $@"\{Path.GetFileName(fileToMove)}");
                File.Move(fileToMove, destinationDirectory + $@"\{Path.GetFileName(fileToMove)}");
                filesMoved++;
                return true;
            }
            catch(Exception e)
            {
                errors++;
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static void SortingFinished()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done'd.");
            Console.WriteLine($"Files found: {filesFound}");
            Console.WriteLine($"Files moved: {filesMoved}");
            Console.WriteLine($"Files ignored: {filesIgnored}");
            if (errors < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($"Errors moving files: {errors}");
            filesFound = 0;
            filesMoved = 0;
            filesIgnored = 0;
            errors = 0;
            fileTypeLocations.Clear();
            recursiveFolderPaths.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
            MainClass.DirectoryQuery();
        }

        public static bool IsValidPath(string path, bool allowRelativePaths = false)
        {
            bool isValid = true;

            try
            {
                string fullPath = Path.GetFullPath(path);

                if (allowRelativePaths)
                {
                    isValid = Path.IsPathRooted(path);
                }
                else
                {
                    string root = Path.GetPathRoot(path);
                    isValid = string.IsNullOrEmpty(root.Trim(new char[] { '\\', '/' })) == false;
                }
            }
            catch (Exception ex)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
