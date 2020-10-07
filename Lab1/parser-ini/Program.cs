using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;

using MyOwnExceptions;
using IniFileParser;

namespace Program
{
  class Program
  {
    static void Main(string[] args)
    {
      string fileName = "C:\\Users\\SliceOfKekus\\Desktop\\test.txt";
      string[] file;
      IniFile iniFile = new IniFile();
      DrawCoolMenu();

      try
      {
        // This text is added only once to the file.
        if (!File.Exists(fileName))
        { throw new BadReadException("Incorrect path to file or this file doesn't exist. Who knows..."); }
        else
        {
          // Open the file to read from.
          file = File.ReadAllLines(fileName);
        }

        iniFile.ParsingIniFile(file);
      }

      catch (BadReadException ex)
      { Console.WriteLine($"Caught file exception: {ex.Message}"); }     
      
      catch (BadFormatOfFileException ex)
      { Console.WriteLine($"Caught format of file exception: {ex.Message}"); }

      catch (Exception ex)
      { Console.WriteLine($"Caught system exception: {ex.Message}"); }
    
    }

    static void DrawCoolMenu()
    {}
  }
}