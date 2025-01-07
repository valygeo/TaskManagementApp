using CommonServices;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksServices.Model;
using Task = TasksServices.Model.Task;

namespace TasksServices.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ICommonService _commonService;
        public TaskRepository(ICommonService commonService)
        {
            _commonService = commonService;
        }

        public IEnumerable<Task> GetAll()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Task] WHERE Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Task>(sQuery);
            }
        }
        public IEnumerable<Task> GetAllToDo()
        {
            using(IDbConnection dbConnection= _commonService.CreateConnection())
            {
                string sQuery = @"SELECT *FROM [Task] WHERE Task_Status='To do'";
                dbConnection.Open();
                return dbConnection.Query<Task>(sQuery);
            }
        }
        public IEnumerable<Task> GetAllInProgress()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT *FROM [Task] WHERE Task_Status='In progress'";
                dbConnection.Open();
                return dbConnection.Query<Task>(sQuery);
            }
        }
        public IEnumerable<Task> GetAllDone()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT *FROM [Task] WHERE Task_Status='Done'";
                dbConnection.Open();
                return dbConnection.Query<Task>(sQuery);
            }
        }
        public Task GetById(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Task] WHERE Task_Id = @Task_Id AND Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Task>(sQuery, new { Task_Id = id }).FirstOrDefault();
            }
        }
        public Task AddTask(Task task)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"INSERT INTO [Task] (Task_Name, Task_Description, Is_Deleted, Id, Pbi_Id,Task_Status ) VALUES (@Task_Name, @Task_Description, 0, @Id, @Pbi_Id, 'To do')";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, task);
                    return task;
                }
            }
            catch
            {
                return null;
            }
        }
        public Task GetByName(string name)
        {
            using(IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Task] WHERE Task_Name=@Task_Name AND Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Task>(sQuery, new { Task_Name = name }).FirstOrDefault();
            }
        }
        public List<Task> GetByUserIdAndPbiId(int user_id, int pbi_id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Task] WHERE Id=@Id AND Pbi_Id=@Pbi_Id AND Is_deleted!=1";
                    dbConnection.Open();
                    List<Task> listOfTasks = dbConnection.Query<Task>(sQuery, new { Id = user_id, Pbi_Id = pbi_id }).ToList();
                    return listOfTasks;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Task] SET Is_Deleted = 1 WHERE Task_Id = @Task_Id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Task_Id = id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public TaskRequest Update(int id,TaskRequest task)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Task] SET Task_Name = @Task_Name, Task_Description = @Task_Description, Id=@User_Id, Pbi_Id=@Pbi_Id, Task_Status=@Task_Status WHERE Task_Id = @id AND Is_Deleted !=1";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Task_Name = task.Task_Name, Task_Description = task.Task_Description, User_Id = task.Id, Pbi_Id = task.Pbi_Id,Task_Status = task.Task_Status,id = id});
                    return task;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
