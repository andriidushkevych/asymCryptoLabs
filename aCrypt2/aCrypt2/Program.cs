using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace aCrypt2
{
    class Program
    {
        static void Main(string[] args)
        {            
            BigInteger a = GenerateBlyadskoeChislo();
            Console.WriteLine(a);
            System.Threading.Thread.Sleep(5000);

            BigInteger b = GenerateBlyadskoeChislo();
            Console.WriteLine(b);
            System.Threading.Thread.Sleep(5000);

            BigInteger c = GenerateBlyadskoeChislo();
            Console.WriteLine(c);
            System.Threading.Thread.Sleep(5000);

            BigInteger d = GenerateBlyadskoeChislo();
            Console.WriteLine(d);

            BigInteger pAlice, qAlice, pBob, qBob;

            List<BigInteger> numbers = new List<BigInteger>();
            numbers.Add(a);
            numbers.Add(b);
            numbers.Add(c);
            numbers.Add(d);
            numbers.Sort();

            pAlice = numbers[3];
            qAlice = numbers[2];
            pBob = numbers[1];
            qBob = numbers[0];

            RSA(pAlice, qAlice, pBob, qBob);



            Console.ReadKey();
        }

        static bool MillerRabin(BigInteger p)
        {
            BigInteger k = p - 1;
            BigInteger s = 0;
            while(k % 2 == 0)
            {
                s++;
                k /= 2;
            }
            BigInteger d = k;            

            Random rand = new Random();
            int x = rand.Next();

            BigInteger gcdXP = BigInteger.GreatestCommonDivisor(x, p);

            if (gcdXP != 1)
                return false;

            BigInteger XvDmodP = BigInteger.ModPow(x, d, p);
            if (XvDmodP == 1 || XvDmodP == BigInteger.Subtract(p, 1))
                return true;

            for (BigInteger r = 1; r < s - 1; r++)
            {
                BigInteger temp = (d * BigInteger.ModPow(2, r, p));
                BigInteger Xr = BigInteger.ModPow(x, temp, p);
                if (Xr == 1)
                    return false;
                else if (Xr == BigInteger.Subtract(p, 1))
                    return true;                    
            }

            return false;
        }

        static BigInteger GenerateBlyadskoeChislo()
        {
            bool checker = true;
            BigInteger a = 0;
            while (checker)
            {
                a = GeneratePseudoPrimeNumber();
                bool check = MillerRabin(a);
                if (check)
                    checker = false;
            }
            return a;
        }

        static BigInteger GeneratePseudoPrimeNumber()
        {            
            Random rand = new Random();
            List<int> list89 = new List<int>();
            List<int> generated = new List<int>();

            for (int i = 0; i < 89; i++)
            {
                list89.Add(rand.Next(2));
            }

            for (int i = 0; i < 256; i++)
            {
                int countGen = list89.Count;
                int gen = list89[countGen - 38] + list89[countGen - 89];
                gen = gen % 2;
                generated.Add(gen);
                list89.Add(gen);
            }

            BigInteger resultNumber = 0;
            int count = 0;
            
            for(int i = 255; i >= 0; i--)
            {                
                if (generated[i] == 1) {
                    resultNumber = BigInteger.Add(resultNumber, (BigInteger)Math.Pow(2, count));
                }
                //Console.WriteLine(generated[i] + " " + resultNumber);
                count++;
            }
            return resultNumber;
            
        }

        static void RSA(BigInteger pAlice, BigInteger qAlice, BigInteger pBob, BigInteger qBob)
        {
            List<BigInteger> keysAlice = GenerateKeys(pAlice ,qAlice);
            BigInteger nAlice = keysAlice[0];
            BigInteger eAlice = keysAlice[1];
            BigInteger dAlice = keysAlice[2];

            List<BigInteger> keysBob = GenerateKeys(pBob, qBob);
            BigInteger nBob = keysBob[0];
            BigInteger eBob = keysBob[1];
            BigInteger dBob = keysBob[2];

            BigInteger openText = 777777777;

            List<BigInteger> encryptedMessageAndSignForAlice = KeyTransferProtocol(openText, dBob, nBob, eAlice, nAlice);
            bool CheckKeyTransferProtocolResult = CheckKeyTransferProtocol(dAlice, eBob, eAlice, encryptedMessageAndSignForAlice, nBob, nAlice);
            if(CheckKeyTransferProtocolResult)
                Console.WriteLine("VSE MAT' EGO RABOTAET!!!!!!");
            else
                Console.WriteLine("DER'MO");
            Console.ReadKey();

        }

        static List<BigInteger> KeyTransferProtocol(BigInteger openText, BigInteger dBob, BigInteger nBob, BigInteger eAlice, BigInteger nAlice)
        {
            BigInteger openTextSignedByMe = CreateSign(openText, dBob, nBob);
            BigInteger encryptedSign = Encryption(openTextSignedByMe, eAlice, nAlice);
            BigInteger encryptedText = Encryption(openText, eAlice, nAlice);
            List<BigInteger> forAlice = new List<BigInteger>();
            forAlice.Add(encryptedSign);
            forAlice.Add(encryptedText);
            return forAlice;
        }

        static bool CheckKeyTransferProtocol(BigInteger dAlice, BigInteger eBob, BigInteger eAlice, List<BigInteger> encryptedPairForAlice, BigInteger nBob, BigInteger nAlice)
        {
            BigInteger encryptedSign = encryptedPairForAlice[0];
            BigInteger encryptedText = encryptedPairForAlice[1];

            BigInteger decryptedText = Decryption(encryptedText, dAlice, nAlice);
            BigInteger decryptedSign = Decryption(encryptedSign, dAlice, nAlice);

            bool check = CheckSign(decryptedSign, decryptedText, eBob, nBob);

            return check;
        }

        static BigInteger Encryption(BigInteger message, BigInteger n, BigInteger e)
        {
            return BigInteger.ModPow(message, e, n);
        }

        static BigInteger Decryption(BigInteger EncryptedText, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(EncryptedText, d, n);
        }
        static BigInteger CreateSign(BigInteger OpenText, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(OpenText, d, n);
        }

        static bool CheckSign(BigInteger sign, BigInteger OpenText, BigInteger e, BigInteger n)
        {
            BigInteger a =  BigInteger.ModPow(sign, e, n);
            return (a == OpenText);
        }

        static List<BigInteger> GenerateKeys(BigInteger p, BigInteger q)
        {
            BigInteger n = BigInteger.Multiply(p, q);//Открытый ключ
            BigInteger FIn = BigInteger.Multiply(BigInteger.Subtract(p, 1), BigInteger.Subtract(q, 1));
            BigInteger e = 65537;//Открытый ключ
            //ИСПРАВИТЬ ОБРАТНОЕ ПО МОДУЛЮ
            BigInteger d = BigInteger.ModPow(e, -1, FIn);// закрытый ключ
            BigInteger check = BigInteger.Multiply(d, e) % FIn;
            Console.WriteLine(check);
            List<BigInteger> keys = new List<BigInteger>();
            keys.Add(n);
            keys.Add(e);
            keys.Add(d);
            return keys;
        }
    }
}
