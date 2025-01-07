using PbiServices.Model;
using PbiServices.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TasksServices.Model;
using Task = TasksServices.Model.Task;

namespace PbiServices.Service
{
    public class PbiService : IPbiService
    {
        private readonly IPbiRepository _pbiRepository;
        public PbiService(IPbiRepository PbiRepository)
        {
            _pbiRepository = PbiRepository;
        }

        public IEnumerable<Pbi> GetAll()
        {
            return _pbiRepository.GetAll();
            //IEnumerable<Pbi> result = _pbiRepository.GetAll();
        }

        public Pbi GetById(int id)
        {
            return _pbiRepository.GetById(id);
        }

        public Pbi GetByName(string name)
        {
            return _pbiRepository.GetByName(name);
        }

        public Pbi GetByUserId(int id)
        {
            return _pbiRepository.GetByUserId(id);
        }

        public List<Pbi> GetBySprintId(int id)
        {
            return _pbiRepository.GetBySprintId(id);
        }

        public Pbi GetByStartDate(DateTime date)
        {
            return _pbiRepository.GetByStartDate(date);
        }

        public Pbi GetByEndDate(DateTime date)
        {
            return _pbiRepository.GetByEndDate(date);
        }

        public Pbi GetByPbiType(string type)
        {
            throw new NotImplementedException();
        }

        public Pbi AddPbi(Pbi pbi)
        {
            return _pbiRepository.AddPbi(pbi);
        }

        public PbiRequest Update(int id, PbiRequest pbi)
        {
            return _pbiRepository.Update(id, pbi);
        }
        public List<Task> GetTasksByPbiId(int pbiId)
        {
            return _pbiRepository.GetTasksByPbiId(pbiId);
        }

        public bool Delete(int id)
        {
            return _pbiRepository.Delete(id);
        }

        public bool DeleteByName(string name)
        {
            return _pbiRepository.DeletePbiByName(name);
        }
        public List<Pbi> GetPbiByUserIdAndSprintId(int user_id, int sprint_id)
        {
            return _pbiRepository.GetPbiByUserIdAndSprintId(user_id, sprint_id);
        }
    }
}
