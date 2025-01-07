using AssignmentServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentServices.Service
{
    public interface IAssignmentService
    {
        public IEnumerable<Assignment> GetAll();
        public Assignment Add(Assignment assignment);
        public Assignment GetById(int id);
        public bool Delete(int id);
        public AssignmentRequest Update(int id, AssignmentRequest assignment);
        public Assignment GetByUserId(int id);
        public Assignment GetByProjectId(int id);
    }
}
