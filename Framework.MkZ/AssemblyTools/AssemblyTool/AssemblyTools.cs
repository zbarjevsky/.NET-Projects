﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace MkZ.Framework.Tools
{
    public class AssemblyTools
    {
        /*
        // Pre-build event command line
        if $(ConfigurationName) == Debug (
        if exist "$(TargetPath)" (
         "$(TargetPath)" -FILE"$(ProjectDir)Properties\AssemblyInfo.cs"
        )
        )
        */

        /// <summary>
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool UpdateAssemblyInfoVersion(string[] args)
        {
            string fileName = FindArgumentStartsWithRemovePrefix(args, "-FILE");
            if (string.IsNullOrWhiteSpace(fileName))
                return false; //no expected command line args

            //Utils.ErrorMessage(fileName);

            //Version version = Assembly.GetEntryAssembly().GetName().Version;
            //Console.WriteLine("AssemblyVersion: " + version.ToString());

            FileInfo fi = new FileInfo(fileName);
            if (!fi.Exists)
            {
                Console.WriteLine("AssemblyInfo.cs: file does not exists: " + fileName);
                return true; //processing requested but not needed
            }

            if (!IsVersionUpdateNeeded(fileName))
            {
                //Console.WriteLine("AssemblyVersion is good :) " + fileName);
                return true; //processing requested but not needed
            }

            SetVersionAsYYYYMMDD_InAssemblyVersion(fileName);

            return true; //processesed - update version info
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
                    Utils.ErrorMessage(err.Message, "SetVersionAsYYYYMMDD_InAssemblyVersion");
                }
        }

        //Avoid unnessesary version updates
        private static bool IsVersionUpdateNeeded(string fileName)
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
                        Console.Write("Current version: " + parts[1]);
                        int mmdd = int.Parse(DateTime.Now.ToString("MMdd"));
                        if (ver.Build != DateTime.Now.Year || ver.Revision != mmdd)
                        {
                            Console.WriteLine(" - Update Needed");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine(" - Version is Good!");
                            return false;
                        }
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
                    Utils.ErrorMessage(err.Message, "IncrementBuildNumberInAssemblyVersion");
                }
            }
        }

        public static string FindArgumentStartsWithRemovePrefix(string[] args, string prefix)
        {
            if (args == null || args.Length == 0)
                return "";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower().StartsWith(prefix.ToLower()))
                    return args[i].Substring(prefix.Length);
            }

            return "";
        }
    }
}
