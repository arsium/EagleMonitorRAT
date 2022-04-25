using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace EagleMonitor.Config
{
    internal class Settings
    {
        public List<int> ports { get; set; }
        public string key { get; set; }
        public bool notificationSound { get; set; }
        public string flagsPackName { get; set; }
    }
}
