using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCopy
{
    public class Menu
    {
        private string[] commands =
        {
            "help",
            "memory",
            "copies",
            "create_file",
            "exit"
        };

        public void Help()
        {
            Console.WriteLine("List of commands: ");
            foreach (var command in commands)
            {
                Console.WriteLine(command);
            }
        }

        public void Memory()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("  Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }
            }
        }
        public void Create_File()
        {
            Console.WriteLine("Type the way to file: ");
            var create = Console.ReadLine();
            Console.WriteLine("Type the name of file: ");
            string name = Console.ReadLine();
            string path = $"{create}/{name}";
            File.Create(path);
            Console.WriteLine("File created!");


        }
        public void Execute(string command)
        {
            switch (command)
            {
                case "help":
                    Help();
                    break;
                case "memory":
                    Memory();
                    break;
                case "copies":
                    break;
                case "create_file":
                    Create_File();
                    break;
                case "exit":
                    Console.WriteLine("Goodbye^_^");
                    break;
                default:
                    Console.WriteLine("Unknown comand");
                    break;
            }
        }
    }
}
