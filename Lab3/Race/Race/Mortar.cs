using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  class Mortar : AirTransport
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
      return distance - (6 * distance) / 100;
    }
  }
}
