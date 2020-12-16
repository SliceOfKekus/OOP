using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
  internal class ItemInCurrentShop : Item
  {
    public long PriceOfItem { get; set; }
    public long Quantity { private set; get; }

    public ItemInCurrentShop(Item temp, long quantity, long price)
      : base(temp)
    {
      PriceOfItem = price;
      Quantity = quantity;
    }

    public ItemInCurrentShop(Item temp)
      : base(temp)
    {
      Quantity = 0;
    }

    public void AddItems(long quantity)
    {
      Quantity += quantity;
    }

    public void BuyItems(long quantity)
    {
      Quantity -= quantity;
    }
  }
}
