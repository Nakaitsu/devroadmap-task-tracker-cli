using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Tracker
{
    public enum UserOperations
    {
        Add = 1,
        Update = 2,
        Delete = 3,

        [Description("list")]
        ListAll = 4,

        [Description("list done")]
        ListDone = 5,

        [Description("list todo")]
        ListTodo = 6,

        [Description("list in-progress")]
        ListInProgress = 7,

        [Description("mark-in-progress")]
        MarkInProgress = 8,

        [Description("mark-done")]
        MarkDone = 9
    }
}
