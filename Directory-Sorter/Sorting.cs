using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Directory_Sorter
{
    class Sorting
    {
        public static List<string> knownFileTypes;
        public static List<string> imageFileTypes;
        public static List<string> audioFileTypes;
        public static List<string> videoFileTypes;
        public static List<string> shortcutFileTypes;
        public static void Initialize()
        {
            Console.Clear();
            Console.WriteLine("Initializing...");
            knownFileTypes = new List<string>();
            imageFileTypes = new List<string>();
            audioFileTypes = new List<string>();
            videoFileTypes = new List<string>();
            shortcutFileTypes = new List<string>();

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
            Console.Clear();
        }

        public static void DefaultSort(string directoryPath)
        {
            foreach (string filePath in Directory.EnumerateFiles(directoryPath))
            {
                
            }
            Console.ReadKey();
        }

        public static void IsolateSort(string directoryPath)
        {

        }

        public static bool IsImage(string filePath)
        {
            return true;
        }
    }
}
