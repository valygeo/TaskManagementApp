using Dapper;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using ICommonService = CommonServices.ICommonService;
using System;
using UserServices.Model;
using ProjectServices.Model;
using TasksServices.Model;
using PbiServices.Model;
using AssignmentServices.Model;

namespace UserServices.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly ICommonService _commonService;

        public UserRepository(ICommonService commonService)
        {
            this._commonService = commonService;
        }

        public User Add(User user)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"INSERT INTO [User] (Username, Email, Password, Is_Deleted) VALUES(@Username, @Email, @Password, 0)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, user);
                    return user;
                }
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [User]";
                dbConnection.Open();
                return dbConnection.Query<User>(sQuery);
            }
        }

        public User GetById(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [User] WHERE Id=@Id AND Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<User>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public User GetByUsername(string username)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [User] WHERE Username=@Username AND Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<User>(sQuery, new { Username = username }).FirstOrDefault();
            }
        }

        public User GetByEmail(string email)
        {
            using(IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [User] WHERE Email=@Email AND Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<User> (sQuery, new { Email = email }).FirstOrDefault();
            }
        }

        public List<User> GetUsersByProjectId(int projectId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = $"SELECT DISTINCT u.id, u.username, u.email, u.[password], u.is_deleted FROM[User] u "
                            + "INNER JOIN[Assignment] a ON a.id = u.id "
                            + "INNER JOIN[Project] p ON @Project_Id = a.project_id "
                            + "WHERE p.is_deleted = 0 AND a.is_deleted = 0 AND u.is_deleted = 0 ";

                    dbConnection.Open();
                    List<User> listOfUsers = dbConnection.Query<User>(sQuery, new { Project_Id = projectId }).ToList();
                    return listOfUsers;
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
                    //setting the delete flag to true
                    string sQuery = "UPDATE [User] SET Is_Deleted=1 WHERE Id=@Id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Id = id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EnrollUserToProject(int user_id, int project_id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    //setting the delete flag to true
                    string sQuery = "INSERT INTO [Assignment] VALUES (@User_Id, @Project_Id, 0);";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { User_Id = user_id, Project_Id = project_id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool DisenrollUser(int user_id, int project_id)
        {
            try
            {
                
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {  //UPDATE [Task] SET Is_Deleted = 1 WHERE Id=@User_Id ;
                   //UPDATE [Pbi] SET Is_Deleted = 1 WHERE Id = @User_Id
                   //setting the delete flag to true
                    string sQuery = @"UPDATE [Assignment] SET Is_Deleted=1 WHERE Id=@User_Id AND Project_Id=@Project_Id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { User_Id = user_id, Project_Id = project_id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public Assignment GetEnrollment(int user_id, int project_id)
        {
            
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    //setting the delete flag to true
                    string sQuery = @"SELECT * FROM [Assignment] WHERE id=@user_id AND project_id=@project_id AND is_deleted!=1";
                    dbConnection.Open();
                    return dbConnection.Query<Assignment>(sQuery, new { User_Id=user_id, Project_id=project_id }).FirstOrDefault();
                   
                }
            
            
        }

        public UserRequest Update(int id,UserRequest user)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = "UPDATE [User] SET Username=@Username, Email=@Email, Password=@Password WHERE Id=@id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new {Username = user.Username,Password=user.Password,Email=user.Email,Id=id});
                    return user;

                    //return dbConnection.Query<User>(sQuery, user).FirstOrDefault();
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Project> GetProjectById(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                    {
                        //string sQuery = @"SELECT * FROM [Assignment] WHERE Id = @Id AND Is_Deleted != 1";
                    string sQuery = $"SELECT DISTINCT p.project_id, p.project_name, p.project_description, p.start_date_project, p.end_date_project FROM Project p "
                            + "INNER JOIN [Assignment] a ON a.project_id = p.project_id "
                            + "INNER JOIN [User] ON @User_Id = a.id "
                            + "WHERE p.is_deleted = 0 AND a.is_deleted= 0 ";

                    dbConnection.Open();
                    List<Project> listOfProjects = dbConnection.Query<Project>(sQuery, new { User_Id = userId }).ToList();
                    return listOfProjects;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Task> GetTaskById(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Task] WHERE Id = @User_Id AND Is_Deleted != 1";

                    dbConnection.Open();
                    List<Task> listOfTasks = dbConnection.Query<Task>(sQuery, new { User_Id = userId }).ToList();
                    return listOfTasks;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Pbi> GetPbiById(int userId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Pbi] WHERE Id = @User_Id AND Is_Deleted != 1";
                    dbConnection.Open();
                    List<Pbi> listOfPbi = dbConnection.Query<Pbi>(sQuery, new { User_Id = userId }).ToList();
                    return listOfPbi;
                }
            }
            catch
            {
                return null;
            }
        }
        public bool DeleteRelatedPbiAndTasks(int sprint_Id)
        {
            try
            {

                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {  //UPDATE [Task] SET Is_Deleted = 1 WHERE Id=@User_Id ;
                   //UPDATE [Pbi] SET Is_Deleted = 1 WHERE Id = @User_Id
                   //setting the delete flag to true
                    string sQuery = @"UPDATE [Pbi] SET Is_Deleted=1 WHERE Sprint_Id=@Sprint_Id
                                      UPDATE R SET      R.is_Deleted = 1
                                                        FROM dbo.Task AS R
                                                        INNER JOIN dbo.Pbi AS P 
                                                        ON R.pbi_id = P.pbi_id 
                                                        WHERE P.is_Deleted = 1";
                                                                                  
      
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new {  Sprint_Id = sprint_Id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
