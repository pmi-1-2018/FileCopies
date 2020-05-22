using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace FileCopy
{
    public class CompareFiles
    {
        const int BYTES_TO_READ = sizeof(Int64);
        public static bool FilesAreEqual(FileInfo first, FileInfo second)
        {
            try
            {
                if (first.Length != second.Length)
                    return false;

                int iterations = (int)Math.Ceiling((double)first.Length / BYTES_TO_READ);

                using (FileStream fs1 = first.OpenRead())
                using (FileStream fs2 = second.OpenRead())
                {
                    byte[] one = new byte[BYTES_TO_READ];
                    byte[] two = new byte[BYTES_TO_READ];

                    for (int i = 0; i < iterations; i++)
                    {
                        fs1.Read(one, 0, BYTES_TO_READ);
                        fs2.Read(two, 0, BYTES_TO_READ);

                        if (BitConverter.ToInt64(one, 0) != BitConverter.ToInt64(two, 0))
                            return false;
                    }
                }
                return true;
            }
            catch (System.IO.FileNotFoundException  e)
            {
                Console.WriteLine(e.Message);
            }
            catch(System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (System.UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }



        public static void AddInTable(Hashtable table, int fileSize, string filePath)
        {
            if (table.ContainsKey(fileSize))
                ((List<string>)table[fileSize]).Add(filePath);
            else
            {
                table.Add(fileSize, new List<string>());
                ((List<string>)table[fileSize]).Add(filePath);
            }
        }
    }

    public class RecursiveFileProcessor
    {
        private static List<string> copies = new List<string>();
        public static void ProcessDirectory(string targetDirectory, FileInfo fileToFind)
        {
            try
            {
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                using (fileToFind.OpenRead())
                {
                    foreach (string fileName in fileEntries)
                    {
                        FileInfo file = new FileInfo(fileName);
                        if (CompareFiles.FilesAreEqual(fileToFind, file))
                            copies.Add($"{file.Directory.FullName}\\{file.Name}");
                    }
                }

                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory, fileToFind);
            }
            catch (System.UnauthorizedAccessException e)
            {
                //Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            catch
            {
                Console.WriteLine("Something goes wrong");
            }
        }

        private static void printAllCopies()
        {
            for (int i = 0; i<copies.Count; i++)
			{
                Console.WriteLine($"{i+1}. {copies[i]}");
			}
        }
        public static void Find_One_File_Copies(string fileName)
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            FileInfo file_to_find = new FileInfo(fileName);
            foreach (DriveInfo drive in allDrives)
            {
                ProcessDirectory(drive.Name, file_to_find);
            }

            printAllCopies();
        }


    }
}
