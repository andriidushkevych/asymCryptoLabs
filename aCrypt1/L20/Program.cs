using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace L20
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/004 L20.txt";
            Random rand = new Random();
            List<int> list20 = new List<int>();
            List<int> generated = new List<int>();

            for (int i = 0; i < 20; i++)
            {
                list20.Add(rand.Next(2));
            }

            for (int i = 0; i < 1000000; i++)
            {
                int countGen = list20.Count;
                int gen = list20[countGen - 3] + list20[countGen - 5] + list20[countGen - 9] + list20[countGen - 20];
                gen = gen % 2;
                generated.Add(gen);
                list20.Add(gen);
            }


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            stopWatch.Start();

            Stream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < 1000000; i++)
            {
                sw.Write(Convert.ToString(generated[i]));
            }
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Console.ReadKey();
        }
    }
}
