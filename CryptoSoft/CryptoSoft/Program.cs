using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace CryptoSoft
{
    class Program
    {
        public static byte[] ImageBytes;
        public static int timeCryptoSoft;
        public static int timeExecuteBackup;

        static void Main(string[] args)
        {
            //for (int i = 0; i < args.Length; i++)
            //{
            //    Console.WriteLine(args[i]);
            //}
            //Console.WriteLine("----------------");
            CryptoSoft(args[0], args);

        }
        public static void CryptoSoft(string path, string[] extensionAndPath)
        {
            string[] extension = new string[extensionAndPath.Length - 1];

            //Console.WriteLine(extensionAndPath.Length);

            for (int i = 0; i < extensionAndPath.Length - 1; i++)
            {
                extension[i] = extensionAndPath[i + 1];
                //Console.WriteLine($"extension[{i}] : " + extension[i]);
                //Console.WriteLine($"extensionAndPath[{i + 1}] : " + extensionAndPath[i + 1]);
            }

            Int64 key = 0xA9A9;
            // Init the byte containing the file reading

            int startCryptTime = DateTime.Now.Millisecond;
            List<string> files = new List<string>();
            foreach (string extensionToEncrypt in extension)
            {
                foreach (string newPath in Directory.GetFiles(path, extensionToEncrypt, SearchOption.AllDirectories))
                {
                    files.Add(newPath);
                }
            }
            foreach (string s in files)
            {
                // Read of the initial file
                ImageBytes = File.ReadAllBytes(s);
                // Encryptation or Decyptation

                for (int i = 0; i < ImageBytes.Length; i++)
                {
                    ImageBytes[i] = (byte)(ImageBytes[i] ^ (byte)key);
                }
                // Write of the cryptation and copy
                File.WriteAllBytes(s, ImageBytes);
            }
        }
    }
}