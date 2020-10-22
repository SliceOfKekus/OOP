using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
  internal class ItemInCurrentShop : Item
  {
    private long _priceOfItem;
    private long _quantity;

    public ItemInCurrentShop(Item temp, long quantity, long price)
      : base(temp)
    {
      _priceOfItem = price;
      _quantity = quantity;
    }

    public ItemInCurrentShop(Item temp)
      : base(temp)
    {
      _quantity = 0;
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

    public void BuyItems(long quantity)
    {
      _quantity -= quantity;
    }

    public long GetQuantity()
    {
      return _quantity;
    }
  }
}
