using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace _008_Librarian
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/008 Librarian.txt";
            string pathToText = "D:/учеба/Крипт/aCrypt/lab1/textForLibrarian.txt";
            Random rand = new Random();
            List<byte> result = new List<byte>();

            StreamReader sr = new StreamReader(pathToText);
            string text = sr.ReadToEnd();            
            foreach (char symb in text)
            {                                
                    result.Add(Convert.ToByte(symb%256));                
            }

            
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            stopWatch.Start();

            Stream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);

            foreach (var item in result)
            {
                sw.WriteLine(item);
            }
            
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Console.ReadKey();

        }
    }
}
