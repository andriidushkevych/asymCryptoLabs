using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace _003_LehmerHigh
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/003 LehmerHigh.txt";
            Random rand = new Random();

            long m = (long)Math.Pow(2, 32);
            long a = (long)(Math.Pow(2, 16) + 1);
            int c = 119;

            long[] x = new long[1000001];

            x[0] = rand.Next() % m;

            for (int i = 0; i < x.Length - 1; i++)
            {
                x[i + 1] = (a * x[i] + c) % m;
            }


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            stopWatch.Start();

            Stream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < x.Length - 1; i++)
            {
                byte[] temp = BitConverter.GetBytes(x[i]);
                sw.WriteLine(temp[3]);
            }
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Console.ReadKey();
        }
    }
}
