using System;

namespace BasicMvvmSample.Models
{
    public class BankAccount
    {
        public string BankName { get; }
        public string Inn { get; }
        public string Bik { get; }
        public string CorrespondentAccount { get; }

        public decimal Balance { get; private set; }

        public decimal WithdrawalCommission { get; }

        public decimal CreditInterestRate { get; }

        public decimal CurrentCredit { get; private set; }

        public BankAccount(
            string bankName,
            string inn,
            string bik,
            string correspondentAccount,
            decimal initialBalance,
            decimal withdrawalCommission,
            decimal creditInterestRate)
        {
            BankName = bankName;
            Inn = inn;
            Bik = bik;
            CorrespondentAccount = correspondentAccount;
            Balance = initialBalance;
            WithdrawalCommission = withdrawalCommission;
            CreditInterestRate = creditInterestRate;
            CurrentCredit = 0;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма пополнения должна быть положительной");
            Balance += amount;
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма снятия должна быть положительной");

            decimal commission = amount * WithdrawalCommission;
            decimal total = amount + commission;

            if (total > Balance)
                return false; 

            Balance -= total;
            return true;
        }

        public bool TakeCredit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма кредита должна быть положительной");

            Balance += amount;
            CurrentCredit += amount;
            return true;
        }

        public bool RepayCredit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Сумма погашения должна быть положительной");

            if (amount > Balance)
                return false; 

            if (amount > CurrentCredit)
                amount = CurrentCredit;

            Balance -= amount;
            CurrentCredit -= amount;
            return true;
        }

        public void AccrueCreditInterest()
        {
            if (CurrentCredit > 0)
            {
                decimal interest = CurrentCredit * CreditInterestRate;
                CurrentCredit += interest;
            }
        }
    }
}
