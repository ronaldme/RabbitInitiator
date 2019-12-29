using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RabbitInitiator.Main;

namespace RabbitInitiator
{
    class Program
    {
        private static readonly Initiator initiator = new Initiator();

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

            Console.WriteLine("Exiting program...");
        }

        private static async Task HandleMenuOptions(int val)
        {
            switch (val)
            {
                case 1:
                    break;
                case 2:
                    await CreateUser(val);
                    break;
                case 3:
                    await CreateUserAndVhostPermissions(val);
                    break;
            }
        }

        private static async Task CreateUserAndVhostPermissions(int val)
        {
            Console.WriteLine("Enter user/password/vhost as follows: user:password:vhost");

            var userPasswordAndVhost = Console.ReadLine();
            if (userPasswordAndVhost == null || !userPasswordAndVhost.Contains(":") || userPasswordAndVhost.Length < 5)
            {
                await HandleMenuOptions(val);
                return;
            }

            var splitted = userPasswordAndVhost.Split(":");
            if (splitted.Length != 3 || splitted.Any(string.IsNullOrEmpty))
            {
                await HandleMenuOptions(val);
                return;
            }

            await initiator.CreateUserAndVhost(splitted[0], splitted[1], splitted[2]);
        }

        private static async Task CreateUser(int val)
        {
            Console.WriteLine("Enter user/password as follows: user:password");

            var userAndPass = Console.ReadLine();
            if (userAndPass == null || !userAndPass.Contains(":") || userAndPass.Length < 3)
            {
                await HandleMenuOptions(val);
                return;
            }

            var splitted = userAndPass.Split(":");
            if (splitted.Length != 2 || splitted.Any(string.IsNullOrEmpty))
            {
                await HandleMenuOptions(val);
                return;
            }

            await initiator.CreateUser(splitted[0], splitted[1]);
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
