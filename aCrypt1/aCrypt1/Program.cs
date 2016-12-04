using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace aCrypt1
{
    [Serializable]
    class Numbers
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/001 Random.txt";
            stopWatch.Start();
            Random rand = new Random();
            
            int number = 125000;               
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            Stream fs = new FileStream(path, FileMode.OpenOrCreate);

            StreamWriter sw = new StreamWriter(fs);
                
            for (int i = 0; i < number; i++)
            {
                sw.WriteLine(rand.Next(256));                
            }
                
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();
            

           
            Console.ReadKey();            
        }
    }
}
