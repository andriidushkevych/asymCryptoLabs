using System;
using System.IO;
using System.Globalization;
using System.Linq;

namespace Test1
{
    class Program
    {
        static void ConvertBitToByteFile(string fn)
        {
            File.Delete("ConvertedTmp.txt");
            using (StreamReader sr = new StreamReader(fn))
            {
                using (StreamWriter sw = new StreamWriter("ConvertedTmp.txt", true))
                {
                    for (int j = 0; j < 1000000 / 8; j++)
                    {
                        char[] buff = new char[8];
                        sr.ReadBlock(buff, 0, 8);
                        string buffs = string.Empty;
                        for (int i = 0; i < buff.Length; i++)
                        {
                            buffs += buff[i];
                        }
                        sw.WriteLine(Convert.ToByte(buffs, 2));
                    }
                }
            }
        }
        static void Test(string input_file_name, string output_file_name)
        {
            byte[] data = File
                .ReadLines(input_file_name)
                .Select(line => Byte.Parse(line, CultureInfo.InvariantCulture))
                .ToArray();
            int[] frequency = new int[256];
            for (int i = 0; i < data.Length; i++)
            {
                frequency[data[i]]++;
            }
            int expected_frequency = data.Length / 256;
            double Quantile = 2.326;
            double Hi_crit = Math.Sqrt(2 * 255) * Quantile + 255;
            double Hi_result = 0;
            for (int i = 0; i < 256; i++)
            {
                Hi_result += Math.Pow(frequency[i] - expected_frequency, 2) / expected_frequency;
            }
            if (Hi_result < Hi_crit)
            {
                Console.WriteLine("H0 is true");
            }
            else
            {
                Console.WriteLine("H0 is false");
            }
            using (StreamWriter sw = new StreamWriter(output_file_name))
            {
                if (Hi_result < Hi_crit)
                    sw.WriteLine("H0 is true");
                else
                    sw.WriteLine("H0 is false");
                sw.WriteLine("Data amount: " + data.Length);
                sw.WriteLine("Hi_Crit: " + Hi_crit);
                sw.WriteLine("Hi_Result: " + Hi_result);
            }
        }

        static void Main(string[] args)
        {
            //ConvertBitToByteFile(".txt");
            Test("010 BBS.txt", "010 BBS Tested.txt");
            Console.Read();

        }
    }
}
