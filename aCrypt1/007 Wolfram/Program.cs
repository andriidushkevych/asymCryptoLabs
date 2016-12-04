using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Collections.Specialized;
using System.Collections;
using System.Numerics;
using System.Globalization;

namespace _007_Wolfram
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/007 Wolfram.txt";
            Random rand = new Random();

            BitArray R = new BitArray(32);            
            byte[] arr = new byte[4];
            rand.NextBytes(arr);
            R = new BitArray(arr);
            int[] x = new int[1000000];
            for (int i = 0; i < 1000000; i++)
            {
                byte[] array = new byte[4];
                R.CopyTo(array, 0);
                uint Ri = BitConverter.ToUInt32(array, 0);

                x[i] = (int)(Ri % 2);
                BitArray Rright = new BitArray(R);
                BitArray Rleft = new BitArray(R);
                for (int j = 1; j < Rright.Count; j++)
                {
                    Rright[j - 1] = R[j];
                }
                Rright[Rright.Count - 1] = R[0];
                for (int j = Rleft.Count - 1; j > 0; j--)
                {
                    Rleft[j] = R[j - 1];
                }
                Rleft[0] = R[Rleft.Count - 1];
                R = Rleft.Xor(R.Or(Rright));
            }          


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            stopWatch.Start();

            Stream fs = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < 1000000; ++i)
            {
                sw.Write(x[i]);
            }
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Console.ReadKey();
        }
    }
}
