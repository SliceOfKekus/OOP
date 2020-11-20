using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{ 
  internal abstract class AirTransport : ITransport
  {
    private const string type = "Air";
    public string Type
    {
      get { return type; }
    }

    public abstract string Name { get; }

    public abstract double Speed { get; }

    public double Run(double distance, out double time)
    {
      return (time = DistanceReducer(distance) / this.Speed);
    }

    public abstract double DistanceReducer(double distance);
  }
}
