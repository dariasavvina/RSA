using System;
using System.IO;

namespace RSAv2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to file: ");
            var path = Console.ReadLine();
            var stream = new StreamReader(path ?? string.Empty);
            var text = stream.ReadToEnd();
            Console.WriteLine("Initial text: ");
            Console.WriteLine(text);
            Console.WriteLine("Start encrypt");
            var rsa = new RSA(new BigInteger(47), new BigInteger(31));
            var (publicKey, privateKey) = rsa.GenerateKeys();
            var crypt = rsa.EncryptText(publicKey, text);
            Console.WriteLine("End crypt");
            Console.WriteLine("Start decrypt");
            var decrypt = rsa.DecryptText(privateKey, crypt);
            Console.WriteLine("End decrypt");
            Console.WriteLine("Decrypt text:");
            Console.WriteLine(decrypt);
        }
    }
}