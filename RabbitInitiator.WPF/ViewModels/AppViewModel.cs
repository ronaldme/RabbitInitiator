using System.Windows.Input;
using ReactiveUI;

namespace RabbitInitiator.WPF.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        public AppViewModel()
        {
            CreateUser = ReactiveCommand.Create(() =>
            {
                // TODO
            });
        }

        private string _user;
        public string User
        {
            get => _user;
            set => this.RaiseAndSetIfChanged(ref _user, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _vhost;
        public string Vhost
        {
            get => _vhost;
            set => this.RaiseAndSetIfChanged(ref _vhost, value);
        }

        public ICommand CreateUser { get; set; }
    }
}