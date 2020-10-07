using System;
using System.Collections.Generic;
using System.Text;

using MyOwnExceptions;

namespace IniFileParser
{  
  class Section
  {
    string nameOfSection;
    public Dictionary<string, string> fields;

    public string GetName()
    { return nameOfSection; }
    public Section(string name)
    { 
      nameOfSection = name; 
      fields = new Dictionary<string, string>(); 
    }
    public void AddField(string name, string value)
    { fields.Add(name, value); }
  }
  class IniFile
  {
    List<Section> sections;

    public IniFile()
    { sections = new List<Section>(); }
    private void AddSection(string name)
    { sections.Add(new Section(name)); }

    private void AddFieldToCurrentSection(int index, string name, string value)
    { sections[index].AddField(name, value); }

    private void IsThisStringCorrect(string currentLine)
    {
      int sizeOfString = currentLine.Length;
      string[] unacceptbleSymbols = { "#", "[", "]", "?", "<", ">", "!", " ", "{", "}" };

      foreach (string symbol in unacceptbleSymbols)
      { currentLine = currentLine.Replace(symbol, ""); }

      if (sizeOfString != currentLine.Length)
      { throw new BadFormatOfStringException("Format of file is incorrect!"); }
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
      { throw new BadFormatOfFileException("Format of file is incorrect!"); }

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

      foreach (Section currentSection in this.sections)
      {
        Console.WriteLine($"This is section named {currentSection.GetName()}");
        
        foreach(KeyValuePair <string, string> fields in currentSection.fields)
        { Console.WriteLine($"{fields.Key}, {fields.Value}"); }
      }
    }

  }

}
