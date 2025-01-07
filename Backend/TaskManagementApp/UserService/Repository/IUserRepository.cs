using AssignmentServices.Model;
using PbiServices.Model;
using ProjectServices.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TasksServices.Model;
using UserServices.Model;

namespace UserServices.Repository
{
    public interface IUserRepository
    {
        public User Add(User user);
        public IEnumerable<User> GetAll();
        public User GetById(int id);
        public User GetByUsername(string username);
        public User GetByEmail(string email);
        public bool Delete(int id);
        public bool EnrollUserToProject(int user_id, int project_id);
        public UserRequest Update(int id,UserRequest user);
        public List<User> GetUsersByProjectId(int projectId);
        public List<Project> GetProjectById(int userId);
        public List<Task> GetTaskById(int userId);
        public List<Pbi> GetPbiById(int userId);
        public Assignment GetEnrollment(int user_id, int project_id);
        public bool DisenrollUser(int user_id, int project_id);
        public bool DeleteRelatedPbiAndTasks(int sprint_Id);
      
    }
}
