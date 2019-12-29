using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace RabbitInitiator.Main
{
    public class ManagementPlugin
    {
        /// <summary>
        /// Enable the management plugin
        /// </summary>
        /// <param name="rabbitLocation">
        /// Installation location, e.g: C:\Program Files\RabbitMQ Server\rabbitmq_server-3.8.1
        /// </param>
        public async Task Enable(string rabbitLocation)
        {
            var process = new Process();
            var info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.RedirectStandardInput = true;
            info.WorkingDirectory = Path.Combine(rabbitLocation, "sbin");
            process.StartInfo = info;
            process.Start();

            await using (StreamWriter sw = process.StandardInput)
                sw.WriteLine("rabbitmq-plugins enable rabbitmq_management");

            await Task.Delay(6000); // HACK: time needed for enabling the plugin
        }
    }
}