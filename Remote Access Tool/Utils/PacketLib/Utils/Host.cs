using System.Net;
using System.Net.Sockets;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Utils
{
    public class Host
    {
        public string host { get; set; }
        public int port { get; set; }
        public bool connected { get; set; }
        public bool available { get; set; }

        public Host(string host, string port)
        {
            this.host = CheckDNS(host);
            this.port = int.Parse(port);
        }
        private string CheckDNS(string url)
        {
            try
            {
                IPAddress[] l = Dns.GetHostAddresses(url);
                foreach (var IP in l)
                {
                    if (IP.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IP.ToString();
                    }
                }
            }
            catch { }
            return null;
        }
    }
}
