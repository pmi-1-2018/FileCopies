using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FileCopy
{
    class AllCopies
    {
        static Hashtable table = new Hashtable();

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
}
