using AssignmentServices.Model;
using CommonServices;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentServices.Repository
{
    public class AssignmentRepository:IAssignmentRepository
    {
        private readonly ICommonService _commonService;
        public AssignmentRepository(ICommonService commonService)
        {
            _commonService = commonService;
        }
        public IEnumerable<Assignment> GetAll()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Assignment] WHERE Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Assignment>(sQuery);
            }
        }
        public Assignment Add(Assignment assignment)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"INSERT INTO [Assignment] (Id,Project_Id,Is_Deleted) VALUES (@User_Id,@Project_Id,0)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, assignment);
                    return assignment;
                }
            }
            catch
            {
                return null;
            }
        }
        public Assignment GetById(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Assignment] WHERE Assignment_Id = @Assignment_Id AND Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Assignment>(sQuery, new { Assignment_Id=id }).FirstOrDefault();
            }
        }
        public bool Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Assignment] SET Is_Deleted = 1 WHERE Assignment_Id = @Assignment_Id" ;          
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Assignment_Id = id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public AssignmentRequest Update(int id, AssignmentRequest assignment)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Assignment] SET Id = @User_Id, Project_Id = @Project_Id WHERE Assignment_Id = @Id AND Is_Deleted !=1";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { User_Id=assignment.Id, Project_Id= assignment.Project_Id, id = id });
                    return assignment;
                }
            }
            catch
            {
                return null;
            }
        }
        public Assignment GetByUserId(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Assignment] WHERE Id = @User_Id AND Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Assignment>(sQuery, new { User_Id = id }).FirstOrDefault();
            }
        }
        public Assignment GetByProjectId(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Assignment] WHERE Project_Id=@Project_Id AND Is_Deleted !=1";
                dbConnection.Open();
                return dbConnection.Query<Assignment>(sQuery, new { Project_Id = id }).FirstOrDefault();
            }
        }



    }
}

