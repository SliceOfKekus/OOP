using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

using MyOwnExceptions;

namespace IniFileParser
{    
  internal class IniFile
  {
    private List<Section> sections;

    public IniFile()
    {
      this.sections = new List<Section>(); 
    }

    public Section LookingForSection(string find)
    {
      foreach (Section section in sections)
        if (section.NameOfSection == find)
          return section;

      throw new NotFoundException("Section with this name wasn't found!\n");
    }

    private void AddSection(string name)
    {
      sections.Add(new Section(name)); 
    }

    private void AddFieldToCurrentSection(int index, string name, string value)
    {
      sections[index].AddField(name, value); 
    }

    private void IsThisStringCorrect(string currentLine)
    {
      int sizeOfString = currentLine.Length;
      string[] unacceptbleSymbols = { "#", "[", "]", "?", "<", ">", "!", " ", "{", "}" };

      currentLine = currentLine.Replace(".", ",");
      foreach (string symbol in unacceptbleSymbols)
        currentLine = currentLine.Replace(symbol, ""); 
      

      if (sizeOfString != currentLine.Length)
        throw new BadFormatOfStringException("Format of file is incorrect!"); 
    }

    private void ParsingNameOfSection(string currentLine)
    {
      char[] uselessSymbols = { '[', ']' };
          
      currentLine = currentLine.TrimStart(uselessSymbols);
      currentLine = currentLine.TrimEnd(uselessSymbols);

      IsThisStringCorrect(currentLine);
      AddSection(currentLine);
    }

    private void ParsingField(string currentLine)
    {
      
      int indexOfFieldSeparator = currentLine.IndexOf(" = ");
      int indexOfComment = currentLine.IndexOf(";");

      if (indexOfFieldSeparator < 0)
        throw new BadFormatOfStringException("Format of one field in this file is incorrect!");
      else
      {
        if (indexOfComment < 0)
          indexOfComment = currentLine.Length + 1;

        int lengthOfValuableString = indexOfComment - 1 - (indexOfFieldSeparator + 3);
        string name = currentLine.Substring(0, indexOfFieldSeparator);
        string value = currentLine.Substring(indexOfFieldSeparator + 3, lengthOfValuableString);

        IsThisStringCorrect(name);
        IsThisStringCorrect(value);
        AddFieldToCurrentSection(sections.Count - 1, name, value);
      }
    }

    public void ParsingIniFile(string[] readFile)
    {
      if (readFile[0][0] != '[')
        throw new BadFormatOfFileException("Format of file is incorrect!"); 
      
      for (int i = 0; i < readFile.Length; i++)
      {
        if (readFile[i][0] == ';' || readFile[i] == "")
          continue;
        ParsingNameOfSection(readFile[i]);
        i++;

        while ((i < readFile.Length) && readFile[i] != "")
        {
          ParsingField(readFile[i]);
          i++;
        }

      }
      
    }

    public void FindSpecificKeyValue(string nameOfSection, string nameOfField, string typeToConvertIn)
    {
      Section temp = LookingForSection(nameOfSection);
      KeyValuePair<string, string> pair = temp.LookingForField(nameOfField);

      switch (typeToConvertIn)
      {
        case "Int":
          if (int.TryParse(pair.Value, out int intResult) == false)
            throw new InvalidConvertToIntException("Cannot covert this value to int!");
          else
            Console.WriteLine($"This is a key-value you are looking for:\n" +
                                    $"Key: {pair.Key}  || Value: {Convert.ToInt32(pair.Value)}\n");
            break;
        case "Double":
          if (int.TryParse(pair.Value, out int doubleResult) == false)
            throw new InvalidConvertToDoubleException("Cannot covert this value to int!");
          else
            Console.WriteLine($"This is a key-value you are looking for:\n" +
                                    $"Key: {pair.Key}  || Value: {Convert.ToDouble(pair.Value)}\n");
            break;
        case "String":
          if (int.TryParse(pair.Value, out int stringResult) == false)
            throw new InvalidConvertToStringException("Cannot covert this value to int!");
          else
            Console.WriteLine($"This is a key-value you are looking for:\n" +
                                    $"Key: {pair.Key}  || Value: {Convert.ToString(pair.Value)}\n");
            break;
      }
      
    }

  }

}
