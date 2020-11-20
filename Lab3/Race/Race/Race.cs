using System;
using System.Collections.Generic;
using System.Text;

using Transport;
using Exceptions;

namespace Race
{
  internal class Race
  {
    readonly private List<ITransport> transports;
    readonly private string typeOfRace;
    private long raceDistance;

    public long RaceDistance 
    {
      set { raceDistance = value; }
      get { return raceDistance; }
    }

    public Race(string type)
    {
      transports = new List<ITransport>();
      RaceDistance = 0;
      typeOfRace = type;
    }
    
    public Race(long distance, string type)
    {
      transports = new List<ITransport>();
      RaceDistance = distance;
      typeOfRace = type;
    }



    public void AddPlayer(ITransport transport)
    {
      transports.Add(transport);
    }

    private void IsRaceCorrect()
    {
      if (RaceDistance <= 0)
        throw new StartRaceException("Race can't be started. Track has a 0km.");

      if (typeOfRace != "AllTypes")
        foreach (ITransport transport in transports)
          if (transport.Type != typeOfRace)
            throw new CreateRaceException($"{transport.Name} can't participate in this race cuz it must be {typeOfRace}, not {transport.Type}");

      return;
    }

    public double StartRace()
    {
      IsRaceCorrect();
      double bestTime = Double.MaxValue;
      
      foreach (ITransport transport in transports)
        if (transport.Run(RaceDistance, out double time) < bestTime)
          bestTime = time;

      return bestTime;
    }

  }
}
