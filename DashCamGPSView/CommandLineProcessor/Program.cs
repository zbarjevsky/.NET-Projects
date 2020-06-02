using CommandsProcessor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandsProcessor
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

                if (args[0] == "ZipBinaries")
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
                else if (args[0] == "UpdateVersion")
                {
                    string assemblyInfoFileName = args[1];
                    AssemblyInfoTools.UpdateVersionWithTodaysDate(assemblyInfoFileName);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Error compressing: " + err);
            }
        }
    }
}

