using System;

namespace MVVM.Models
{
    public class Account
    {
        public string AccountHolderName { get; set; }

        public double Amount { get; set; }

        public DateTime StartDate { get; set; }

        public TimeSpan GetAccountAge() => DateTime.Now - StartDate;

        public override string ToString()
        {
            return $"{AccountHolderName} has {Amount}";
        }
    }
}