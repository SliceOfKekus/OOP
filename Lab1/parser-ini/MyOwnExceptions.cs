using System;

namespace MyOwnExceptions
{
  class BadReadException : Exception
  {
    public BadReadException(string msg)
    : base(msg)
    { }
  }
  class BadFormatOfFileException : Exception
  {
    public BadFormatOfFileException(string msg)
    : base(msg)
    { }
  }

  class InvalidConvertToIntException : Exception
  {
    public InvalidConvertToIntException(string msg)
    : base(msg)
    { }
  }

  class InvalidConvertToDoubleException : Exception
  {
    public InvalidConvertToDoubleException(string msg)
    : base(msg)
    { }
  }

  class InvalidConvertToStringException : Exception
  {
    public InvalidConvertToStringException(string msg)
    : base(msg)
    { }
  }

  class BadFormatOfStringException : Exception
  {
    public BadFormatOfStringException(string msg)
    : base(msg)
    { }
  }
  class NotFoundException : Exception
  {
    public NotFoundException(string msg)
    : base(msg)
    { }
  }
}