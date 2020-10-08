using System;
using System.Collections.Generic;
using System.Text;

using MyOwnExceptions;

namespace IniFileParser
{
  internal class Section
  {
    public string NameOfSection { get; }
    private readonly Dictionary<string, string> fields;

    public KeyValuePair<string, string> LookingForField(string field)
    {
      if (!fields.TryGetValue(field, out string result))
        throw new NotFoundException("Field with this name wasn't found!\n");
      else
      {
        KeyValuePair<string, string> temp = new KeyValuePair<string, string>(field, result);
        return temp;
      }

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

}