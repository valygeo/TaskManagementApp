using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksServices.Model;
using Task = TasksServices.Model.Task;

namespace PbiServices.Model
{
    public class Pbi
    {
        public int Pbi_Id { get; set; }
        public string Pbi_Name { get; set; }
        public string Pbi_Description { get; set; }
        public DateTime Start_Date_Pbi { get; set; }
        public DateTime End_Date_Pbi { get; set; }
        public int Is_Deleted { get; set; }
        public PbiType pbi_type { get; set; }
        public int Id { get; set; }
        public int Sprint_Id { get; set; }
        public IEnumerable <Task> Tasks { get; set; }

    }
    public enum PbiType
    {
        Bug,
        Feature
        
    }
}
