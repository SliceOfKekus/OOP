using System;
using System.Collections.Generic;
using System.Text;

namespace Banks
{
  enum BankAccountType
  { CreditAccount, DebitAccount, Deposit }

  internal class Bank
  {
    public string Name { get; }
    public double MaxWithDrawValue {get; set; }
    public double TransferMoneyCommission { get; set; }
    public double Percent { get; private set; }
    private readonly Dictionary<int, BankClient> clientBase;
    private readonly List<Tuple<int, BankAccount, BankAccount, double>> transactions;

    public Bank(string name, double percent)
    {
      Name = name;
      Percent = percent;
      clientBase = new Dictionary<int, BankClient>();  
      transactions = new List<Tuple<int, BankAccount, BankAccount, double>>();
    }

    public int GetAvaliableIndex()
    {
      return clientBase.Count;
    }

    public void AddClientToBase( BankClient client, out int clientId )
    {
      if (clientBase.ContainsKey( client.ClientId ))
        throw new Exception("client already in clientBase.");

      clientId = clientBase.Count;
      client.AddBankInfo(this);
      clientBase.Add( client.ClientId, client );
    }

    public void AddAddressToThisClient(int clientId, string address)
    {
      if (!clientBase.TryGetValue(clientId, out BankClient client))
        throw new Exception("This client absent in client base.");

      client.AddAddress(address);
    }

    public void AddPassportIdToThisClient(int clientId, string passportId)
    {
      if (!clientBase.TryGetValue(clientId, out BankClient client))
        throw new Exception("This client absent in client base.");

      client.AddPassportId(passportId);
    }

    public bool GetClient(int clientId, out BankClient bankClient)
    {
      bankClient = null;
      if (!clientBase.TryGetValue(clientId, out BankClient client))
        return false;
      
      bankClient = client;
      return true;
    }

    public void TransferFromFirstAccountToSecond( int firstClientId, int firstBankAccountId,
                                                  int secondClientId, int secondBankAccountId,
                                                  double money )
    {
      if (!clientBase.TryGetValue(firstClientId, out BankClient firstClient))
        throw new Exception("This client absent in client base.");

      if (!clientBase.TryGetValue(secondClientId, out BankClient secondClient))
        throw new Exception("This client absent in client base.");
      
      if (!firstClient.GetAccount(firstBankAccountId, out BankAccount firstAccount))
        throw new Exception("This client absent in client base.");
      
      if (!secondClient.GetAccount(secondBankAccountId, out BankAccount secondAccount))
        throw new Exception("This client absent in client base.");

      transactions.Add(new Tuple<int, BankAccount, BankAccount, double>
                                 (transactions.Count, firstAccount, secondAccount, money));
      firstAccount.TransferToAnotherBankAccount(secondAccount, money);
    }

    public void TransferFromFirstAccountToSecond( Bank secondBank, double money, 
                                                  int firstClientId, int firstBankAccountId,
                                                  int secondClientId, int secondBankAccountId )
    {
      if (!clientBase.TryGetValue(firstClientId, out BankClient firstClient))
        throw new Exception("This client absent in client base.");

      if (!secondBank.GetClient(secondClientId, out BankClient secondClient))
        throw new Exception("This client absent in client base.");

      if (!firstClient.GetAccount(firstBankAccountId, out BankAccount firstAccount))
        throw new Exception("This client absent in client base.");

      if (!secondClient.GetAccount(secondBankAccountId, out BankAccount secondAccount))
        throw new Exception("This client absent in client base.");

      transactions.Add(new Tuple<int, BankAccount, BankAccount, double>
                                 (transactions.Count, firstAccount, secondAccount, money));
      firstAccount.TransferToAnotherBankAccount(secondAccount, money);
    }

    public void CancelTransaction( int transactionId )
    {
      var money = transactions[transactionId].Item4;

      transactions[transactionId].Item2.TopUpAccount(money);

      transactions[transactionId].Item3.WithdrawMoney(money);
    }
  }
}
