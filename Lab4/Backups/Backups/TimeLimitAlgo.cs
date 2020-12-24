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
        foreach (var restorePoint in restorePoints)
          if (restorePoint.CreationTime >= timeLimit)
            finalList.Add(restorePoint);
      }

      return finalList;
    }
  }
}
