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
            });
        }
    }
}
