﻿using System;
using System.IO;
using System.Threading;



namespace FileCopy
{
    enum Commands
    {
        Help = 1,
        Memory = 2,
        AllCopies = 3,
        CopiesInDefinedDirectory = 4,
        Create_file = 5,
        Exit = 0
    }
    public class Menu
    {
        Type com = typeof(Commands);


        public void DrawMenu()
        {
            string command = "1";
            while (Convert.ToInt32(command) != 0)
            {
                Console.Clear();
                Help();
                command = Console.ReadLine();
                Console.Clear();
                Execute(Convert.ToInt32(command));
            }
            Thread.Sleep(1000);
        }

        public void Help()
        {
            Console.WriteLine("Type a number of the command");
            Console.WriteLine("List of commands: ");
            foreach (var command in Enum.GetNames(com))
            {
                Console.WriteLine("{0,-11}= {1}", command, Enum.Format(com, Enum.Parse(com, command), "d"));
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
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        public void Create_File()
        {
            Console.WriteLine("Type the way to file: ");
            var create = Console.ReadLine();
            Console.WriteLine("Type the name of file: ");
            string name = Console.ReadLine();
            string path = $"{create}/{name}";
            System.IO.File.Create(path);
            Console.WriteLine("File created!");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        public void Execute(int command)
        {
            switch (command)
            {
                case 1:
                    Help();
                    break;
                case 2:
                    Memory();
                    break;
                case 3:
                    AllCopies.FindAllFiles(3);
                    break;
                case 4:
                    AllCopies.FindAllFiles(4);
                    break;
                case 5:
                    Create_File();
                    break;
                case 0:
                    Console.WriteLine("Goodbye^_^");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
