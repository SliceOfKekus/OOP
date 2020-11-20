using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

using Transport;
using Race;
using Exceptions;

namespace program
{
  class Program
  {
    static void Main()
    {
      try
      {
        Race.Race ImSpeed = new Race.Race("Air") { RaceDistance = 1000 };
        ImSpeed.AddPlayer(new Broom());
        ImSpeed.AddPlayer(new MagicCarpet());
        ImSpeed.AddPlayer(new Mortar());
        Console.WriteLine($"Winner: {ImSpeed.StartRace()}s.");
      }
      catch (StartRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (CreateRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }

      try
      {
        Race.Race McQueen = new Race.Race("Ground") { RaceDistance = 2000 };
        McQueen.AddPlayer(new TwoHumpedCamel());
        McQueen.AddPlayer(new Centaur());
        McQueen.AddPlayer(new BootsOfTravel());
        Console.WriteLine($"Winner: {McQueen.StartRace()}s.");
      }
      catch (StartRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (CreateRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }

      try
      {
        Race.Race multiple = new Race.Race("AllTypes") { RaceDistance = 120 };
        multiple.AddPlayer(new TwoHumpedCamel());
        multiple.AddPlayer(new Broom());
        multiple.AddPlayer(new BootsOfTravel());
        multiple.AddPlayer(new Mortar());
        Console.WriteLine($"Winner: {multiple.StartRace()}s.");
      }
      catch (StartRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (CreateRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }

      try
      {
        Race.Race last = new Race.Race("Air") { RaceDistance = 120 };
        last.AddPlayer(new TwoHumpedCamel());
        last.AddPlayer(new Broom());
        last.AddPlayer(new BootsOfTravel());
        last.AddPlayer(new Mortar());
        Console.WriteLine($"Winner: {last.StartRace()}s.");
      }
      catch (StartRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }
      catch (CreateRaceException ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
