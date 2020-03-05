using System;

namespace FileCopy
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            Console.WriteLine("Hello, user! Type command or \"help\" to see avaiable commands");
            var command = Console.ReadLine();
            menu.Execute(command);

            while (command != "exit")
            {
                Console.WriteLine("What's next?");
                command = Console.ReadLine();
                menu.Execute(command);
            }
            Thread.Sleep(1000);
        }
    }
}
