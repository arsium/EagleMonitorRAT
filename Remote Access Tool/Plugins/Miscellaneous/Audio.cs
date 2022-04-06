using System.Windows.Forms;
using static Plugin.Imports;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class Audio
    {
        private static Form formNeeded;
        static Audio()
        {
            formNeeded = new Form();
        }

        internal static void IncreaseVolume()
        {
            SendMessage(formNeeded.Handle, WM_APPCOMMAND, formNeeded.Handle, APPCOMMAND_VOLUME_UP);
        }

        internal static void DecreaseVolume()
        {
            SendMessage(formNeeded.Handle, WM_APPCOMMAND, formNeeded.Handle, APPCOMMAND_VOLUME_DOWN);
        }

    }
}
