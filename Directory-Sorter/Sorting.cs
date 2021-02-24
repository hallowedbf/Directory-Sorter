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
        static int errors = 0;
        public static bool verbose = true;

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

        public static void DefaultSort(string directoryPath)
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

            foreach (string filePath in Directory.EnumerateFiles(directoryPath))
            {
                filesFound++;
                string fileExtension = Path.GetExtension(filePath);

                if (verbose == true)
                {
                    Console.WriteLine();
                    Console.WriteLine($"Found file: {Path.GetFileName(filePath)}");
                }

                string pathToMoveTo = null;
                if (fileTypeLocations.ContainsKey(fileExtension))
                {
                    fileTypeLocations.TryGetValue(fileExtension, out pathToMoveTo);
                    if(pathToMoveTo != null)
                    {
                        MoveFile(filePath, pathToMoveTo);
                    }
                    else
                    {
                        StoreFileTypeQuery(filePath);
                    }
                }
                else
                {
                    StoreFileTypeQuery(filePath);
                }

            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Done'd.");
            Console.WriteLine($"Files found: {filesFound}");
            Console.WriteLine($"Files moved: {filesMoved}");
            if(errors < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine($"Errors moving files: {errors}");
            filesFound = 0;
            filesMoved = 0;
            errors = 0;
            fileTypeLocations.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
            MainClass.DirectoryQuery();
        }

        public static void IsolateSort(string directoryPath)
        {

        }

        public static void CustomSort()
        {

        }

        public static void StoreFileTypeQuery(string filePath)
        {
            Console.WriteLine();
            Console.WriteLine($"Not sure where to put files of this type: {Path.GetExtension(filePath)} where would you like to store it?");
            Console.WriteLine("Enter a full path to the directory. This will be remembered when we come across the same file extension again.");
            Console.WriteLine();
            string path = Console.ReadLine();
            if(Directory.Exists(path))
            {
                fileTypeLocations.Add(Path.GetExtension(filePath), path);
                MoveFile(filePath, path);
            }
            else
            {
                Console.WriteLine("That is not a valid directory.");
                StoreFileTypeQuery(filePath);
            }
        }

        public static void MoveUnsureFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath);
            string pathToMoveTo = null;
            if (fileTypeLocations.ContainsKey(fileExtension))
            {
                fileTypeLocations.TryGetValue(fileExtension, out pathToMoveTo);
                try
                {
                    filesMoved++;
                    File.Move(filePath, pathToMoveTo);
                }
                catch(Exception e)
                {
                    errors++;
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                StoreFileTypeQuery(filePath);
            }
        }

        public static bool MoveFile(string fileToMove, string destinationDirectory)
        {
            try
            {
                if(verbose == true)
                {
                    Console.WriteLine($"Moving {Path.GetFileName(fileToMove)} to {destinationDirectory}");
                }
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
    }
}
