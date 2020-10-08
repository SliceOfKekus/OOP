using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

using MyOwnExceptions;

namespace IniFileParser
{  
  internal class Section
  {
    public string NameOfSection { get; }
    private Dictionary<string, string> fields;

    public KeyValuePair<string, string> LookingForField(string field)
    {
      foreach (KeyValuePair<string, string> pair in fields)
        if (pair.Key == field)
          return pair;

      throw new NotFoundException("Field with this name wasn't found!\n");
    }

    public Section(string name)
    { 
      NameOfSection = name; 
      fields = new Dictionary<string, string>(); 
    }

    public void AddField(string name, string value)
    {
      fields.Add(name, value); 
    }
  }
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
      {
        currentLine = currentLine.Replace(symbol, ""); 
      }

      if (sizeOfString != currentLine.Length)
      {
        throw new BadFormatOfStringException("Format of file is incorrect!"); 
      }
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
      {
        throw new BadFormatOfStringException("Format of one field in this file is incorrect!");
      }
      else
      {
        if (indexOfComment < 0)
        {
          indexOfComment = currentLine.Length + 1;
        }

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
      {
        throw new BadFormatOfFileException("Format of file is incorrect!"); 
      }

      for (int i = 0; i < readFile.Length; i++)
      {
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
          Convert.ToInt32(pair.Value);
          break;
        case "Double":
          Convert.ToDouble(pair.Value);
          break;
        case "String":
          Convert.ToString(pair.Value);
          break;
      }

      Console.WriteLine($"This is a key-value you are looking for:\n" +
                                    $"Key: {pair.Key}  || Value: {pair.Value}\n");      
    }

  }

}
