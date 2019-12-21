using System;
using System.Threading.Tasks;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;

namespace RabbitInitiator.Main
{
    public class Initiator
    {
        public async Task CreateUser(string user, string password)
        {
            var initial = new ManagementClient("http://localhost", "guest", "guest");
            await initial.CreateUserAsync(new UserInfo(user, password));
        }
    }
}
