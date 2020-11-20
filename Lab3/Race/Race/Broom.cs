using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  class Broom : AirTransport
  {
    private const string name = "Broom";
    override public string Name
    {
      get { return name; }
    }

    readonly private double speed = 10;
    override public double Speed
    {
      get { return speed; }
    }

    override public double DistanceReducer(double distance)
    {
      if (distance < 100000)
        return distance - (int)distance / 1000 * (distance / 100);
      else
        return 0;
    }
  }
}
