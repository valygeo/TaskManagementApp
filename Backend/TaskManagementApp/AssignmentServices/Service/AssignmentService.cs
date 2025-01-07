using AssignmentServices.Model;
using AssignmentServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentServices.Service
{
    public class AssignmentService:IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public IEnumerable<Assignment> GetAll()
        {
            return _assignmentRepository.GetAll();
        }
        public Assignment Add(Assignment assignment)
        {
            return _assignmentRepository.Add(assignment);
        }
        public Assignment GetById(int id)
        {
            return _assignmentRepository.GetById(id);
        }
        public bool Delete(int id)
        {
            return _assignmentRepository.Delete(id);
        }
        public AssignmentRequest Update(int id, AssignmentRequest assignment)
        {
            return _assignmentRepository.Update(id, assignment);
        }
        public Assignment GetByUserId(int id)
        {
            return _assignmentRepository.GetByUserId(id);
        }
        public Assignment GetByProjectId(int id)
        {
            return _assignmentRepository.GetByProjectId(id);
        }
    }
}
