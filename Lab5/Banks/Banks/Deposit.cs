using System;
using System.Collections.Generic;
using System.Text;

namespace Banks
{
  internal class Deposit : BankAccount
  {
    private DateTime timeToWithDraw;
    private DateTime nextMonth;
    private double interestOnTheBalance;
    private readonly BankClient bankAccountOwner;
    private readonly Bank whatBankContainsThisBankAccount;
    private double balance;
    private readonly double percentageOnBalance;

    public Deposit( int accountId, BankClient client, Bank bank , double money)
    {
      if (money <= 50000)
        percentageOnBalance = 3;

      if (money > 50000 && money <= 100000)
        percentageOnBalance = 3.5;

      if (money > 100000)
        percentageOnBalance = 4;

      timeToWithDraw = DateTime.Now.AddMinutes(2);
      CreationTime = DateTime.Now;
      bankAccountOwner = client;
      whatBankContainsThisBankAccount = bank;
      balance = money;
      Id = accountId;
    }
    public override void TopUpAccount( double money )
    {
      balance += money;
    }

    public override void InterestOnTheBalance()
    {
      interestOnTheBalance += balance * percentageOnBalance;

      if (DateTime.Now < nextMonth)
        return;


      if (DateTime.Now == nextMonth)
        balance += interestOnTheBalance;
      nextMonth = nextMonth.AddMonths(1);
      interestOnTheBalance = 0;
    }

    public override void WithdrawMoney( double money )
    {
      if (bankAccountOwner.ClientType == ClientType.doubtfulAccount)
        if (whatBankContainsThisBankAccount.MaxWithDrawValue < money)
          throw new Exception("MaxWithDrawValue greater than money");

      if (DateTime.Now < timeToWithDraw)
        throw new Exception("Can't withdraw money.");

      if (balance < money)
        throw new Exception("Not enough money on this deposit.");

      balance -= money;
    }

    public override void TransferToAnotherBankAccount( BankAccount bank, double money )
    {
      if (bankAccountOwner.ClientType == ClientType.doubtfulAccount)
        throw new Exception("Do not trust this client.");

      if (DateTime.Now < timeToWithDraw)
        throw new Exception("Can't transfer money.");

      if (balance < money)
        throw new Exception("Not enough money on balance.");

      this.WithdrawMoney( money );
      bank.TopUpAccount( money );
    }
  }
}
