using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LinqSamples
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Windows";
            ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("***");
            ShowLargeFilesWithLing(path);
        }

        private static void ShowLargeFilesWithLing(string path)
        {
            // Lambda Expression and Extension Method syntax
            //var lambda = new DirectoryInfo(path).GetFiles()
            //                .OrderByDescending(f => f.Length)
            //                .Take(5);
            //foreach (var file in lambda)
            //{
            //    Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            //}

            // Comprehension Query Syntax
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;
            foreach (var file in query.Take(5))
            {
                Console.WriteLine($"{file.Name,-20} : {file.Length,10:N0}");
            }
        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());

            for (int i = 0; i < 5; i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Name, -20} : {file.Length, 10:N0}");
            }            
        }
    }

    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
