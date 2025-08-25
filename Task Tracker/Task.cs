using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public Task(string description)
        {
            Description = description;
            CreatedAt = DateTime.Now;
            UpdateAt = CreatedAt;
        }

        public uint Id { get; set; }
        private string _description;
        public string Description {
            get => _description;
            set {
                if (string.IsNullOrEmpty(value) || value.Trim().Length < 3)
                    throw new ArgumentException("Description must have at least 3 characters.");

                _description = value;
            }
        }
        public Status Status { get; set; } = Status.Todo;
        public DateTime CreatedAt { get; init; }
        public DateTime UpdateAt { get; set; }

        public override string ToString() => Description;
    }
}
