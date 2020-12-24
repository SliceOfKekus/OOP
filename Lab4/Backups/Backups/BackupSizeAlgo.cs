using System;
using System.Collections.Generic;
using System.Text;

namespace Backups
{
  class BackupSizeAlgo : IAlgorithm
  {
    readonly private Int32 maxBackupSize;

    public BackupSizeAlgo(int maxSize)
    {
      maxBackupSize = maxSize;
    }
    List<RestorePoint> IAlgorithm.DoSmthWithRestorePoint(List<RestorePoint> restorePoints)
    {
      List<RestorePoint> backupSizeLimitFiles = new List<RestorePoint>();
      
        long tempSum = 0;
        foreach (var restorePoint in restorePoints)
          if (restorePoint.Size < maxBackupSize
              && (tempSum + restorePoint.Size) < maxBackupSize)
          {
            backupSizeLimitFiles.Add(restorePoint);
            tempSum += restorePoint.Size;
          }

      return backupSizeLimitFiles;
    }
  }
}
