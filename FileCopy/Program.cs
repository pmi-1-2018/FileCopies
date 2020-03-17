using System;
using System.Threading;

namespace FileCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Console.WriteLine("Hello, user! Type number of command or \"1\" to see avaiable commands");
            var command = Console.ReadLine();
            menu.Execute(Convert.ToInt32(command));

            while (Convert.ToInt32(command) != 0)
            {
                Console.WriteLine("What's next?");
                command = Console.ReadLine();
                menu.Execute(Convert.ToInt32(command));
            }
            Thread.Sleep(1000);
        }
    }
}
