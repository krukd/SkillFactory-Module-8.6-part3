using System.Security.Cryptography;

namespace SkillFactory_Module_8._6_part3
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            string dirName = "\\\\Mac\\Home\\Desktop\\Homework";
            long totalSum;
            long totalSumAfterCleaning;

            if (Directory.Exists(dirName))
            {
                DirectoryInfo myDir = new DirectoryInfo(dirName);
                try
                {
                    DirectoryInfo[] diArr = myDir.GetDirectories();
                    FileInfo[] fiArr = myDir.GetFiles();

                    totalSum = TotalSizeMainFolder(myDir, 0);

                    Console.WriteLine($"Исходный размер папки {totalSum} байт");

                    DirectoryDelete(diArr);
                    FilesDelete(fiArr);
                    totalSumAfterCleaning = TotalSizeMainFolder(myDir, 0);
                    Console.WriteLine($"Текущий размер папки {totalSum} байт");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error {ex.Message}");
                }

            }

            static long TotalSizeMainFolder(DirectoryInfo dir, long r_vol)
            {
                long r_sum = r_vol;
                FileInfo[] r_fileArr = dir.GetFiles();
                for (int i = 0; i < r_fileArr.Length; i++)
                {

                    r_sum += r_fileArr[i].Length;
                }

                DirectoryInfo[] dirArr = dir.GetDirectories();
                r_sum += TotalSizeSubfolder(dirArr, r_sum);

                return r_sum;
            }


            static long TotalSizeSubfolder(DirectoryInfo[] workDir, long vol)
            {
                long sum = vol;

                for (int i = 0; i < workDir.Length; i++)
                {

                    FileInfo[] fileArr = workDir[i].GetFiles();
                    for (int j = 0; j < fileArr.Length; j++)
                    {

                        sum += fileArr[j].Length;
                    }
                    
                    sum += TotalSizeSubfolder(workDir[i].GetDirectories(), sum);
                }
                return sum;
            }


            static void FilesDelete(FileInfo[] fl)
            {
                double duration;
                int deletedFiles = 0;
                for (int i = 0; i < fl.Length; i++)
                {
                    duration = (DateTime.Now - fl[i].LastAccessTime).TotalMinutes;
                    if (duration > 30)
                    {

                        fl[i].Delete();
                        deletedFiles++;

                    }

                }
                Console.WriteLine(deletedFiles + " files have been deleted");
            }

            static void DirectoryDelete(DirectoryInfo[] vs)
            {
                double duration;
                int deletedDirectories = 0;

                for (int i = 0; i < vs.Length; i++)
                {
                    duration = (DateTime.Now - vs[i].LastAccessTime).TotalMinutes;
                    if (duration > 30)
                    {

                        vs[i].Delete(true);
                        deletedDirectories++;
                    }


                }
                Console.WriteLine(deletedDirectories + " directories have been deleted");
            }

        }
    }
}