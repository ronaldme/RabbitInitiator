using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using RabbitInitiator.Main;
using ReactiveUI;

namespace RabbitInitiator.WPF.ViewModels
{
    public class AppViewModel : ReactiveObject
    {
        private readonly Initiator _rabbitInitiator;

        public AppViewModel()
        {
            _rabbitInitiator = new Initiator();

            CreateUserAndVhost = ReactiveCommand.Create(CreateUserAndVhostImpl, GetCanExecute());
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

        public ICommand CreateUserAndVhost { get; set; }

        public IObservable<Task> CreateUserAndVhostImpl()
            => Observable.Start(async () => await _rabbitInitiator.CreateUserAndVhost(User, Password, Vhost));

        private IObservable<bool> GetCanExecute()
        {
            return this.WhenAnyValue(
                x => x.User,
                x => x.Password,
                x => x.Vhost,
                (userName, password, vhost) =>
                    !string.IsNullOrEmpty(userName) &&
                    !string.IsNullOrEmpty(password) &&
                    !string.IsNullOrEmpty(vhost));
        }
    }
}