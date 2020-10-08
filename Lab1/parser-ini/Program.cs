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

      try
      {
        int option;

        while (true)
        {
          DrawCoolMenu();
          option = Convert.ToInt32(Console.ReadLine());

          switch (option)
          {
            case 1:
            {
                if (fileName != "")
                {
                    Console.WriteLine("Are you sure you wanna rewrite iniFile? Type \"Yes\" if so:");
                    string answer = Console.ReadLine();

                    if (answer != "Yes")
                    {
                      break;
                    }
                }

                Console.WriteLine("Input full path to file:");
                fileName = Console.ReadLine();

                if (Path.GetExtension(fileName).ToLower() != ".ini")
                {
                    fileName = "";
                    Console.WriteLine("This is not .ini file. Try to use another one.");
                    break;
                }

                if (!File.Exists(fileName))
                {
                    Console.WriteLine("Incorrect path to file or this file doesn't exist. Who knows..."); 
                }
                else
                {
                  file = File.ReadAllLines(fileName);

                  iniFile.ParsingIniFile(file);

                  Console.Clear();
                  break;
                }
                break;
            }
            case 2:
            {
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("Bruh, you should find a file first...");
                    break;
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
                      iniFile.FindSpecificKeyValue(nameOfSection, nameOfField, "Int");
                      break;
                  }
                  case 2:
                  {
                      iniFile.FindSpecificKeyValue(nameOfSection, nameOfField, "Double");
                      break;
                  }
                  case 3:
                  {                      
                      iniFile.FindSpecificKeyValue(nameOfSection, nameOfField, "String");
                      break;
                  }
                }
                break;
            }
            case 3:
            {
              return;
            }
            default:
            {
              Console.WriteLine("I guess, you made a mistake. Let's look on menu again.\n");
                break;
            }

          }

        }

      }
      catch (BadReadException ex)
      {
        Console.WriteLine($"Caught file exception: {ex.Message}");
      }
      catch (BadFormatOfFileException ex)
      {
        Console.WriteLine($"Caught format of file exception: {ex.Message}");
      }
      catch(BadFormatOfStringException ex)
      {
        Console.WriteLine($"Caught format of string exception: {ex.Message}");
      }
      catch (InvalidConvertToIntException ex)
      {
        Console.WriteLine($"Caught cast exception: {ex.Message}");
      }
      catch (InvalidConvertToDoubleException ex)
      {
        Console.WriteLine($"Caught cast exception: {ex.Message}");
      }
      catch (InvalidConvertToStringException ex)
      {
        Console.WriteLine($"Caught cast exception: {ex.Message}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Caught system exception: {ex.Message}");
      }

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