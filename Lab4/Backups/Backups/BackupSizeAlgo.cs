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
      RestorePoint lastFull = null;

      foreach (var restorePoint in restorePoints)
      {
        if (restorePoint is IncrementaleRestorePoint
            && lastFull == null)
          continue;

        if (restorePoint.Size < maxBackupSize
            && (tempSum + restorePoint.Size) < maxBackupSize)
        {
          if (restorePoint is FullRestorePoint)
            lastFull = restorePoint;
          

          backupSizeLimitFiles.Add(restorePoint);
          tempSum += restorePoint.Size;
        }
      }
      return backupSizeLimitFiles;
    }
  }
}
