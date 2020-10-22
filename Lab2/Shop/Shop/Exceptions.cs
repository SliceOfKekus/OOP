using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
  class ShopDoesntExistException : Exception
  {
    public ShopDoesntExistException(string msg)
    : base(msg)
    { }
  }

  class ItemDoesntExistException : Exception
  {
    public ItemDoesntExistException(string msg)
    : base(msg)
    { }
  }

  class ItemDoesntContainsException : Exception
  {
    public ItemDoesntContainsException(string msg)
    : base(msg)
    { }
  }
}
