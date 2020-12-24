using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Shop
{
  internal sealed class Shop
  {
    public string ShopId { get; set; }
    private string ShopName { get; set; }
    private string ShopAddress { get; set; }

    readonly private Dictionary<string, ItemInCurrentShop> items;

    public Dictionary<string, ItemInCurrentShop>.ValueCollection Values
    {
      get { return items.Values; }
    }

    public Shop(string id, string name, string address)
    {
      ShopId = id;
      ShopName = name;
      ShopAddress = address;
      items = new Dictionary<string, ItemInCurrentShop>();
    }

    public bool DoesThisItemExistHere(string itemId, out ItemInCurrentShop item)
    {
      item = null;

      foreach (String id in items.Keys)
      {
        if (id == itemId)
        {
          items.TryGetValue(itemId, out item);
          return true;
        }
      }

      return false;
    }

    public long BuyItemInThisShop(ItemInCurrentShop item, long quantity)
    {
      if (item.Quantity - quantity >= 0)
      {
        item.BuyItems(quantity);

        return quantity * item.PriceOfItem;
      }
      else
        throw new ItemDoesntContainsException("This shop doesn't contain enough items you are looking for.");
    }

    public long CostOfItemInCurrentShop(string itemId)
    {
      if (items.TryGetValue(itemId, out ItemInCurrentShop item))
        return item.PriceOfItem;
      else
        throw new ItemDoesntExistException($"Can't find item with id:{itemId} in current shop.");
    }

    public void AddItemToCurrentShop(string itemId, Item item, long quantity, long price)
    {
      if (!items.TryGetValue(itemId, out ItemInCurrentShop currentItem))
        items.Add(itemId, new ItemInCurrentShop(item, quantity, price)); 
      else
        currentItem.AddItems(quantity);
    }
  }
}