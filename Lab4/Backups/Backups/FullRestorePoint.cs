using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Backups
{
  internal class FullRestorePoint : RestorePoint
  {
    public override DateTime CreationTime { get; }
    public override long Size { get; }   

    public FullRestorePoint(List<FileInfo> listOfFiles)
    {
      CreationTime = DateTime.Now;
      copiedFiles = new List<FileInfo>(listOfFiles);
      Size = 0;

      foreach (var file in listOfFiles)
      {
        Size += file.Length;
      }
    }
  }
}
