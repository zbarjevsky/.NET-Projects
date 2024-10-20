﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommandsProcessor
{
    public class AssemblyInfoTools
    {
        //https://florent.clairambault.fr/insert-svn-version-and-build-number-in-your-c-assemblyinfo-file/
        //Regex="(\d+)\.(\d+)\.(\d+)\.(\d+)"
        //ReplacementText="$1.$2.$3.$(Revision)"


        public static void UpdateVersionWithTodaysDate(string assemblyInfoFileName)
        {
            if (!File.Exists(assemblyInfoFileName))
            {
                Console.WriteLine("Error - AssemblyInfo file not found: " + assemblyInfoFileName);
                return;
            }

            //"[assembly: AssemblyFileVersion(\"1.03.2019.28356\")]";
            string assemblyInfo = File.ReadAllText(assemblyInfoFileName);

            string findPattern = @"(?<major>\d+)\.(?<minor>\d+)\.(?<build>\d+)\.(?<revision>\d+)";
            Match m = Regex.Match(assemblyInfo, findPattern);
            if (m.Success) //check if update needed
            {
                string year = DateTime.Now.ToString("yyyy");
                string month_day = DateTime.Now.ToString("MMdd");
                if (m.Groups["build"].Value != year || m.Groups["revision"].Value != month_day)
                {
                    string replacement = string.Format(@"$1.$2.{0}", DateTime.Now.ToString("yyyy.MMdd"));
                    string res = Regex.Replace(assemblyInfo, findPattern, replacement);

                    File.WriteAllText(assemblyInfoFileName, res);

                    m = Regex.Match(res, findPattern);
                    Console.WriteLine("Version Updated Successfully to: " + m.ToString());
                }
                else
                {
                    Console.WriteLine("No Version Update Needed: " + m.ToString());
                }
            }
            else
            {
                Console.WriteLine("Error: Version info not found in file: " + assemblyInfoFileName);
            }
        }

        public static void SetVersionAsYYYYMMDD_InAssemblyVersion(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists && !fi.IsReadOnly)
                try
                {
                    Version version = Assembly.GetEntryAssembly().GetName().Version;

                    int major = version.Major;
                    int minor = version.Minor;
                    string year = DateTime.Now.Year.ToString();
                    string mmdd = DateTime.Now.ToString("MMdd");
                    string[] lines = File.ReadAllLines(fileName);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string line = lines[i];
                        if (line.StartsWith("[assembly: AssemblyVersion("))
                        {
                            lines[i] = string.Format("[assembly: AssemblyVersion(\"{0}.{1}.{2}.{3}\")]", major, minor, year, mmdd);
                            Console.WriteLine("AssemblyVersion was updated to: " + lines[i]);
                        }

                        if (line.StartsWith("[assembly: AssemblyFileVersion("))
                        {
                            lines[i] = string.Format("[assembly: AssemblyFileVersion(\"{0}.{1}.{2}.{3}\")]", major, minor, year, mmdd);
                            Console.WriteLine("AssemblyFileVersion was updated to: " + lines[i]);
                        }
                    }

                    File.WriteAllLines(fileName, lines);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
        }

        //Avoid unnessesary version updates
        private static bool IsUpdateNeeded(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            foreach (string line in lines)
            {
                if (line.StartsWith("[assembly: AssemblyVersion("))
                {
                    string[] parts = line.Split('\"');
                    Version ver;
                    if (Version.TryParse(parts[1], out ver))
                    {
                        int mmdd = int.Parse(DateTime.Now.ToString("MMdd"));
                        return (ver.Build != DateTime.Now.Year || ver.Revision != mmdd);
                    }
                }
            }

            Console.WriteLine("Version info was not found in: " + fileName);
            return false; //no version info found - no update needed
        }

        public static void IncrementBuildNumberInAssemblyVersion(string fileName)
        {
            //string file = "Properties/AssemblyInfo.cs";
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists && !fi.IsReadOnly)
            {
                try
                {
                    string fileText = File.ReadAllText(fileName);
                    fileText = Regex.Replace(
                        fileText,
                        @"(?<=\[assembly: AssemblyFileVersion\(""[0-9]*.[0-9]*.)[0-9]*(?=.[0-9]*""\)\])",
                        m => (Convert.ToInt16(m.Value) + 1).ToString()
                        );

                    File.WriteAllText(fileName, fileText);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
            }
        }
    }
}
