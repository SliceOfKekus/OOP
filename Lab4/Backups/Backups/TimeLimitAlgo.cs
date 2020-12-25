using System;
using System.Collections.Generic;
using System.Text;

namespace Backups
{
  class TimeLimitAlgo : IAlgorithm
  {
    private DateTime timeLimit;
    public TimeLimitAlgo(DateTime lifeTime)
    {
      timeLimit = lifeTime;
    }

    List<RestorePoint> IAlgorithm.DoSmthWithRestorePoint(List<RestorePoint> restorePoints)
    {
      var finalList = new List<RestorePoint>();

      foreach (var resPoint in restorePoints)
      {
        RestorePoint lastFullRestorePoint = null;
        foreach (var restorePoint in restorePoints)
        {
          if (restorePoint.CreationTime < timeLimit)
            continue;

          if (restorePoint is IncrementaleRestorePoint && lastFullRestorePoint == null)
            continue;

          if (restorePoint is FullRestorePoint)
            lastFullRestorePoint = restorePoint;

          finalList.Add(restorePoint);
        }
      }

      return finalList;
    }
  }
}
