using System;
using System.Collections.Generic;
using System.Text;

namespace Exceptions
{
  internal class StartRaceException : Exception
  {
    public StartRaceException(string msg)
    : base(msg)
    { }
  }
}

