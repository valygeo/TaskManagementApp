using PbiServices.Model;
using SprintServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintServices.Repository
{
    public interface ISprintRepository
    {
        public Sprint Add(Sprint sprint);
        public SprintRequest Update(int id,SprintRequest sprint);
        public IEnumerable<Sprint> GetAll();
        public Sprint GetById(int id);
        public Sprint GetByName(string name);
        public bool Delete(int id);
        public List<Sprint> GetByProjectId(int projectId);

        public List<Pbi> GetPbiBySprintId(int sprintId);
    }
}
