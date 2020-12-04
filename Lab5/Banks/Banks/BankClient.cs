using System;
using System.Collections.Generic;
using System.Text;

namespace Banks
{
  enum ClientType { doubtfulAccount, trustAccount }

  internal class BankClient
  {
    public Bank thisBankClient;
    public ClientType ClientType { get; private set; }
    public int ClientId { get; }
    public string Name { get; }
    public string SurName { get; }
    public string Address { get; set; }
    public string PassportId { get; set; }

    private readonly List<BankAccount> bankAccounts;
    
    public BankClient( int id, string name, string surName, 
                       string address = "", string passportId = "" )
    {
      ClientType = ClientType.doubtfulAccount;
      ClientId = id;
      Name = name;
      SurName = surName;
      
      AddAddress(address);
      AddPassportId(passportId);

      bankAccounts = new List<BankAccount>();
    }

    public bool GetAccount(int accountId, out BankAccount account)
    {
      account = null;

      if (bankAccounts.Count <= accountId)
        return false;

      account = bankAccounts[accountId];
      return true;
    }

    public void AddBankInfo(Bank bank)
    {
      thisBankClient = bank;
    }

    public void AddAddress(string address)
    {
      if (PassportId != "")
        ClientType = ClientType.trustAccount;

      Address = address;
    }

    public void AddPassportId(string passportId)
    {
      if (Address != "")
        ClientType = ClientType.trustAccount;

      PassportId = passportId;
    }

    public void CreateBankAccount(BankAccountType type, double money = 0)
    {
      switch (type)
      {
        case BankAccountType.CreditAccount:
          bankAccounts.Add(new CreditAccount(bankAccounts.Count, this, thisBankClient));
          bankAccounts[^1].TopUpAccount(money);
          break;
        case BankAccountType.DebitAccount:
          bankAccounts.Add(new DebitAccount(bankAccounts.Count, this, thisBankClient, money));
          break;
        case BankAccountType.Deposit:
          bankAccounts.Add(new DebitAccount(bankAccounts.Count, this, thisBankClient, money));
          break;
      }
    }
  }
}
