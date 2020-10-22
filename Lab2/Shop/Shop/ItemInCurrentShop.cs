using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
  internal class ItemInCurrentShop : Item
  {
    private long _priceOfItem;
    private long _quantity;

    public ItemInCurrentShop(string itemId, string name, long price, long quantity)
      : base(itemId, name)
    {
      _priceOfItem = price;
      _quantity = quantity;
    }

    public ItemInCurrentShop(Item temp)
      : base(temp)
    {
      _quantity = 0;
    }

    public ItemInCurrentShop(Item temp, long quantity)
      : base(temp)
    {
      _quantity = quantity;
    }

    public void SetCost(long cost)
    {
      _priceOfItem = cost; 
    }

    public long GetCost()
    {
      return _priceOfItem; 
    }

    public void AddItems(long quantity)
    {
      _quantity += quantity;
    }

    public long GetQuantity()
    {
      return _quantity;
    }
  }
}
