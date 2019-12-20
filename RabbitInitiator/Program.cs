using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;

namespace RabbitInitiator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                ShowMenu();

                var text = Console.ReadLine();
                if (text?.ToUpper() == "Q") break;
                Console.WriteLine();

                if (!int.TryParse(text, out int val))
                {
                    Console.WriteLine($"Invalid menu entry: {text}");
                    continue;
                }

                await HandleMenuOptions(val);
                Console.WriteLine();
            }

            Console.WriteLine("Exiting program");
        }

        private static async Task HandleMenuOptions(int val)
        {
            switch (val)
            {
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Select an option.");
            Console.WriteLine("1: Setup RabbitMQ management plugin");
            Console.WriteLine("2: Create user");
            Console.WriteLine("3: Create user and vhost");
            Console.WriteLine("Q: Quit");
            Console.WriteLine("Enter menu entry");
        }
    }
}
