using System.Reactive.Disposables;
using ReactiveUI;
using RabbitInitiator.WPF.ViewModels;

namespace RabbitInitiator.WPF
{
    public partial class MainWindow : ReactiveWindow<AppViewModel>
    {
        public MainWindow()
        {
            InitializeComponent(); 
            ViewModel = new AppViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.Bind(ViewModel,
                        viewModel => viewModel.User,
                        view => view.User.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.Password,
                        view => view.Password.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.Vhost,
                        view => view.VHost.Text)
                    .DisposeWith(disposableRegistration);

                this.Bind(ViewModel,
                        viewModel => viewModel.RabbitLocation,
                        view => view.RabbitLocation.Text)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                        viewModel => viewModel.CreateUserAndVhost,
                        view => view.CreateUserAndVhost)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(ViewModel,
                        viewModel => viewModel.EnableManagementPlugin,
                        view => view.EnableManagementPlugin)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
