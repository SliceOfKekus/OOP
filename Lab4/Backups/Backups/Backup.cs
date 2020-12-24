using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Backups
{
  public enum TypeOfStore { Archive, directory }

  public enum HybridType { Maximum, Minimum }

  internal class Backup
  {
    public string Id { get; }
    public DateTime CreationTime { get; }
    public DateTime LifeTime { set; get; }
    public int MaxQuantityOfRestorePoints { set; get; }
    public int MaxBackupSize { set; get; }
    public TypeOfStore typeOfStore;

    private long backupSize;
    private List<RestorePoint> restorePoints;
    readonly private List<FileInfo> files;

    public Backup( List<FileInfo> listOfFiles, string id )
    {
      Id = id;
      CreationTime = DateTime.Now;
      LifeTime = DateTime.Now;
      MaxQuantityOfRestorePoints = Int32.MaxValue;
      MaxBackupSize = Int32.MaxValue;

      restorePoints = new List<RestorePoint>();
      files = new List<FileInfo>();

      foreach (var file in listOfFiles)
        AddFileToBackup(file);

      CreateFullRestorePoint();
      
      CalculateSize();
    }

    public long CalculateSize()
    {
      backupSize = 0;
      foreach (var point in restorePoints)
        backupSize += point.Size;

      return backupSize;
    }

    public void CreateFullRestorePoint()
    {
      RefreshAllInfoFile();
      restorePoints.Add(new FullRestorePoint(files));
    }

    public void CreateIncrementaleRestorePoint()
    {
      RefreshAllInfoFile();
      restorePoints.Add( new IncrementaleRestorePoint(files,
                                                     restorePoints[restorePoints.Count - 1])); 
    }

    private void RefreshAllInfoFile()
    {
      foreach (var file in files)
        file.Refresh();
    }

    public void AddFileToBackup( FileInfo addingFile )
    {
      //RefreshAllFileInfo ? Нужно ли?
      RefreshAllInfoFile();

      for (int i = files.Count - 1; i >= 0; i--)
      {
        if (files[i].Length == addingFile.Length 
            && files[i].FullName == addingFile.FullName)
        {
          return;
        }

        if (files[i].Length != addingFile.Length
            && files[i].FullName == addingFile.FullName)
        {
          files[i].Refresh();
          break;
        }  
      }

      files.Add(addingFile);
    }

    public void GeneralSaveFiles() 
    {
      typeOfStore = TypeOfStore.directory;
    }

    public void SeparateSaveFiles()
    {
      typeOfStore = TypeOfStore.Archive;
    } 

    public void DeleteRestorePoints(List<IAlgorithm> algorithms, HybridType hybridType)
    {
      var listOfResPointLists = new List<List<RestorePoint>>();

      foreach (var algo in algorithms)
      {
        listOfResPointLists.Add(algo.DoSmthWithRestorePoint(restorePoints));
      }

      foreach (var list in listOfResPointLists)
      {
        Int32 index = 0;
        Int32 tempSize = listOfResPointLists.Count;


        if (hybridType == HybridType.Maximum)
        {
          Int32 maxSize = -1;

          for (int count = 0; count < tempSize; count++)
            if (listOfResPointLists[count].Count > maxSize)
              index = count;


          restorePoints.Clear();
          restorePoints = listOfResPointLists[index];
        }
        else
        {
          Int32 minSize = Int32.MaxValue;

          for (int count = 0; count < tempSize; count++)
            if (listOfResPointLists[count].Count < minSize)
              index = count;


          restorePoints.Clear();
          restorePoints = listOfResPointLists[index];
        }     
      }

      ////Что делать, если у нас есть IncrementaleRestorePoints?
      //List<RestorePoint> timeLimitFiles = new List<RestorePoint>();
      //if (timeLimit)
      //{        
      //  foreach (var restorePoint in restorePoints)
      //    if (restorePoint.CreationTime >= LifeTime)
      //      timeLimitFiles.Add(restorePoint);
      //}
      //
      //List<RestorePoint> quantityLimitFiles = new List<RestorePoint>();
      //if (quantityLimit)
      //{
      //  if (MaxQuantityOfRestorePoints >= restorePoints.Count)
      //    quantityLimitFiles = new List<RestorePoint>(restorePoints);
      //  else
      //  {
      //    quantityLimitFiles = new List<RestorePoint>();
      //
      //    if (MaxQuantityOfRestorePoints <= 0)
      //      throw new Exception("can not store less or equal than zero restore points.");
      //
      //    int count = restorePoints.Count - MaxQuantityOfRestorePoints;
      //    for (; count <= restorePoints.Count - 1; count++)
      //      quantityLimitFiles.Add(restorePoints[count]);
      //  }
      //}
      //
      //List<RestorePoint> backupSizeLimitFiles = new List<RestorePoint>();
      //if (backupSizeLimit)
      //{
      //  long tempSum = 0;
      //  foreach (var restorePoint in restorePoints)
      //    if (restorePoint.Size < MaxBackupSize 
      //        && (tempSum + restorePoint.Size) < MaxBackupSize)
      //    {
      //      backupSizeLimitFiles.Add(restorePoint);
      //      tempSum += restorePoint.Size;
      //    }
      //}
      //
      //restorePoints.Clear();   
      //
      //if (quantityLimit
      //    && quantityLimitFiles.Count >= timeLimitFiles.Count
      //    && quantityLimitFiles.Count >= backupSizeLimitFiles.Count)
      //  restorePoints = quantityLimitFiles;
      //
      //if (timeLimit
      //    && timeLimitFiles.Count >= quantityLimitFiles.Count
      //    && timeLimitFiles.Count >= backupSizeLimitFiles.Count)
      //  restorePoints = timeLimitFiles;
      //
      //if (backupSizeLimit
      //    && backupSizeLimitFiles.Count >= quantityLimitFiles.Count
      //    && backupSizeLimitFiles.Count >= timeLimitFiles.Count)
      //  restorePoints = backupSizeLimitFiles;
    } 


    public void DeleteObjectFromBackup( FileInfo deletingFile )
    {
      if (!files.Remove(deletingFile))
        throw new Exception("Can not delete non existing file.");
    }

  }
}
