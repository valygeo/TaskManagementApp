using PbiServices.Model;
using SprintServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintServices.Service
{
    public interface ISprintService
    {
        public IEnumerable<Sprint> GetAll();
        public Sprint Add(Sprint sprint);
        public Sprint GetByName(string name);
        public Sprint GetById(int id);
        public bool Delete(int id);
        public SprintRequest Update(int id ,SprintRequest sprint);
        public List<Sprint> GetByProjectId(int projectId);
        public List<Pbi> GetPbiById(int sprintId);
        
    }
}
