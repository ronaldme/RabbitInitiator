using System.Threading.Tasks;
using EasyNetQ.Management.Client;
using EasyNetQ.Management.Client.Model;

namespace RabbitInitiator.Main
{
    public class Initiator
    {
        private ManagementClient GetClient(string host = "http://localhost", string user = "guest", string password = "guest")
            => new ManagementClient(host, user, password);

        public async Task CreateUser(string user, string password)
        {
            var client = GetClient();
            await client.CreateUserAsync(new UserInfo(user, password));
        }

        public async Task CreateUserAndVhost(string userName, string password, string vhostName)
        {
            var client = GetClient();

            var user = await client.CreateUserAsync(new UserInfo(userName, password));
            var vhost = await client.CreateVhostAsync(vhostName);
            await client.CreatePermissionAsync(new PermissionInfo(user, vhost));
        }

        public async Task GiveUserVhostPermissions(string userName, string vhostName)
        {
            var client = GetClient();

            var user = await client.GetUserAsync(userName);
            var vhost = await client.GetVhostAsync(vhostName);
            await client.CreatePermissionAsync(new PermissionInfo(user, vhost));
        }
    }
}
