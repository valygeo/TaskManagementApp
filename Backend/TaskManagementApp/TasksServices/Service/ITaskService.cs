using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksServices.Model;
using Task = TasksServices.Model.Task;

namespace TasksServices.Service
{
    public interface ITaskService
    {
        public IEnumerable<Task> GetAll();
        public IEnumerable<Task> GetAllToDo();
        public IEnumerable<Task> GetAllInProgress();
        public IEnumerable<Task> GetAllDone();
        public Task GetById(int id);
        public Task AddTask(Task task);
        public Task GetByName(string name);
        public bool Delete(int id);
        public TaskRequest Update(int id ,TaskRequest task);
        public List<Task> GetByUserIdAndPbiId(int user_id, int pbi_id);
    }
}
