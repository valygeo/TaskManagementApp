using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentServices.Model
{
    public class Assignment
    {
        public int Assignment_Id { get; set; }
        public int Id { get; set; }
        public int Project_Id { get; set; }
        public int IsDeleted { get; set; }
    }
}
