using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  internal interface ITransport
  {
    public string Name { get; }

    public string Type { get; }

    public double Speed { get; }

    public double Run(double distance, out double time );
  }
}
