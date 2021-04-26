using MVVM.Models;
using System;

namespace MVVM.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        private Account _account;

        public string AccountInfo => _account.ToString();

        public TimeSpan AccountAge => _account.GetAccountAge();

        public AccountViewModel()
        {
            _account = new Account
            {
                AccountHolderName = "MVVM User",
                Amount = 100.5,
                StartDate = new DateTime(2021, 1, 1)
            };
        }
    }
}