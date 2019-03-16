using System;
using System.Windows.Forms;
using KMA.ProgrammingInCSharp2019.Lab01.DateApp.Models;

namespace KMA.ProgrammingInCSharp2019.Lab01.DateApp.Tools.Managers
{
    internal static class StationManager
    {
      
        internal static Person CurrentUser { get; set; }

       
        internal static void Initialize()
        {
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            Environment.Exit(1);
        }
    }
}
