using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
  class CreateRaceException : Exception
  {
    public CreateRaceException(string msg)
    : base(msg)
    { }
  }
}
