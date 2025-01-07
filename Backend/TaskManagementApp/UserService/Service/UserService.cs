using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UserServices.Repository;
using System.Net;
using System.Net.Http;
using UserServices.Model;
using ProjectServices.Model;
using PbiServices.Model;
using TasksServices.Model;
using AssignmentServices.Model;

namespace UserServices.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        //TODO: o sa functioneze dupa DI
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //TODO: metoda asta returneaza User, nu object. Modificati si la update
        public User AddUser(User user)
        {
            return _userRepository.Add(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetUserByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
        public Assignment GetEnrollment(int user_id,int project_id)
        {
            return _userRepository.GetEnrollment(user_id, project_id);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public bool DeleteUser(int id)
        {
            _userRepository.Delete(id);
            return true;
        }
        public bool DisenrollUser(int user_id,int project_id)
        {
            _userRepository.DisenrollUser(user_id, project_id);
            return true;
        }

        public bool EnrollUser(int user_id, int project_id)
        {
             _userRepository.EnrollUserToProject(user_id, project_id);
            return true;
            
        }

        public List<User> GetUsersByProject(int projectId)
        {
            return _userRepository.GetUsersByProjectId(projectId);
        }

        public UserRequest UpdateUser(int id, UserRequest user)
        {
            return _userRepository.Update(id,user);
        }

        public List<Project> GetProjectByUserId(int id)
        {
            return _userRepository.GetProjectById(id);
        }

        public List<Task> GetTaskByUserId(int id)
        {
            return _userRepository.GetTaskById(id);
        }

        public List<Pbi> GetPbiByUserId(int id)
        {
            return _userRepository.GetPbiById(id);
        }
        public bool DeleteRelatedPbiAndTasks(int sprint_Id)
        {
            return _userRepository.DeleteRelatedPbiAndTasks(sprint_Id);
            
        }
    }
}
