namespace BasicMvvmSample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        public BankAccountViewModel BankAccountViewModel { get; } = new BankAccountViewModel();
    }
}
