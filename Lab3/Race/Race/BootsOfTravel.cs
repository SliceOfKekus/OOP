using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  class BootsOfTravel : GroundTransport
  {
    private const string name = "BootsOfTravel";
    override public string Name
    {
      get { return name; }
    }

    private const double speed = 6;
    override public double Speed
    {
      get { return speed; }
    }

    private const double restInterval = 60;
    override public double RestInterval
    {
      get { return restInterval; }
    }

    int count = 1;
    override public double Rest()
    {
      switch (count)
      {
        case 1:
          count++;
          return 10;
        default:
          count++;
          return 5;
      }
    }
  }
}
