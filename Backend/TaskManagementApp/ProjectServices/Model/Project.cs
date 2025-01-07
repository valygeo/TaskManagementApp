using SprintServices.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectServices.Model
{
    public class Project
    {
        [Key]
        public int Project_Id { get; set; }
        public string Project_Name { get; set; }
        public string Project_Description { get; set; }
        public DateTime Start_Date_Project { get; set; }
        public DateTime End_Date_Project { get; set; }
        public int IsDeleted { get; set; }
        public IEnumerable <Sprint> Sprints { get; set; }
    }
}
