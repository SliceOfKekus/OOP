using System;
using System.Collections.Generic;
using System.Text;

namespace Backups
{
  interface IAlgorithm
  {
    public List<RestorePoint> DoSmthWithRestorePoint(List<RestorePoint> restorePoints);
  }
}
