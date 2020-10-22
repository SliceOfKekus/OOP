using System;
using System.Collections.Generic;
using System.Text;

namespace Shop
{
  internal class Item
  {
    protected string _itemId;
    protected string _name;

    public Item(string id, string name)
    {
      _itemId = id;
      _name = name;
    }
    public Item(Item temp)
    {
      _itemId = temp._itemId;
      _name = temp._name;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetId()
    {
      return _itemId;
    }
  }
}
