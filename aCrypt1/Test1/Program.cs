using System;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;

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
        
        static void Test1(string input_file_name, string output_file_name)
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

        static void Test2(string input_file_name, string output_file_name)
        {
            byte[] data = File
           .ReadLines(input_file_name)
           .Select(line => Byte.Parse(line, CultureInfo.InvariantCulture))
           .ToArray();
            double Hi_tmp = 0;
            int[] afrec = new int[256];
            int[] bfrec = new int[256];
            for (int i = 0; i < data.Length - 1; i += 2)
            {
                afrec[data[i]]++;
            }
            for (int i = 1; i < data.Length - 2; i += 2)
            {
                bfrec[data[i]]++;
            }
            int[,] Vij = new int[256, 256];
            for (int i = 0; i < data.Length - 1; i += 2)
            {
                Vij[data[i], data[i + 1]]++;
            }

            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    if (afrec[i] != 0 && bfrec[j] != 0)
                    {
                        Hi_tmp += Math.Pow(Vij[i, j], 2) / afrec[i] / bfrec[j];
                    }
                }
            }

            double Hi_result = data.Length / 2 * (Hi_tmp - 1);
            double Quantile = 2.326;
            double Hi_crit = Math.Sqrt(2) * 255 * Quantile + 255 * 255;
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

        static void Test3(string input_file_name, string output_file_name)
        {
            byte[] data = File
           .ReadLines(input_file_name)
           .Select(line => Byte.Parse(line, CultureInfo.InvariantCulture))
           .ToArray();
            int number_of_segments = 20;
            double Hi_tmp = 0;
            int M1 = data.Length / number_of_segments;
            int[] frequences = new int[256];
            for (int i = 0; i < data.Length; i++)
            {
                frequences[data[i]]++;
            }
            int[,] Vij = new int[number_of_segments, 256];
            for (int i = 0; i < data.Length; i++)
            {
                Vij[i / M1, data[i]]++;
            }
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < number_of_segments; j++)
                {
                    Hi_tmp += Math.Pow(Vij[j, i], 2) / frequences[i] / M1;
                }
            }
            double Hi_result = number_of_segments * M1 * (Hi_tmp - 1);
            double Quantile = 2.326;
            double Hi_crit = Math.Sqrt(2 * 255 * (number_of_segments - 1)) * Quantile + 255 * (number_of_segments - 1);
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
            //ConvertBitToByteFile("007 Wolfram.txt"); //for WOLFRAM GEFFE L89 L20
            Test1("008 Librarian.txt", "008 Librarian Tested1.txt");
            Console.WriteLine("press enter to run 2nd test...");
            Console.ReadLine();
            Test2("008 Librarian.txt", "008 Librarian Tested2.txt");
            Console.WriteLine("press enter to run 3rd test...");
            Console.ReadLine();
            Test2("008 Librarian.txt", "008 Librarian Tested3.txt");
            Console.Read();

        }
    }
}
