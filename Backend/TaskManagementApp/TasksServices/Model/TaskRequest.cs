using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksServices.Model
{
    public class TaskRequest
    {
        public string Task_Name { get; set; }
        public string Task_Description { get; set; }
        public string Task_Status { get; set; }
        public int Id { get; set; }
        public int Pbi_Id { get; set; }
    }
}
