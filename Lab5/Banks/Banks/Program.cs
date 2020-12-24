using System;
using System.Collections.Generic;
using System.Text;

namespace Banks
{
  class Program
  {
    static void Main(string[] args)
    {
      List<Bank> banks = new List<Bank>();
      //var bankAndClientId = new List<KeyValuePair<Bank, int>>();

      banks.Add(new Bank("Sberbank", 0.1));
      banks.Add(new Bank("Tinkoff", 4));
      banks.Add(new Bank("Alpha", 2));
      banks[0].MaxWithDrawValue = 100;
      banks[1].MaxWithDrawValue = 15000;
      banks[2].MaxWithDrawValue = 7000;
      banks[0].TransferMoneyCommission = 10;
      banks[1].TransferMoneyCommission = 2;
      banks[2].TransferMoneyCommission = 6;
      
      //var thirdClient = new BankClient(bankAndClientId.Count, "Petya", "Petrushin");      
      try
      {
        banks[0].AddClientToBase(new BankClient(banks[0].GetAvaliableIndex(), "Ivan", "Ivanov"),
                                 out int firstBankClientId);
        banks[1].AddClientToBase(new BankClient(banks[1].GetAvaliableIndex(), "Dmitry", "Leschikov", "ITMO", "239"),
                                 out int secondBankClientId);
        banks[2].AddClientToBase(new BankClient(banks[2].GetAvaliableIndex(), "Dmitry", "Leschikov", "ITMO", "239"),
                                 out int thirdBankClientId);
        banks[1].AddClientToBase(new BankClient(banks[1].GetAvaliableIndex(), "Slice", "Kekusov", "SPBU", "30"),
                                 out int newClientId);
        //bankAndClientId.Add(new KeyValuePair<Bank, int>(banks[0], firstBankClientId));
        //bankAndClientId.Add(new KeyValuePair<Bank, int>(banks[1], secondBankClientId));
        //bankAndClientId.Add(new KeyValuePair<Bank, int>(banks[2], thirdBankClientId));


        banks[0].GetClient(firstBankClientId, out BankClient firstClient);        
        firstClient.CreateBankAccount(BankAccountType.Deposit, 10000);
        firstClient.CreateBankAccount(BankAccountType.CreditAccount, 12000);
        firstClient.CreateBankAccount(BankAccountType.DebitAccount, 220000);
        banks[2].GetClient(thirdBankClientId, out BankClient secondClient);
        secondClient.CreateBankAccount(BankAccountType.DebitAccount, 1000000);
        secondClient.CreateBankAccount(BankAccountType.Deposit, 120000);
        secondClient.GetAccount(1, out BankAccount secondClientAccount);

        firstClient.AddAddress("Pushkina street, Kolotushkina home");
        firstClient.GetAccount(1, out BankAccount firstClientAccount);
        firstClientAccount.TopUpAccount(12000);
        firstClientAccount.WithdrawMoney(5000);

        //Added BackToTheFuture call

        Console.WriteLine(secondClientAccount.BackToTheFuture(DateTime.Now.AddMonths(10)));

        banks[0].TransferFromFirstAccountToSecond(banks[2], 2000, firstBankClientId, 1, thirdBankClientId, 1);
        banks[0].CancelTransaction(0);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"{ex.Message}");
      }
      // banks[0].GetClient(clientId).CreateBankAccount(BankAccountType.Deposit, 10000);
      //banks[0].GetClient(clientId).CreateBankAccount(BankAccountType.Deposit, 10000);

      //      banks[0].GetClient(clientId).GetAccount(1);
    }
  }
}
