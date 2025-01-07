using PbiServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksServices.Model;
using Task = TasksServices.Model.Task;

namespace PbiServices.Service
{
    public interface IPbiService
    {
        public IEnumerable<Pbi> GetAll();
        public Pbi GetById(int id);
        public Pbi GetByName(string name);
        public Pbi GetByUserId(int id);
        public List<Pbi> GetBySprintId(int id);
        public Pbi GetByStartDate(DateTime date);
        public Pbi GetByEndDate(DateTime date);
        public Pbi GetByPbiType(string type);
        public Pbi AddPbi(Pbi pbi);
        public bool Delete(int id);
        public bool DeleteByName(string name);
        public PbiRequest Update(int id, PbiRequest pbi);
        public List<Task> GetTasksByPbiId(int pbiId);
        public List<Pbi> GetPbiByUserIdAndSprintId(int user_id, int sprint_id);
    }
}
