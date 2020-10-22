using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Shop
{
  internal class ShopManager
  {
    readonly private Dictionary<string, Item> allItems;
    readonly private Dictionary<string, Shop> allShops;

    public ShopManager()
    {
      allItems = new Dictionary<string, Item>();
      allShops = new Dictionary<string, Shop>();
    }

    public void CreateShop(string id, string name, string address)
    { 
      if (!allShops.TryGetValue(id, out Shop temp))
      {
        allShops.Add(id, new Shop(id, name, address));
      }
      else
      {
        Console.WriteLine("This shop already exist.");
      }
    }

    public void CreateItem(string itemId, string name)
    {
      if (!allItems.TryGetValue(itemId, out Item temp))
      {
        allItems.Add(itemId, new Item(itemId, name));
      }
      else
      {
        Console.WriteLine("This item already exist.");
      }
    }

    public void AddToThisShopItem(string shopId, string itemId, long quantity, long price)
    {
      if (!allItems.TryGetValue(itemId, out Item item))
      {
        if (!allShops.TryGetValue(shopId, out Shop shop))
        {
          shop.AddItemToCurrentShop(itemId, item, quantity);
        }
        else
        {
          //throw exception "shop doesn't exist."
        }
      }
      else
      {
        //throw exception "item doesn't exist."
      }
    }

    public void WhereIsTheCheapestItem(string itemId)
    {
      long costOfCheapestItem = int.MaxValue;
      long temp;

      foreach (Shop shop in allShops.Values)
      {
        
        if (shop.DoesThisItemExistHere(itemId, out ItemInCurrentShop item))
        {
          if ((temp = item.GetCost()) <= costOfCheapestItem)
            costOfCheapestItem = temp;
        }
      }
      //What i have to do if this item contains nowhere?
    }

    public void WhatItemsICanBuyOnThisMoneyInThisShop(long myMoney)
    {
      foreach (Shop shop in allShops.Values)
      {
        foreach (ItemInCurrentShop item in shop.Values)
        {
          int count = 0;

          for (long cost = 0; cost <= myMoney; cost += item.GetCost())
          {
            count++;
          }

          Console.WriteLine($"On {myMoney} you can buy {count} times this item : {item.GetId()} aka {item.GetName()}");
        }
      }
    }

    public void BuyItemsInThisShop(string shopId, Dictionary<string, long> itemsAndQuantity)
    {
      if (!allShops.TryGetValue(shopId, out Shop shop))
      {
        foreach (KeyValuePair<string, long> items in itemsAndQuantity)
        {
          if (shop.DoesThisItemExistHere(items.Key, out ItemInCurrentShop item))
          {
            if (item.GetQuantity() - items.Value < 0)
            {
              //throw "This shop doesn't contain enough items you are looking for."
            }
            else
            {
              
            }
          }
        }
      }
      else
      {
        //throw "Shop doesn't exist."
      }
    }

    public void WhereIsTheCheapestItems()
    {
    }

  }
}
