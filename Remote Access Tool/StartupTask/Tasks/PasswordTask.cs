using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Tasks_Configurator.Tasks
{
    [Serializable]
    public class PasswordTask : ITasks
    {
        /*public PasswordTask(TaskType taskType = TaskType.TT_PASSWORD)
        {
            this.taskType = taskType;
        }*/
        
        public string name { get; set; }
        public string description { get; set; }
        public TaskType taskType { get; set; }
    }
}
