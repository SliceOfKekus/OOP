using System;
using System.Collections.Generic;
using System.Text;

using MyOwnExceptions;

namespace IniFileParser
{  
  class Section
  {
    string nameOfSection;
    Dictionary<string, string> fields;
   
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
      string[] unacceptbleSymbols = { "#", "[", "]", "?", "<", ">", "!"};

      foreach (string symbol in unacceptbleSymbols)
      { currentLine = currentLine.Replace(symbol, ""); }

      if (sizeOfString != currentLine.Length)
      { throw new BadFormatOfStringException("Format of file is incorrect!"); }
      else
      { return; }
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
      string name;
      string value;
    }
    public void ParsingIniFile(string[] readFile)
    {
      if (readFile[0][0] != '[')
      { throw new BadFormatOfFileException("Format of file is incorrect!"); }

      for(int i = 0; i < readFile.Length; i++)
      {
        ParsingNameOfSection(readFile[i]);
        i++;

        while (readFile[i] != "\n")
        {
          ParsingField(readFile[i]);

          i++;
        }

      }     

    }

  }

}
