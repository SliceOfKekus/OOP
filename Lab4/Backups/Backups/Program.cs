using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Backups
{
  class Program
  {
    static void Main(string[] args)
    {
      List<FileInfo> files = new List<FileInfo>();

      FileInfo firstTestFile = new FileInfo("C:\\Users\\SliceOfKekus\\Desktop\\testfile1.txt");
      FileInfo secondTestFile = new FileInfo("C:\\Users\\SliceOfKekus\\Desktop\\testfile2.txt");

      //Console.WriteLine($"{firstTestFile.Length}");

      files.Add(firstTestFile);
      files.Add(secondTestFile);

      Backup firstBackup = new Backup(files, "1");

      firstBackup.CreateFullRestorePoint();
      firstBackup.MaxQuantityOfRestorePoints = 1;
      firstBackup.DeleteRestorePoints(false, true, false);

      Backup secondBackup = new Backup(files, "2");
      
      secondBackup.CreateFullRestorePoint();
      secondBackup.MaxBackupSize = 350; //350 т.к. файлы размерами по 200
      secondBackup.DeleteRestorePoints(false, false, true);

      FileInfo thirdTestFile = new FileInfo("C:\\Users\\SliceOfKekus\\Desktop\\testfile3.txt");
      Backup thirdBackup = new Backup(files, "3");
      
      thirdBackup.CreateFullRestorePoint();
      thirdBackup.AddFileToBackup(thirdTestFile);
      thirdBackup.CreateIncrementaleRestorePoint();
      thirdBackup.GeneralSaveFiles();
      if (thirdBackup.typeOfStore == TypeOfStore.directory)
        Console.WriteLine("Files was saved in special directory separately.");
      else
        Console.WriteLine("Files was archived.");

      Backup fourthBackup = new Backup(files, "4");

      fourthBackup.CreateFullRestorePoint();
      fourthBackup.MaxQuantityOfRestorePoints = 1;
      fourthBackup.MaxBackupSize = 350;
      secondBackup.DeleteRestorePoints(false, true, true);
    }
  }
}
