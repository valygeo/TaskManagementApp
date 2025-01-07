using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksServices.Model;
using TasksServices.Repository;
using Task = TasksServices.Model.Task;

namespace TasksServices.Service
{
    public class TaskService :ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public IEnumerable <Task> GetAll()
        {
            return _taskRepository.GetAll();
        }
        public IEnumerable<Task> GetAllToDo()
        {
            return _taskRepository.GetAllToDo();
        }
        public IEnumerable<Task> GetAllInProgress()
        {
            return _taskRepository.GetAllInProgress();
        }
        public IEnumerable<Task> GetAllDone()
        {
            return _taskRepository.GetAllDone();
        }
        public Task GetById(int id)
        {
            return _taskRepository.GetById(id);
        }
        public Task AddTask(Task task)
        {
            return _taskRepository.AddTask(task);
        }
        public Task GetByName(string name)
        {
            return _taskRepository.GetByName(name);
        }
        public bool Delete(int id)
        {
            return _taskRepository.Delete(id);
        }
        public TaskRequest Update(int id,TaskRequest task)
        {
            return _taskRepository.Update(id, task);
        }
        public List<Task> GetByUserIdAndPbiId(int user_id, int pbi_id)
        {
            return _taskRepository.GetByUserIdAndPbiId(user_id, pbi_id);
        }
    }
}
