using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;

namespace aCrypt3
{
    class Program
    {
        static void Main(string[] args)
        {
            Alice();
            Console.ReadKey();
        }

        static void Bob(BigInteger t, BigInteger n)
        {
            Console.WriteLine("VVedite koren' kbadratniy s servaka:");
            BigInteger z = BigInteger.Parse(Console.ReadLine(), NumberStyles.AllowHexSpecifier);
            BigInteger sum = BigInteger.Add(z, t);
            BigInteger result = BigInteger.GreatestCommonDivisor(sum, n);
            Console.WriteLine(result);
        }

        static void Alice()
        {
            BigInteger t = Generator();
            string hexString = "AD8333172E26ACBEEDBC51978DACEB4C5564949E97E97BB966242A1DB8D28D912A1353C2FA7E5E18450D49022A6341DC9E85524BC51BE4347EC8CDFC0805A7134823D9362D7084CA1D393E76AE04D8ECC83212AA20EEA62CA50303FAA25E8160B2874188388AADD78F5D1C1F1FF4C1AF6DA31CE079EB75578F0DBE17A19FCDD6229E7F96A54962D2FBC21A8972685A4D37E6F312603965A593639E8BF3DF4F1540D2893D79E8D4055E96F184E17918D392B700E04993F1DC2436F1B322F449FF830DFFFFF363B04D96C6702536A3E4F8378C075FC2DA736358C0B2DC5A0A8467929A605841B9142D819380CB11A6C66BDE4B2DEE8319C7F542EC29B4BF1811F5";
            BigInteger n = BigInteger.Parse(hexString, NumberStyles.AllowHexSpecifier);            
            BigInteger y = BigInteger.ModPow(t, 2, n);
            Console.WriteLine("tvoy (t^1)modN = {0}", y);
            string ForBobHEX = y.ToString("X");
            Console.WriteLine("tvoy (t^1)modN HEX = {0}", ForBobHEX);
            Bob(t, n);
        }

        static BigInteger Generator()
        {
            Random rand = new Random();
            int a = rand.Next(1000, 9999);
            System.Threading.Thread.Sleep(1000);
            int b = rand.Next(20, 40);
            return BigInteger.Pow(a, b);
        }
    }

}
