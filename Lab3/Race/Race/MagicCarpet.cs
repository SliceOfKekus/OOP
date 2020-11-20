using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  class MagicCarpet : AirTransport
  {
    private const string name = "MagicCarpet";
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
      if (distance <= 1000)
        return distance;
      else
        if (distance <= 5000)
        return distance - (3 * distance) / 100;
      else
          if (distance <= 10000)
        return distance - distance / 10;
      else
        return distance - distance / 20;
    }
  }
}
