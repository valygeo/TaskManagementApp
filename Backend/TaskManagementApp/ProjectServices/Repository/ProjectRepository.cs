using ProjectServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServices;
using System.Data;
using Dapper;
using SprintServices.Model;

namespace ProjectServices.Repository
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly ICommonService _commonService;
        public ProjectRepository(ICommonService commonService)
        {
            _commonService = commonService;
        }

        public Project Add(Project project)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"INSERT INTO [Project] (Project_Name, Project_Description, Start_Date_Project, End_Date_Project, Is_Deleted) VALUES (@Project_Name, @Project_Description, @Start_Date_Project, @End_Date_Project, 0)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery,project);
                    return project;
                }
            }
            catch
            {
                return null;
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


        public bool Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Project] SET Is_Deleted = 1 WHERE Project_Id = @Project_id ;
                                      UPDATE [Sprint] SET Is_Deleted = 1 WHERE Project_Id = @Project_id;
                                      UPDATE [Assignment] SET Is_Deleted =1 WHERE Project_Id=@Project_id"; 
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Project_Id = id }); 
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Project> GetAll()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Project] WHERE Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Project>(sQuery);
            }
        }

        public Project GetById(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Project] WHERE Project_Id = @Project_Id AND Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Project>(sQuery, new { Project_Id = id }).FirstOrDefault();
            }
        }
    

        public Project GetByName(string name)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Project] WHERE Project_Name=@Project_Name AND Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Project>(sQuery, new { Project_Name = name }).FirstOrDefault();
            }
        }

        public ProjectRequest Update(int id, ProjectRequest project)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Project] SET Project_name=@Project_Name, Project_description=@Project_Description, Start_Date_Project=@Start_Date_Project, End_Date_Project=@End_Date_Project WHERE project_id=@id AND Is_Deleted != 1";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Project_name = project.Project_Name, Project_description = project.Project_Description, Start_Date_Project = project.Start_Date_Project, End_date_Project = project.End_Date_Project, id = id });
                    return project;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<Sprint> GetSprintsByProjectId(int projectId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Sprint] WHERE Project_Id = @Project_Id AND Is_Deleted != 1";               
                    dbConnection.Open();
                    List<Sprint> listOfSprints = dbConnection.Query<Sprint>(sQuery, new { Project_Id = projectId }).ToList();
                    return listOfSprints;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
