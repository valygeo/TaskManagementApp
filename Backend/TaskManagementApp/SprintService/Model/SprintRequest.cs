using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintServices.Model
{
    public class SprintRequest
    {
        public string Sprint_Name { get; set; }
        public string Sprint_Description { get; set; }
        public DateTime Start_Date_Sprint { get; set; }
        public DateTime End_Date_Sprint { get; set; }
        public int Project_Id { get; set; }
    }
}
