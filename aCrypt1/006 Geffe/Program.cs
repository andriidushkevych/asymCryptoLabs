using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace _006_Geffe
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            string path = "C:/Users/Uzer/Documents/Visual Studio 2015/Projects/aCrypt1/Test1/bin/Debug/006 Geffe.txt";
            Random rand = new Random();
            List<int> l11 = new List<int>();
            List<int> l9 = new List<int>();
            List<int> l10 = new List<int>();
            List<int> X = new List<int>();
            List<int> Y = new List<int>();
            List<int> S = new List<int>();
            List<int> result = new List<int>();

            for (int i = 0; i < 11; i++)
            {
                l11.Add(rand.Next(2));
            }

            for (int i = 0; i < 9; i++)
            {
                l9.Add(rand.Next(2));
            }

            for (int i = 0; i < 10; i++)
            {
                l10.Add(rand.Next(2));
            }

            for (int i = 0; i < 1000000; i++)
            {
                int countL11 = l11.Count;
                int tempX = l11[countL11 - 11] + l11[countL11 - 9];
                tempX = tempX % 2;
                l11.Add(tempX);
                X.Add(tempX);

                int countL9 = l9.Count;
                int tempY = l9[countL9 - 9] + l9[countL9 - 8] + l9[countL9 - 6] + l9[countL9 - 5];
                tempY = tempY % 2;
                l9.Add(tempY);
                Y.Add(tempY);

                int countL10 = l10.Count;
                int tempS = l10[countL10 - 10] + l10[countL10 - 7];
                tempS = tempS % 2;
                l10.Add(tempS);
                S.Add(tempS);


            }

            for (int i = 0; i < 1000000; i++)
            {
                int res = ((S[i] * X[i]) + ((1 + S[i]) % 2) * Y[i]) % 2;
                result.Add(res);
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
                sw.Write(Convert.ToString(result[i]));
            }
            sw.Flush();

            stopWatch.Stop();
            Console.WriteLine("Time: {0}ms, Ticks: {1}", stopWatch.ElapsedMilliseconds, stopWatch.ElapsedTicks);
            stopWatch.Reset();

            Console.ReadKey();
        }
    }
}
