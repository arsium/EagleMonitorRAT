using EagleMonitor.Controls;
using EagleMonitor.Networking;
using EagleMonitor.Utils;
using PacketLib;
using PacketLib.Packet;
using System;
using System.IO;


namespace EagleMonitor.Forms
{
    public partial class DownloadFileForm : FormPattern
    {
        internal string fileToDownload { get; set; }
        internal string baseIp { get; set; }
        public DownloadFileForm(string fileToDownload, string baseIp)
        {
            this.fileToDownload = fileToDownload;
            this.baseIp = baseIp;
            InitializeComponent();
        }

        private void DownloadFileForm_Load(object sender, EventArgs e)
        {

        }

        private void DownloadFileForm_Shown(object sender, EventArgs e)
        {
            ClientHandler.ClientHandlersList[this.baseIp].fileManagerForm.files.Add(Miscellaneous.SplitPath(this.fileToDownload), this);

            DownloadFilePacket dowloadFilePacket = new DownloadFilePacket(fileToDownload)
            {
                plugin = Compressor.QuickLZ.Compress(File.ReadAllBytes(Utils.Miscellaneous.GPath + "\\Plugins\\FileManager.dll"), 1)
            };
            ClientHandler.ClientHandlersList[this.baseIp].SendPacket(dowloadFilePacket);
        }
    }
}
