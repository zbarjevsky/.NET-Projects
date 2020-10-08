using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MZ.Framework.Tools
{
    public class DependenciesTool
    {
        //Load FrameworkMkZ.exe from resource - no need on disk
        public static Version LoadFrameworkMkZ_Dependency(byte [] rawAssembly)
        {
            //Assembly assembly = Assembly.ReflectionOnlyLoad(rawAssembly);
            ////Assembly assembly = AppDomain.CurrentDomain.Load(rawAssembly);
            //AssemblyName assemblyName = assembly.GetName();
            //Console.WriteLine("Loaded Assembly: " + assemblyName);

            return UpdateDependency("Framework.MkZ.dll", rawAssembly);

            //return assemblyName.Version;
        }

        //Add dependency as resource
        public static Version UpdateDependency(string dependencyFileName, byte[] rawAssembly)
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dependencyPath = Path.Combine(dir, dependencyFileName);

            Version storedVersion = GetFileVersion(rawAssembly);

            try
            {
                if (File.Exists(dependencyPath))
                {
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(dependencyPath);
                    if (versionInfo.ProductVersion == storedVersion.ToString())
                        return storedVersion; //no update needed

                    return storedVersion; //already extracted
                }

                File.WriteAllBytes(dependencyPath, rawAssembly);

                return storedVersion;
            }
            catch (Exception err)
            {
                ErrorMessage(
                    "Error while Processing Dependency: \n" + dependencyPath + "\nError:\n " + err.Message,
                    "UpdateDependencies");
                return null;
            }
        }

        //Add dependency as resource
        public static bool UpdateDependenciesSfx(string dependencyFolder, string dependencyFileName, byte[] resource)
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dependencyPath = Path.Combine(dir, dependencyFolder, dependencyFileName);

            try
            {
                if (File.Exists(dependencyPath))
                    return true; //already extracted

                string sfx = Path.Combine(dir, "", "Dependencies.sfx.exe");
                if (File.Exists(sfx))
                    File.Delete(sfx);

                File.WriteAllBytes(sfx, resource);
                if (File.Exists(sfx))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(sfx)
                    {
                        WorkingDirectory = Path.GetDirectoryName(sfx)
                    };
                    Process p = Process.Start(startInfo);
                    p.WaitForExit();
                    File.Delete(sfx);
                }

                return true;
            }
            catch (Exception err)
            {
                ErrorMessage(
                    "Error while Processing Dependency: \n" + dependencyPath + "\nError:\n " + err.Message,
                    "UpdateDependenciesSfx"); 
                return false;
            }
        }

        public static Version GetFileVersion(byte [] rawAssembly)
        {
            var domain = AppDomain.CreateDomain("check version");
            Assembly assembly = domain.Load(rawAssembly);
            //Assembly assembly = Assembly.ReflectionOnlyLoad(rawAssembly);
            var verion = assembly.GetName().Version;
            AppDomain.Unload(domain);

            GC.Collect(); // collects all unused memory
            GC.WaitForPendingFinalizers(); // wait until GC has finished its work
            GC.Collect();

            return verion;
        }

        public static void ErrorMessage(string message, string title = "ERROR")
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error,
                MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
        }
    }
}
