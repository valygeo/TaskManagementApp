using AssignmentServices.Model;
using PbiServices.Model;
using ProjectServices.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TasksServices.Model;
using UserServices.Model;

namespace UserServices.Service
{

    //TODO: am modificat eu, interfata trebuie sa fie public
    public interface IUserService
    {
        public User AddUser(User user);
        public IEnumerable<User> GetAllUsers();
        public User GetUserById(int id);
        public Assignment GetEnrollment(int user_id, int project_id);
        public User GetUserByUsername(string username);
        public User GetUserByEmail(string email);
        public bool DeleteUser(int id);
        public bool EnrollUser(int user_id, int project_id);
        public bool DisenrollUser(int user_id, int project_id);
        public UserRequest UpdateUser(int id, UserRequest user);
        public List<User> GetUsersByProject(int projectId);
        public List<Project> GetProjectByUserId(int id); 
        public List<Task> GetTaskByUserId(int id); 
        public List<Pbi> GetPbiByUserId(int id);
        public bool DeleteRelatedPbiAndTasks(int sprint_Id);
    }
}
