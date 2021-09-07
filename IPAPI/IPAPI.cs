using Leaf.xNet;
using Newtonsoft.Json;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

/// <summary>
/// Api from : https://ip-api.com/
/// </summary>
namespace IPAPI
{
    public class IPAPI
    {
   
        public static IP GetDetails(string Address, int TimeOut)
        {
            IP Details = new IP();
            using (HttpRequest httpRequest = new HttpRequest())
            {
                httpRequest.IgnoreProtocolErrors = true;
                httpRequest.UserAgent = Http.ChromeUserAgent();
                httpRequest.ConnectTimeout = TimeOut;//30000
                string checkauth = httpRequest.Post("http://ip-api.com/json/" + Address).ToString();
                Details = JsonConvert.DeserializeObject<IP>(JsonConvert.DeserializeObject(checkauth).ToString());            
            }
            return Details;
        }
    }

    public struct IP
    {
        public string query { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string continent { get; set; }
        public string continentCode { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public int zip { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string timezone { get; set; }
        public int offset { get; set; }
        public string currency { get; set; }
        public string isp { get; set; }
        public string org { get; set; }
        public string ass { get; set; }
        public string asname { get; set; }
        public bool mobile  { get; set; }
        public bool proxy { get; set; }
        public bool hosting { get; set; }
    }
}
