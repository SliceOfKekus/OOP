using System;
using System.Collections.Generic;
using System.Text;

namespace Banks
{
  internal abstract class BankAccount
  {
    public DateTime CreationTime { get; protected set; }
    public int Id { get; protected set; }
    public abstract void WithdrawMoney(double money);
    public abstract void TopUpAccount(double money);
    public abstract void TransferToAnotherBankAccount(BankAccount bank, double money);
    public abstract void InterestOnTheBalance();
  }
}
