using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Persistence
{
    public static class Launch
    {
        internal static string ProgramPath = Application.ExecutablePath;
        internal static string ExecName = AppDomain.CurrentDomain.FriendlyName;
    }
}
