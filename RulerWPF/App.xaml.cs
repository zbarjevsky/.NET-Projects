using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


using MkZ.WPF.RulerWPF.Tools;

namespace MkZ.WPF.RulerWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (UpdateAssemblyVersion.ProcessCommandLine(e.Args))
            {
                this.Shutdown();
                return;
            }
            base.OnStartup(e);
        }
    }
}
