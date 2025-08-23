using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
    [Serializable]
    public class Task
    {
        public Task()
        {
            CreatedAt = DateTime.Now;
            UpdateAt = CreatedAt;
        }

        public uint Id { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Todo;
        public DateTime CreatedAt { get; init; }
        public DateTime UpdateAt { get; set; }

        public override string ToString() => Description;
    }
}
