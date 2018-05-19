using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Delimon.Win32.IO;

namespace SplitFiles
{
    class Program
    {
        /// <summary>
        /// Fields 
        /// </summary>
        private static string 
            sourcePath = "", 
            searchPattern = "", 
            numericPath = "#";

        /// <summary>
        /// Main entry point
        /// TODO: Refactor to take args for source path, etc.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Enter Source Path: ");
            Console.Out.Flush();

            sourcePath = Console.ReadLine();

            Console.Write("File Mask (*) for all: ");
            Console.Out.Flush();

            searchPattern = Console.ReadLine();

            Console.WriteLine($"Proceeding with {sourcePath}\\{searchPattern}");

            if (!Delimon.Win32.IO.Directory.Exists(sourcePath))
            {
                Exit("Folder not found");
            }

            string[] files = Delimon.Win32.IO.Directory.GetFiles(sourcePath, searchPattern);

            for (int i = 0; i < files.Length; i++)
            {
                string fileName = Delimon.Win32.IO.Path.GetFileName(files[i]);
                string outputFolder = sourcePath + "\\output\\" + (!Char.IsLetter(fileName.ToCharArray()[0]) ? numericPath : fileName[0].ToString());
                string dest = outputFolder + "\\" + fileName;

                System.IO.Directory.CreateDirectory(outputFolder);
                Delimon.Win32.IO.File.Copy(files[i], dest, false);

                Console.WriteLine(dest);
            }

            Console.ReadKey();
            Environment.Exit(1);

        }

        private static void Exit(string error)
        {
            Console.WriteLine($"{error}. Press any key to quit");
            Console.ReadKey();
            System.Environment.Exit(1);
        }
    }
}
