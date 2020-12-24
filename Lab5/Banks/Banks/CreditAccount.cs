using System;
using System.Collections.Generic;
using System.Text;

namespace Banks
{
  internal class CreditAccount : BankAccount
  {
    public double CreditLimit { get; set; }
    private readonly BankClient bankAccountOwner;
    private readonly Bank whatBankContainsThisBankAccount;
    private double balance;

    public CreditAccount( int accountId, BankClient client, Bank bank )
    {
      CreationTime = DateTime.Now;      
      Id = accountId;
      bankAccountOwner = client;
      whatBankContainsThisBankAccount = bank;
      balance = 0;
    }
    
    public override void InterestOnTheBalance() 
    {
      return;
    }

    public override void TopUpAccount( double money )
    {
      balance += money;
    }

    public override void WithdrawMoney( double money )
    {
      if (bankAccountOwner.ClientType == ClientType.doubtfulAccount)
        if (whatBankContainsThisBankAccount.MaxWithDrawValue < money)
          throw new Exception("MaxWithDrawValue greater than money");

      if (balance - money < CreditLimit)
      {        
        var temp = CreditLimit - (balance - money);
        balance -= (money + temp * whatBankContainsThisBankAccount.TransferMoneyCommission);
      }
      else
        balance -= money;
    }

    public override void TransferToAnotherBankAccount( BankAccount bank, double money )
    {
      if (bankAccountOwner.ClientType == ClientType.doubtfulAccount)
        throw new Exception("Do not trust this guy.");

      this.WithdrawMoney( money );
      bank.TopUpAccount( money );
    }

    public override double BackToTheFuture(DateTime futureDate)
    {
      throw new Exception("Nothing will happen, Silly!");
    }
  }
}
