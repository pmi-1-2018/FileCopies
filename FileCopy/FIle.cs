using System;
using System.Collections.Generic;
using System.Text;

namespace FileCopy
{
    class File
    {
        public File()
        {
            name = "unknown";
            directory = "unknown";
        }
        public File(string n, string dir)
        {
            name = n;
            directory = dir;
        }

        public string name { get; }
        public string directory { get; }
        public override string ToString()
        {
            return name + '\\' + directory;
        }
    }
}
