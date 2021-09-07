using System.Net;
using System.Net.Sockets;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Shared
{
    public class Host
    {
        public string host { get; set; }
        public int port { get; set; }
        public bool connected { get; set; }
        public Host(string host, int port)
        {
            this.host = CheckDNS(host);
            this.port = port;
        }
        public static string CheckDNS(string url)
        {
            IPAddress[] l = Dns.GetHostAddresses(url);
            foreach (var IP in l)
            {
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    return IP.ToString();
                }
            }
            return null;
        }
    }
}
