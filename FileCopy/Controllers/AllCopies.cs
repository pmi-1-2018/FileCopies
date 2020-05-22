﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace FileCopy
{
    class AllCopies
    {
        static Hashtable table = new Hashtable();
        static List<List<string>> equalFiles = new List<List<string>>();
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

        public static void FindAllFiles(int command)
        {
            Console.Clear();
            var watch = Stopwatch.StartNew();
            if (command == 3)
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in allDrives)
                {
                    ProcessAllFiles(drive.Name);
                }
            }
            if (command == 4)
            {
                string dir;
                Console.WriteLine("Write directory you want to find copies in : ");
                dir = Console.ReadLine();
                ProcessAllFiles(@"" + dir + "\\");
            }
            watch.Stop();
            Console.WriteLine($"Scanning lasted {watch.ElapsedMilliseconds/Math.Pow(10,5)} minutes");
            watch.Restart();
            int n = table.Count;
            var files = new List<string>[n];
            table.Values.CopyTo(files, 0);
            long count = 0;
            for (int i = 0; i < n; i++)
            {
                if (!files[i].Count.Equals(1))
                {
                    List<string> equal = new List<string>();
                    for (int k = 0; k < files[i].Count; k++)
                    {
                        for (int j = k + 1; j < files[i].Count; j++)
                        {

                            if (CompareFiles.FilesAreEqual(new FileInfo(files[i][k]), new FileInfo(files[i][j])))
                            {
                                count++;
                                if (!equal.Contains(files[i][k]))
                                    equal.Add(files[i][k]);
                                if (!equal.Contains(files[i][j]))
                                {
                                    equal.Add(files[i][j]);
                                    files[i].RemoveAt(j);
                                    j--;
                                }

                            }
                        }
                    }
                    if (!equal.Count.Equals(0))
                        equalFiles.Add(equal);

                }

            }
            watch.Stop();

            foreach (List<string> res_ls in equalFiles)
            {
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");
                for (int i = 0; i < res_ls.Count; i++)
                {
                    Console.WriteLine(res_ls[i]);
                }

                Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------------------");
            }
            Console.WriteLine($"Finding copies and comparing files lasted {watch.ElapsedMilliseconds / Math.Pow(10, 5)} minutes");
            Console.WriteLine("Found " + Convert.ToString(count) + " copies");
            Console.WriteLine("\n\n If you want to quit press q\n if you want to return to the menu press any other button");
            var c = Console.ReadKey();
            if (c.KeyChar == 'q')
            {
                Console.Clear();
                Console.WriteLine("Goodbye^_^");
                Environment.Exit(0);
            }
            Menu menu = new Menu();
            menu.DrawMenu();
        }
        static void ProcessAllFiles(string targetDirectory)
        {
            try
            {
                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                {
                    FileInfo file = new FileInfo(fileName);
                        AddInTable(file.Length, file.DirectoryName + "\\" + file.Name);
                }

                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                {
                    
                    ProcessAllFiles(subdirectory);
                }
            }
            catch (System.UnauthorizedAccessException e)
            {
                Console.WriteLine(e.Message);
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

    }
}
