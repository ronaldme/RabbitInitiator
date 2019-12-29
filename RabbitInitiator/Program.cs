using System;
using System.Linq;
using System.Threading.Tasks;
using RabbitInitiator.Main;

namespace RabbitInitiator
{
    class Program
    {
        private static readonly Initiator Initiator = new Initiator();
        private static readonly ManagementPlugin ManagementPlugin = new ManagementPlugin();
        private static string rabbitLocation;

        static async Task Main(string[] args)
        {
            if (!string.IsNullOrEmpty(args[0])) rabbitLocation = args[0];

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
                    Console.WriteLine("Enabling RabbitMQ management plugin");
                    await ManagementPlugin.Enable(rabbitLocation);
                    Console.WriteLine("Done.");
                    break;
                case 2:
                    await CreateUser(val);
                    break;
                case 3:
                    await CreateUserAndVhostPermissions(val);
                    break;
                case 4:
                    SetRabbitLocation();
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

            await Initiator.CreateUserAndVhost(splitted[0], splitted[1], splitted[2]);
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

            await Initiator.CreateUser(splitted[0], splitted[1]);
        }

        private static void SetRabbitLocation()
        {
            Console.WriteLine(@"Enter rabbit install location example: C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.1");
            var location = Console.ReadLine();
            rabbitLocation = location;

            Console.WriteLine($"Location set to: {rabbitLocation}");
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Select an option.");
            Console.WriteLine("1: Setup RabbitMQ management plugin");
            Console.WriteLine("2: Create user");
            Console.WriteLine("3: Create user and vhost");
            Console.WriteLine($"4: Change rabbit installation location ({rabbitLocation})");
            Console.WriteLine();
            Console.WriteLine("Q: Quit");
        }
    }
}
