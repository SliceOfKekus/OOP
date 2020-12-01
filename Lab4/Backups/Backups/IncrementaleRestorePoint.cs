using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Backups
{
  internal class IncrementaleRestorePoint : RestorePoint
  {
    //private readonly List<FileRestoreCopyInfo> copiedFiles;
    public override DateTime CreationTime { get; }
    public override long Size { get; }

    public IncrementaleRestorePoint(List<FileInfo> listOfFiles, RestorePoint previousPoint)
    {
      copiedFiles = new List<FileInfo>();

      foreach (var file in previousPoint.copiedFiles)
      {
        foreach (var sameFile in listOfFiles)
        {
          if (sameFile.FullName == file.FullName
              && sameFile.Length != file.Length)
          {            
            copiedFiles.Add(file);
            break;
          }
        }

      }
    }
  }
}
