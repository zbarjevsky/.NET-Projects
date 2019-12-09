using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipOutput
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length < 2)
                {
                    Console.WriteLine("Not enough parameters - need: DirectoryName ZipFileName");
                    return;
                }

                string DirectoryName = Path.GetFullPath(args[0]);
                string ZipFileName = Path.GetFullPath(args[1]);

                Console.WriteLine("Compressing: {0} into: {1}", DirectoryName, ZipFileName);

                if(!Directory.Exists(DirectoryName))
                {
                    Console.WriteLine("Directry does not exists: " + DirectoryName);
                    return;
                }

                if (File.Exists(ZipFileName))
                    File.Delete(ZipFileName);

                string[] pdbFiles = Directory.GetFiles(DirectoryName, "*.pdb");
                foreach (string file in pdbFiles)
                {
                    File.Delete(file);
                }

                ZipFile.CreateFromDirectory(DirectoryName, ZipFileName);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error compressing: " + err);
            }
        }
    }
}

