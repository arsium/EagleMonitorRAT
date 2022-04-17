using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class ResumeProcess
    {
        private const uint NT_SUCCESS = 0x00000000;
        internal static bool IsResumed(int processId)
        {
            uint ntstatus = 0x1;
            try
            {
                IntPtr procHandle = Process.GetProcessById(processId).Handle;
                ntstatus = Imports.NtResumeProcess(procHandle);

                if (ntstatus == NT_SUCCESS)
                    return true;
            }
            catch { }

            try
            {
                IntPtr procHandle = Process.GetProcessById(processId).Handle;
                ntstatus = Imports.NtResumeProcess(procHandle);

                if (ntstatus == NT_SUCCESS)
                    return true;
            }
            catch { }

            return false;
        }
    }
}
