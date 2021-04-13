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
        public const string version = "1.0.0";
        static void Main(string[] args)
        {
            Sorting.Initialize();
            Dialog.IntroQuery();
        }

        
    }
}
