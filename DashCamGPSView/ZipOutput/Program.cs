using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
                    Console.WriteLine("Error: Not enough parameters - need: DirectoryName ZipFileName");
                    return;
                }

                if (args[0] == "zip")
                {
                    string DirectoryName = Path.GetFullPath(args[1]);
                    string ZipFileName = Path.GetFullPath(args[2]);

                    Console.WriteLine("Compressing: {0} into: {1}", DirectoryName, ZipFileName);

                    if (!Directory.Exists(DirectoryName))
                    {
                        Console.WriteLine("Error: Directry does not exists: " + DirectoryName);
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
                else if (args[0] == "ver")
                {
                    string assemblyInfoFileName = args[1];
                    if (!File.Exists(assemblyInfoFileName))
                    {
                        Console.WriteLine("Error: assemblyInfoFileName does not exists: " + assemblyInfoFileName);
                        return;
                    }

                    string assemblyInfoFileText = File.ReadAllText(assemblyInfoFileName);
                    Match m = Regex.Match(assemblyInfoFileText, @"(?<major>\d{1,3})\.(?<minor>\d{1,3})\.(?<build>\d{1,3})\.(?<revision>\d{1,3})");
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Error compressing: " + err);
            }
        }
    }
}

