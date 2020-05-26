using System;

namespace SolitaireCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter a message to encrypt: ");
            string message = Console.ReadLine();

            Cipher cipher = new Cipher();

            string encrypted = cipher.Encrypt(message);

            Console.WriteLine(String.Format("Encrypted Message: {0}", encrypted));

            string decrypted = cipher.Decrypt(encrypted);

            Console.WriteLine(String.Format("Decrypted Message: {0}", decrypted));

            Console.WriteLine();
            Console.WriteLine("...Press any key to exit...");
            Console.Read();
        }
    }
}
