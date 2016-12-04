using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Globalization;

namespace _009_BM
{
    class Program
    {
         
        static BigInteger RandomBigInteger(int length)
        {
            Random rand = new Random();
            byte[] arr = new byte[length];
            rand.NextBytes(arr);
            return new BigInteger(arr);

        }
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/009 BM.txt";
            stopWatch.Start();
            Random rand = new Random();
            BigInteger p = BigInteger.Parse("0CEA42B987C44FA642D80AD9F51F10457690DEF10C83D0BC1BCEE12FC3B6093E3", NumberStyles.AllowHexSpecifier);
            BigInteger a = BigInteger.Parse("05B88C41246790891C095E2878880342E88C79974303BD0400B090FE38A688356", NumberStyles.AllowHexSpecifier);
            BigInteger c = (p - 1) / 2;

            byte[] byteArr = new byte[32];
            rand.NextBytes(byteArr);
            BigInteger t = new BigInteger(byteArr);
            int[] arr = new int[1000000];
            t = BigInteger.ModPow(t, 1, p);
            BigInteger t0 = RandomBigInteger(10);
            if (t0 >= (p - 1))
            {
                t0 -= (p - 1);
            }
            for (int i = 0; i < 1000000; i++)
            {
                if(BigInteger.ModPow(a, t0, p - 1) == 0)
                {
                    arr[i] = (int)((256 * t0) / (p - 1));
                }else
                {
                    arr[i] = (int)(((256 * t0) / (p - 1)) - 1);
                }
                t = BigInteger.ModPow(a,t0,p);
                t0 = t;
            }


            if (File.Exists(path))
            {
                File.Delete(path);
            }            

            Stream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < 1000000; ++i)
            {
                sw.WriteLine(arr[i]);
            }
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Console.ReadKey();
        }
    }
}
