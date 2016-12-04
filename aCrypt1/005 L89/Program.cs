using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace _005_L89
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "D:/учеба/Крипт/aCrypt/lab1/005 L89.txt";
            Random rand = new Random();
            List<int> list89 = new List<int>();
            List<int> generated = new List<int>();

            for (int i = 0; i < 89; i++)
            {
                list89.Add(rand.Next(2));
            }

            for (int i = 0; i < 1000000; i++)
            {
                int countGen = list89.Count;
                int gen = list89[countGen - 38] + list89[countGen - 89];
                gen = gen % 2;
                generated.Add(gen);
                list89.Add(gen);
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
                sw.Write(" ");
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
