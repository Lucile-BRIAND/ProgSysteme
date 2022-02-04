using System;
using System.Web;
using System.Text;
using System.IO;
namespace ConsoleApp4
{
    class Program
    {

        static void Main(string[] args)
        {

            string path = "src/pss.pdf";
            string dest = "dst/pss.pdf";

            string text = File.ReadAllText(path);

            Int64 key = 0xA9A9;
            EncryptFile(path,dest,key);
            

        }

        // Crypt and Decrypt Function 
        private static void  EncryptFile(string src, string dest ,Int64 key)
        {
                // Init the byte containing the file reading
                byte[] ImageBytes;
                // Read of the initial file
                ImageBytes = File.ReadAllBytes(src);
                // Encryptation or Decyptation 
                for (int i = 0; i < ImageBytes.Length; i++)
                {
                    ImageBytes[i] = (byte)(ImageBytes[i] ^ (byte)key);
                }
                // Write of the cryptation and copy
                File.WriteAllBytes(src, ImageBytes);
                File.Copy(src, dest, true);
        }


    } 


    
}
