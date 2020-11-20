using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  internal class Centaur : GroundTransport
  {
    private const string name = "Centaur";
    override public string Name
    {
      get { return name; }
    }

    private const double speed = 15;
    override public double Speed
    {
      get { return speed; }
    }

    private const double restInterval = 8;
    override public double RestInterval
    {
      get { return restInterval; }
    }

    override public double Rest()
    {
      return 2;
    }
  }
}
