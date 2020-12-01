using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Backups
{
  internal abstract class RestorePoint
  {
    public abstract DateTime CreationTime { get; }
    public abstract long Size { get; }

    public List<FileInfo> copiedFiles;

  }
}
