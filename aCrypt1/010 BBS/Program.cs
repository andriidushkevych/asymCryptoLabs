using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Globalization;

namespace _010_BBS
{
    class Program
    {        
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/010 BBS.txt";
            Random rand = new Random();
            stopWatch.Start();
            Console.WriteLine("START...");
            BigInteger p = BigInteger.Parse("0D5BBB96D30086EC484EBA3D7F9CAEB07", NumberStyles.AllowHexSpecifier);
            BigInteger q = BigInteger.Parse("0425D2B9BFDB25B9CF6C416CC6E37B59C1F", NumberStyles.AllowHexSpecifier);
            BigInteger n = p * q;
            BigInteger[] r = new BigInteger[1000001];
            int[] x = new int[1000000];
            r[0] = rand.Next() + 2;
            for (int i = 0; i < 1000000; ++i)
            {
                r[i + 1] = BigInteger.ModPow(r[i], BigInteger.Parse("2"), n);
                x[i] = (int)(r[i] % 256);
            }


            if (File.Exists(path))
            {
                File.Delete(path);
            }            

            Stream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < 1000000; ++i)
            {
                String str = Convert.ToString(x[i]);
                sw.WriteLine(str);
            }
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Console.ReadKey();
        }
    }
}
