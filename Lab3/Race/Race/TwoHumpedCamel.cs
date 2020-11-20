using System;
using System.Collections.Generic;
using System.Text;

namespace Transport
{
  internal class TwoHumpedCamel : GroundTransport
  {
   

    private const string name = "TwoHumpedCamel";
    override public string Name
    {
      get { return name; }
    }

    private const double speed = 10;
    override public double Speed
    {
      get { return speed; }
    }

    private const double restInterval = 30;
    override public double RestInterval
    {
      get { return restInterval; }
    }

    int count = 1;
    override public double Rest()
    {
      switch(count)
      {
        case 1:
          count++;
          return 5;
        default:
          count++;
          return 8;
      }      
    }
  } 
    
}
