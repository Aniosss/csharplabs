using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BasicMvvmSample.Models;
using BasicMvvmSample.Helpers;

namespace BasicMvvmSample.ViewModels
{
    public class BankAccountViewModel : INotifyPropertyChanged
    {
        private BankAccount _bankAccount;
        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string BankName => _bankAccount.BankName;
        public string Inn => _bankAccount.Inn;
        public string Bik => _bankAccount.Bik;
        public string CorrespondentAccount => _bankAccount.CorrespondentAccount;
        public decimal Balance => _bankAccount.Balance;
        public decimal CurrentCredit => _bankAccount.CurrentCredit;

        private string _inputAmount = "";
        public string InputAmount
        {
            get => _inputAmount;
            set
            {
                if (_inputAmount != value)
                {
                    _inputAmount = value;
                    RaisePropertyChanged();
                }
            }
        }

        public ICommand DepositCommand { get; }
        public ICommand WithdrawCommand { get; }
        public ICommand TakeCreditCommand { get; }
        public ICommand RepayCreditCommand { get; }
        public ICommand AccrueInterestCommand { get; }

        public BankAccountViewModel()
        {
            _bankAccount = new BankAccount(
                bankName: "Банк Пример",
                inn: "1234567890",
                bik: "987654321",
                correspondentAccount: "000111222333",
                initialBalance: 1000m,
                withdrawalCommission: 0.02m,
                creditInterestRate: 0.05m);

            DepositCommand = new RelayCommand(Deposit);
            WithdrawCommand = new RelayCommand(Withdraw);
            TakeCreditCommand = new RelayCommand(TakeCredit);
            RepayCreditCommand = new RelayCommand(RepayCredit);
            AccrueInterestCommand = new RelayCommand(AccrueInterest);
        }

        private decimal ParseInputAmount()
        {
            if (decimal.TryParse(InputAmount, out decimal value))
                return value;
            return 0;
        }

        private void Deposit()
        {
            decimal amount = ParseInputAmount();
            if (amount > 0)
            {
                _bankAccount.Deposit(amount);
                RaisePropertyChanged(nameof(Balance));
            }
        }

        private void Withdraw()
        {
            decimal amount = ParseInputAmount();
            if (amount > 0)
            {
                if (_bankAccount.Withdraw(amount))
                    RaisePropertyChanged(nameof(Balance));
            }
        }

        private void TakeCredit()
        {
            decimal amount = ParseInputAmount();
            if (amount > 0)
            {
                _bankAccount.TakeCredit(amount);
                RaisePropertyChanged(nameof(Balance));
                RaisePropertyChanged(nameof(CurrentCredit));
            }
        }

        private void RepayCredit()
        {
            decimal amount = ParseInputAmount();
            if (amount > 0)
            {
                if (_bankAccount.RepayCredit(amount))
                {
                    RaisePropertyChanged(nameof(Balance));
                    RaisePropertyChanged(nameof(CurrentCredit));
                }
            }
        }

        private void AccrueInterest()
        {
            _bankAccount.AccrueCreditInterest();
            RaisePropertyChanged(nameof(CurrentCredit));
        }
    }
}
