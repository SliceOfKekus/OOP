using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;

using Exceptions;

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
      if (allItems.TryGetValue(itemId, out Item item))
      {
        if (allShops.TryGetValue(shopId, out Shop shop))
          shop.AddItemToCurrentShop(itemId, item, quantity, price);
        else
          throw new ShopDoesntExistException($"Shop with id:{shopId} doesn't exist.");
      }
      else
        throw new ItemDoesntExistException($"item with id:{itemId} doesn't exist.");
    }

    public string WhereIsTheCheapestItem(string itemId)
    {
      long costOfCheapestItem = int.MaxValue;
      long temp;
      bool containsSomewhere = false;
      string shopId = "";

      foreach (Shop shop in allShops.Values)
      {
        if (shop.DoesThisItemExistHere(itemId, out ItemInCurrentShop item))
        {
          containsSomewhere = true;
          if ((temp = item.GetCost()) <= costOfCheapestItem)
          {
            shopId = shop.ShopId;
            costOfCheapestItem = temp;
          }
        }
      }

      if (!containsSomewhere)
        throw new ItemDoesntContainsException($"item with id:{itemId} not funduk.");
      else
        return shopId;
    }

    public void WhatItemsICanBuyOnThisMoneyInThisShop(string shopId, long myMoney)
    {
      if (allShops.TryGetValue(shopId, out Shop shop))
      {
        foreach (ItemInCurrentShop item in shop.Values)
        {
          int count = 0;

          for (long cost = 0; cost <= myMoney; cost += item.GetCost())
            count++;

          Console.WriteLine($"On {myMoney} you can buy {count} times this item : {item.GetId()} aka {item.GetName()}");
        }
      }
      else
        throw new ShopDoesntExistException($"Shop with id:{shopId} doesn't exist.");
      
    }

    public void BuyItemsInThisShop(string shopId, Dictionary<string, long> itemsAndQuantity)
    {
      if (allShops.TryGetValue(shopId, out Shop shop))
      {
        long cost = 0;

        foreach (KeyValuePair<string, long> items in itemsAndQuantity)
        {
          if (shop.DoesThisItemExistHere(items.Key, out ItemInCurrentShop item))
            cost += shop.BuyItemInThisShop(item, items.Value);
        }
      }
      else
        throw new ShopDoesntExistException($"Shop with id:{shopId} doesn't exist.");
    }

    public string WhereIsTheCheapestItems(Dictionary<string, long> idAndQuantity)
    {
      SortedList<long, string> costAndId = new SortedList<long, string>();

      foreach (KeyValuePair<String, Shop> shop in allShops)
      {
        long tempCost = 0;
        bool itemContains = true;

        foreach (KeyValuePair<string, long> idQnty in idAndQuantity)
        {
          itemContains = true;
          if (shop.Value.DoesThisItemExistHere(idQnty.Key, out ItemInCurrentShop item))
          {
            if (item.GetQuantity() - idQnty.Value >= 0)
              tempCost += shop.Value.BuyItemInThisShop(item, idQnty.Value);
          }
          else
          {
            itemContains = false;
            break;
          }
        }

        if (itemContains)
        {
          costAndId.Add(tempCost, shop.Key);
        }
      }

      if (costAndId.Count != 0)
        return costAndId.Values[0];
      else
        throw new ItemDoesntContainsException("This combination of items doesn't contains ");
    }

  }
}
