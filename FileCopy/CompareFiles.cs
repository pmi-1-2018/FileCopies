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
                //Console.WriteLine("Copy: " + second.Directory + '\\' + second.Name);
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
            return false;
            //finally
            //{
            //    return false;
            //}
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
                        CompareFiles.FilesAreEqual(fileToFind, file);
                    }
                }

                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory, fileToFind);
            }
            catch (System.UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void CheckAllFiles(string directoryName, FileInfo file_to_find)
        {

            if (Directory.Exists(directoryName))
            {
                ProcessDirectory(directoryName, file_to_find);
            }
            else
            {
                Console.WriteLine("{0} is not a valid directory.", directoryName);
            }
        }


    }
}
