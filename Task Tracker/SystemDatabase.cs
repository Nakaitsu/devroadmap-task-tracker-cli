using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
    [Serializable]
    public sealed class SystemDatabase
    {
        public static readonly string FileName = "database.json";
        public uint LastInsertedId { get; set; } = 0;
        public ICollection<Task_Tracker.Task> Tasks { get; set; } = new List<Task_Tracker.Task>();
    }
}
