using System;
using System.Collections.Generic;
using System.Text;

namespace Backups
{
  class QuantityLimitAlgo : IAlgorithm
  {
    readonly private Int32 maxQuantity;

    public QuantityLimitAlgo(int maxQuantity)
    {
      this.maxQuantity = maxQuantity;
    }

    public List<RestorePoint> DoSmthWithRestorePoint(List<RestorePoint> restorePoints)
    {
      List<RestorePoint> quantityLimitFiles;

      if (maxQuantity >= restorePoints.Count)
        quantityLimitFiles = new List<RestorePoint>(restorePoints);
      else
      {
        quantityLimitFiles = new List<RestorePoint>();

        if (maxQuantity <= 0)
          throw new Exception("can not store less or equal than zero restore points.");

        int count = restorePoints.Count - maxQuantity;
        for (; count <= restorePoints.Count - 1; count++)
        {
          if (restorePoints[count] is IncrementaleRestorePoint)
            continue;

          quantityLimitFiles.Add(restorePoints[count]);
        }
      }

      return quantityLimitFiles;
    }
  }
}
