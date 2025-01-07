using PbiServices.Model;
using SprintServices.Model;
using SprintServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintServices.Service
{
    public class SprintService:ISprintService
    {
        private readonly ISprintRepository _sprintRepository;
        public SprintService(ISprintRepository sprintRepository)
        {
            this._sprintRepository = sprintRepository;
        }

        public IEnumerable<Sprint> GetAll()
        {
            return _sprintRepository.GetAll();
        }

        public Sprint Add(Sprint sprint)
        {
            return _sprintRepository.Add(sprint);
        }

        public Sprint GetByName(string name)
        {
            return _sprintRepository.GetByName(name);
        }

        public Sprint GetById(int id)
        {
            return _sprintRepository.GetById(id);
        }

        public bool Delete(int id)
        {
           return _sprintRepository.Delete(id);
        }
        public SprintRequest Update(int id,SprintRequest sprint)
        {
            return _sprintRepository.Update(id,sprint);
        }
        public List<Sprint> GetByProjectId(int projectId)
        {
            return _sprintRepository.GetByProjectId(projectId);
        }

        public List<Pbi> GetPbiById(int sprintId)
        {
            return _sprintRepository.GetPbiBySprintId(sprintId);
        }
    }
}
