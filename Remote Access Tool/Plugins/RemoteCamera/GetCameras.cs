using AForge.Video.DirectShow;
using System.Collections.Generic;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/RemoteCamera/RemoteCamera/Packet.cs
*/

namespace Plugin
{
    internal static class GetCameras
    {
        internal static List<string> GetListCameras()
        {
            List<string> CameraList = new List<string>();
            FilterInfoCollection videoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo videoCaptureDevice in videoCaptureDevices)
            {
                CameraList.Add(videoCaptureDevice.Name);
            }
            return CameraList;
        }
    }
}
