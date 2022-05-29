using System.Runtime.InteropServices;

namespace Plugin
{
    internal class Imports
    {
        #region "srclient"
        private const string srclient = "srclient.dll";
        internal const uint ERROR_SUCCESS = 0x0;
        [DllImport(srclient)]
        public static extern uint SRRemoveRestorePoint(int index);
        #endregion
    }
}
