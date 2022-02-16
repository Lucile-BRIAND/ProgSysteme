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
            CryptoSoft(args[0]);
            Console.WriteLine(args[1]);
           
        }
        public static void CryptoSoft(string path)
        {
            Int64 key = 0xA9A9;
            // Init the byte containing the file reading

            int startCryptTime = DateTime.Now.Millisecond;
            List<string> files = new List<string>();
            foreach (string newPath in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                files.Add(newPath);
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

            timeCryptoSoft += DateTime.Now.Millisecond - startCryptTime;
        }
    }
}
