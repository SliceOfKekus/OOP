using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text;

using MyOwnExceptions;
using IniFileParser;
using System.Collections.Generic;

namespace Program
{
  class Program
  {
    static void Main()
    {
      string fileName = "";
      string[] file;
      IniFile iniFile = new IniFile();

      DrawCoolMenu();
      try
      {
        int option = Convert.ToInt32(Console.ReadLine());

        while (true)
        {
          switch (option)
          {
            case 1:
            {
              if (fileName != "")
              {
                Console.WriteLine("Are you sure you wanna rewrite iniFile? Type \"Yes\" if so:");
                string answer = Console.ReadLine();

                if (answer != "Yes")                
                  break;
              }
                Console.WriteLine("Input full path to file:");
                fileName = Console.ReadLine();

              if (!File.Exists(fileName))
              { Console.WriteLine("Incorrect path to file or this file doesn't exist. Who knows..."); }
              else
              {
                file = File.ReadAllLines(fileName);

                iniFile.ParsingIniFile(file);

                Console.Clear();
                goto case 30239366;
              }
              goto case 30239366;
            }
            case 2:
            {
              if (!File.Exists(fileName))
              {
                  Console.WriteLine("Bruh, you should find a file first...");
                  goto case 30239366;
              }
              Console.Clear();
              Console.WriteLine("Type a name of section you are looking for below this text:");
              string nameOfSection = Console.ReadLine();

              Console.WriteLine("Type a name of field you are looking for below this text:");
              string nameOfField = Console.ReadLine();

              Console.WriteLine("Choose from 1 to 3 which type of value you are looking for:");
              Console.WriteLine("1. Integer");
              Console.WriteLine("2. Double");
              Console.WriteLine("3. String");
              
              int typeOfField = Convert.ToInt32(Console.ReadLine());

              switch (typeOfField)
              {
                case 1:
                  {
                    Section temp = iniFile.LookingForSection(nameOfSection);
                    KeyValuePair<string, string> pair = temp.LookingForField(nameOfField);
                    int value = Convert.ToInt32(pair.Value);

                    Console.WriteLine($"This is a key-value you are looking for:\n" +
                                      $"Key: {pair.Key}  || Value: {pair.Value}\n");
                    break;
                  }
                case 2:
                  {
                    Section temp = iniFile.LookingForSection(nameOfSection);
                    KeyValuePair<string, string> pair = temp.LookingForField(nameOfField);
                    double value = Convert.ToDouble(pair.Value);

                    Console.WriteLine($"This is a key-value you are looking for:\n" +
                                      $"Key: {pair.Key}  || Value: {pair.Value}\n");
                    break;
                  }
                case 3:
                  {
                    Section temp = iniFile.LookingForSection(nameOfSection);
                    KeyValuePair<string, string> pair = temp.LookingForField(nameOfField);
                    string value = Convert.ToString(pair.Value);

                    Console.WriteLine($"This is a key-value you are looking for:\n" +
                                      $"Key: {pair.Key}  || Value: {pair.Value}\n");
                    break;
                  }
              }
              goto case 30239366;
            }
            case 3:
            { return; }
            default:
            {
              Console.WriteLine("I guess, you made a mistake. Let's look on menu again.\n");
              goto case 30239366;
            }
            case 30239366:
            {
              DrawCoolMenu();
              option = Convert.ToInt32(Console.ReadLine());
              break; 
            }
          }
        }
      }
      catch (BadReadException ex)
      { Console.WriteLine($"Caught file exception: {ex.Message}"); }     
      catch (BadFormatOfFileException ex)
      { Console.WriteLine($"Caught format of file exception: {ex.Message}"); }
      catch(BadFormatOfStringException ex)
      { Console.WriteLine($"Caught format of string exception: {ex.Message}"); }
      catch (Exception ex)
      { Console.WriteLine($"Caught system exception: {ex.Message}"); }
    }

    static void DrawCoolMenu()
    {
      Console.WriteLine(" _______________________________\n" +
                        "|Henlo. This a menu. Choose one |\n" +
                        "|option  below  this  text  to  |\n" +
                        "|continue.                      |\n" +
                        "|_______________________________|\n" +
                        "|1. Input a  path to  file and  |\n" +
                        "|   parsing   it.               |\n" +
                        "|_______________________________|\n" +
                        "|2. Search  for  key-specific   |\n" +
                        "| value  in  specific  section. |\n" +
                        "|_______________________________|\n" +
                        "|3. Exit program.               |\n" + 
                        "|_______________________________|\n");
    }
  }
}