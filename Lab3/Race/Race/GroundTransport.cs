using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  internal abstract class GroundTransport : ITransport
  {
    private const string type = "Ground";
    public string Type
    { 
      get { return type; }
    }

    public abstract string Name { get; }

    public abstract double Speed { get; }

    public abstract double RestInterval { get; }

    public double Run(double distance, out double time)
    {
      time = 0;
      double currentDistance = 0;

      while (currentDistance < distance)
      {
        if ((currentDistance + this.Speed * RestInterval) < distance)
        {
          currentDistance += this.Speed * RestInterval;
          time += (RestInterval + this.Rest());
        }
        else
        {
          time += (distance - currentDistance) / this.Speed;
          currentDistance = distance;
        }
      }

      return time;
    }

    public abstract double Rest();
  }
}
