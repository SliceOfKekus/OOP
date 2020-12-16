using System;
using System.Collections.Generic;

using Shop;
using Exceptions;

namespace Program
{
  class Program
  {
    static void Main(string[] args)
    {
      ShopManager manager = new ShopManager();
      Dictionary<string, long> listOfItems = new Dictionary<string, long>();

      // Creating shops...
      manager.CreateShop("1", "Tompson", "OG");
      manager.CreateShop("2", "Johan N0tail", "Fnatic");
      manager.CreateShop("3", "SliceOfKekus", "NRG");
      
      //creating items...
      manager.CreateItem("1", "Best KDA");
      manager.CreateItem("2", "CoolGuy");
      manager.CreateItem("3", "This guy has no chill");
      manager.CreateItem("4", "Coke");
      manager.CreateItem("5", "TESS tea");
      manager.CreateItem("6", "DoDo pizza");
      manager.CreateItem("7", "Dodster spicy");
      manager.CreateItem("8", "Pencil");
      manager.CreateItem("9", "Nike air force 1");
      manager.CreateItem("10", "239sucks");
      try
      {
        manager.AddToThisShopItem("1", "2", 10, 100);
        manager.AddToThisShopItem("2", "2", 15, 150);
        manager.AddToThisShopItem("3", "2", 5, 70);
        manager.WhereIsTheCheapestItem("2");

        manager.AddToThisShopItem("1", "9", 10, 10000);
        manager.AddToThisShopItem("2", "8", 15, 20);
        manager.AddToThisShopItem("3", "9", 5, 7000);

        listOfItems.Add("2", 7);
        listOfItems.Add("8", 15);

        manager.WhatItemsICanBuyOnThisMoneyInThisShop(manager.WhereIsTheCheapestItems(listOfItems), 1000);
        manager.BuyItemsInThisShop("1", listOfItems);
      }
      catch (ShopDoesntExistException ex)
      {
        Console.WriteLine($"Caught Shop exception: {ex.Message}");
      }
      catch (ItemDoesntExistException ex)
      {
        Console.WriteLine($"Caught Item exception: {ex.Message}");
      }
      catch (ItemDoesntContainsException ex)
      {
        Console.WriteLine($"Caught Item contains exception: {ex.Message}");
      }
    }
  }
}
