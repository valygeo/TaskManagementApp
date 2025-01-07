using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksServices.Model
{
    public class Task
    {
        public int Task_Id { get; set; }
        public string Task_Name { get; set; }
        public string Task_Description { get; set; }
        public int Is_Deleted { get; set; }
        public string Task_Status { get; set; }
        public int Id { get; set; }
        public int Pbi_Id { get; set; }
    }
}
