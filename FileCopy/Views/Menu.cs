using System;
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
        Delete_file = 6,
        File_info = 7,
        Move_file = 8,
        Exit = 0
    }
    public class Menu
    {
        Type com = typeof(Commands);


        public void DrawMenu()
        {
            char command = '1';
            while (Convert.ToInt32(command) != 0)
            {
                Console.Clear();
                Help();
                command = Console.ReadKey().KeyChar;
                Console.Clear();
                Execute(command);
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
            Console.WriteLine("Type the path to the file: ");
            var create = Console.ReadLine();
            Console.WriteLine("Type the name of the file: ");
            string name = Console.ReadLine();
            string path = $"{create}/{name}";
            try
            {
                System.IO.File.Create(path);
                Console.WriteLine("File created!");
            }
            catch(Exception e)
            {
                Console.WriteLine("Incorrect file path");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        public void Delete_File()
        {
            Console.WriteLine("Type the full path to the file: ");
            string file = Console.ReadLine();
            try
            {
                System.IO.File.Delete(file);
                Console.WriteLine("File deleted!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Incorrect file path");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
        public void File_info()
        {
            Console.WriteLine("Type the way to file: ");
            string path = $"{Console.ReadLine()}";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                Console.WriteLine("File name is : {0}", fileInf.Name);
                Console.WriteLine("Creation time: {0}", fileInf.CreationTime);
                Console.WriteLine("Size: {0}", fileInf.Length);
            }
            else
            {
                Console.WriteLine("File with that name doesn't exists, try again");
            }
            Console.ReadKey();
        }

        public void Move_file()
        {
            Console.WriteLine("Type the way to file: ");
            string path = $"{Console.ReadLine()}";
            Console.WriteLine("Type new way to file: ");
            string newPath = $"{Console.ReadLine()}";
            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.MoveTo(newPath);
                Console.WriteLine("Operation was successfully completed!");
            }
            else
            {
                Console.WriteLine("Something go wrong, please try again");
            }
            Console.ReadKey();
        }
        public void Execute(char command)
        {
            switch (command)
            {
                case '1':
                    Help();
                    break;
                case '2':
                    Memory();
                    break;
                case '3':
                    AllCopies.FindAllFiles(3);
                    break;
                case '4':
                    AllCopies.FindAllFiles(4);
                    break;
                case '5':
                    Create_File();
                    break;
                case '6':
                    Delete_File();
                    break;
                case '7':
                    File_info();
                    break;
                case '8':
                    Move_file();
                    break;
                case '0':
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
