using System.Reflection;
using System.Windows;
using ReactiveUI;
using Splat;

namespace RabbitInitiator.WPF
{
    public partial class App : Application
    {
        public App()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        }
    }
}
