using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace FileCopy
{
    class AllCopies
    {
        static Hashtable table = new Hashtable();
        static List<List<string>> equelFiles = new List<List<string>>();
        static void AddInTable(long fileSize, string filePath)
        {
            if (table.ContainsKey(fileSize))
                ((List<string>)table[fileSize]).Add(filePath);
            else
            {
                table.Add(fileSize, new List<string>());
                ((List<string>)table[fileSize]).Add(filePath);
            }
        }

        public static void FindAllFiles()
        {
            //DriveInfo[] allDrives = DriveInfo.GetDrives();
            //foreach (DriveInfo drive in allDrives)
            //{
            var watch = Stopwatch.StartNew();
              ProcessAllFiles(@"D:\PostgreSQL\");
            watch.Stop();
            Console.WriteLine($"ProcessAll... {watch.ElapsedMilliseconds} ms");

            watch.Restart();
            long count = 0;
            foreach (List<string> ls in table.Values)
            {
                if (!ls.Count.Equals(1))
                {
                    List<string> equel = new List<string>();
                    for (int i = 0; i < ls.Count; i++)
                    {
                        for (int j = i+1; j < ls.Count; j++)
                        {
                            
                            if (CompareFiles.FilesAreEqual(new FileInfo(ls[i]), new FileInfo(ls[j])))
                            {
                                count++;
                                if (!equel.Contains(ls[i]))
                                    equel.Add(ls[i]);
                                if (!equel.Contains(ls[j]))
                                {
                                    equel.Add(ls[j]);
                                    ls.RemoveAt(j);
                                    j--;
                                }
                                
                            }
                        }
                    }
                    if(!equel.Count.Equals(0))
                        equelFiles.Add(equel);

                }
                
            }
            watch.Stop();
            Console.WriteLine($"Compare... {watch.ElapsedMilliseconds} ms");
            Console.WriteLine(count);

            foreach (List<string> res_ls in equelFiles)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < res_ls.Count; i++)
                {
                    Console.WriteLine(res_ls[i]);
                }

                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine($"Compare... {watch.ElapsedMilliseconds} ms");
            Console.WriteLine(count);
        }
        static void ProcessAllFiles(string targetDirectory)
        {
            try
            {
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                {
                    FileInfo file = new FileInfo(fileName);
                    AddInTable(file.Length,file.DirectoryName+"\\"+file.Name);
                }

                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessAllFiles(subdirectory);
            }
            catch (System.UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
            }


        }

    }
}
