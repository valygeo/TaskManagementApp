using CommonServices;
using Dapper;
using PbiServices.Model;
using SprintServices.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SprintServices.Repository
{
    public class SprintRepository : ISprintRepository
    {
        private readonly ICommonService _commonService;
        public SprintRepository(ICommonService commonService)
        {
            this._commonService = commonService;
        }
        public IEnumerable<Sprint> GetAll()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Sprint] WHERE Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Sprint>(sQuery);
            }
        }
        public Sprint Add(Sprint sprint)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"INSERT INTO [Sprint] (Sprint_Name, Sprint_Description, Start_Date_Sprint, End_Date_Sprint, Is_Deleted, Project_Id) VALUES (@Sprint_Name, @Sprint_Description, @Start_Date_Sprint, @End_Date_Sprint, 0, @Project_Id)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, sprint);
                    return sprint;
                }
            }
            catch
            {
                return null;
            }
        }
        public Sprint GetByName(string name)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Sprint] WHERE Sprint_Name = @Sprint_Name AND Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Sprint>(sQuery, new { Sprint_Name = name }).FirstOrDefault();
            }
        }
        public Sprint GetById(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Sprint] WHERE Sprint_Id = @Sprint_Id AND Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Sprint>(sQuery, new { Sprint_Id = id }).FirstOrDefault();
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Sprint] SET Is_Deleted = 1 WHERE Sprint_Id = @Sprint_id; 
                                      UPDATE [Pbi] SET Is_Deleted = 1 WHERE Sprint_Id = @Sprint_id; ";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Sprint_Id = id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public SprintRequest Update(int id,SprintRequest sprint)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Sprint] SET Sprint_Name = @Sprint_Name, Sprint_Description = @Sprint_Description, Start_Date_Sprint = @Start_Date_Sprint, End_Date_Sprint = @End_Date_Sprint, Project_Id = @Project_Id WHERE Sprint_Id = @id AND Is_Deleted !=1";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Sprint_Name=sprint.Sprint_Name, Sprint_Description = sprint.Sprint_Description, Start_Date_Sprint = sprint.Start_Date_Sprint, End_Date_Sprint = sprint.End_Date_Sprint, Project_Id=sprint.Project_Id, id=id});
                    return sprint;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<Sprint> GetByProjectId(int projectId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Sprint] WHERE Project_Id = @Project_Id AND Is_Deleted != 1";
                    dbConnection.Open();
                    List <Sprint> listOfSprints = dbConnection.Query<Sprint>(sQuery, new { Project_Id = projectId }).ToList();
                    return listOfSprints;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<Pbi> GetPbiBySprintId(int sprintId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Pbi] WHERE Sprint_Id = @Sprint_Id AND Is_Deleted != 1";
                    dbConnection.Open();
                    List<Pbi> listOfPbi = dbConnection.Query<Pbi>(sQuery, new { Sprint_Id = sprintId }).ToList();
                    return listOfPbi;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
