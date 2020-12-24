using System;
using System.Collections.Generic;
using System.Text;

namespace Banks
{
  internal class DebitAccount : BankAccount
  {
    private DateTime nextMonth;
    private double interestOnTheBalance;
    private readonly BankClient bankAccountOwner;
    private readonly Bank whatBankContainsThisBankAccount;
    private double balance;

    public DebitAccount( int accountId, BankClient client, Bank bank, double money )
    {
      CreationTime = DateTime.Now;
      nextMonth = DateTime.Now.AddMonths(1);
      interestOnTheBalance = money * bank.Percent;
      Id = accountId;
      bankAccountOwner = client;
      whatBankContainsThisBankAccount = bank;
      balance = money;
    }

    public override void InterestOnTheBalance()
    {
      interestOnTheBalance += balance * whatBankContainsThisBankAccount.Percent;

      if (DateTime.Now < nextMonth)  
        return;
      

      if (DateTime.Now == nextMonth)
        balance += interestOnTheBalance;
      nextMonth = nextMonth.AddMonths(1);
      interestOnTheBalance = 0;
    }

    public override void TopUpAccount(double money)
    {
      balance += money;
    }

    public override void WithdrawMoney(double money)
    {
      if (bankAccountOwner.ClientType == ClientType.doubtfulAccount)
        if (whatBankContainsThisBankAccount.MaxWithDrawValue < money)
          throw new Exception("MaxWithDrawValue greater than money");

      if (balance < money)
        throw new Exception();

      balance -= money;
    }

    public override void TransferToAnotherBankAccount(BankAccount bank, double money)
    {
      if (bankAccountOwner.ClientType == ClientType.doubtfulAccount)
        throw new Exception("Do not trust this client.");

      if (balance < money)
        throw new Exception();

      this.WithdrawMoney(money);
      bank.TopUpAccount(money);
    }

    public override double BackToTheFuture(DateTime futureDate)
    {
      DateTime temp = DateTime.Now;
      DateTime currentDate = DateTime.Now;
      int years = futureDate.Year - DateTime.Now.Year;
      int months = futureDate.Month - DateTime.Now.Month + years * 12;

      if (months == 0)
        throw new Exception("Nothing will happen");

      double tempMonthBalance = balance;
      double tempBalance = balance;

      for ( ; temp.Month - currentDate.Month + (temp.Year - currentDate.Year) * 12 < months;)
      {
        temp = temp.AddMonths(1);

        for (int count = 0; count < (temp - currentDate).TotalDays; count++)
          tempBalance += tempBalance * whatBankContainsThisBankAccount.Percent / 36500;

        tempMonthBalance = tempBalance;
      }

      return tempMonthBalance;
    }
  }
}
