using System;

/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Eagle_Monitor_Tasks_Configurator.Tasks
{
    [Serializable]
    public class HistoryTask : ITasks
    {
        /*public HistoryTask(TaskType taskType = TaskType.TT_HISTORY) 
        {
            this.taskType = taskType;
        }*/

        public string name { get; set; }
        public string description { get; set; }
        public TaskType taskType { get; set; }
    }
}
