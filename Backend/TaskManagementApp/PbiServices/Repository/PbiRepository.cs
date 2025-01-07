using CommonServices;
using Dapper;
using PbiServices.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TasksServices.Model;
using Task = TasksServices.Model.Task;

namespace PbiServices.Repository
{
    public class PbiRepository: IPbiRepository
    {
        private readonly ICommonService _commonService;
        public PbiRepository(ICommonService commonService)
        {
            _commonService = commonService;
        }

        public IEnumerable<Pbi> GetAll()
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Pbi] WHERE is_deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Pbi>(sQuery);
            }
        }
        
        public Pbi GetById(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Pbi] WHERE pbi_id = @Pbi_Id AND Is_Deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Pbi>(sQuery, new { Pbi_Id = id }).FirstOrDefault();
            }
        }

        public Pbi GetByUserId(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Pbi] WHERE id=@User_Id AND is_deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Pbi>(sQuery, new { User_Id = id }).FirstOrDefault();
            }
        }

        public Pbi GetByName(string name)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Pbi] WHERE pbi_name=@Pbi_Name AND is_deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Pbi>(sQuery, new { Pbi_Name = name }).FirstOrDefault();
            }
        }

        public List<Pbi> GetBySprintId(int id)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"select * from [Pbi] p inner join [Sprint] s on p.sprint_id = s.sprint_id WHERE p.is_deleted!=1 and s.is_deleted!=1 and s.sprint_id=@Sprint_Id";
                dbConnection.Open();
                return dbConnection.Query<Pbi>(sQuery, new { Sprint_Id = id }).ToList();
            }
        }

        public Pbi GetByStartDate(DateTime date)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Pbi] WHERE start_date_pbi = @Pbi_Start AND is_deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Pbi>(sQuery, new { Pbi_Start = date }).FirstOrDefault();
            }
        }

        public Pbi GetByEndDate(DateTime date)
        {
            using (IDbConnection dbConnection = _commonService.CreateConnection())
            {
                string sQuery = @"SELECT * FROM [Pbi] WHERE end_date_pbi = @Pbi_End AND is_deleted != 1";
                dbConnection.Open();
                return dbConnection.Query<Pbi>(sQuery, new { Pbi_End = date }).FirstOrDefault();
            }
        }

        public Pbi GetByPbiType(string type)
        {
            throw new NotImplementedException();
        }

        public Pbi AddPbi(Pbi pbi)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"INSERT INTO [Pbi] (pbi_name, pbi_description, start_date_pbi, end_date_pbi, is_deleted, id, sprint_id, pbi_type ) VALUES (@Pbi_Name, @Pbi_Description, @Start_Date_Pbi, @End_Date_Pbi, @Is_Deleted, @Id, @Sprint_Id, @pbi_type)";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, pbi);
                    return pbi;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Pbi] SET is_deleted = 1 WHERE pbi_id = @Pbi_Id;
                                      UPDATE [Task] SET Is_Deleted = 1 WHERE Pbi_Id = @Pbi_Id";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Pbi_Id = id });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DeletePbiByName(string name)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Pbi] SET is_deleted = 1 WHERE pbi_name = @Pbi_Name";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Pbi_Name = name });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public PbiRequest Update(int id, PbiRequest pbi)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"UPDATE [Pbi] SET pbi_name = @Pbi_Name, pbi_description = @Pbi_Description, start_date_pbi = @Start_Date_Pbi, end_date_pbi = @End_Date_Pbi, id=@User_Id, sprint_id=@Sprint_Id, pbi_type=@pbi_type WHERE pbi_id = @id AND is_deleted != 1;
                                       UPDATE [Task] SET id = @User_Id WHERE Pbi_Id = @Id ";
                    dbConnection.Open();
                    dbConnection.Execute(sQuery, new { Pbi_Name = pbi.Pbi_Name, Pbi_Description = pbi.Pbi_Description, Start_Date_Pbi = pbi.Start_Date_Pbi, End_Date_Pbi = pbi.End_Date_Pbi, User_Id = pbi.Id, Sprint_Id = pbi.Sprint_Id, pbi_type = pbi.pbi_type, id = id });
                    return pbi;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<Task> GetTasksByPbiId(int pbiId)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Task] WHERE Pbi_Id = @Pbi_Id AND Is_Deleted != 1";
                    dbConnection.Open();
                    List <Task> listOfTasks = dbConnection.Query<Task>(sQuery, new { Pbi_Id = pbiId }).ToList();
                    return listOfTasks;
                }
            }
            catch
            {
                return null;
            }
        }
        public List<Pbi> GetPbiByUserIdAndSprintId(int user_id, int sprint_id)
        {
            try
            {
                using (IDbConnection dbConnection = _commonService.CreateConnection())
                {
                    string sQuery = @"SELECT * FROM [Pbi] WHERE Id = @Id AND Sprint_Id=@Sprint_Id AND Is_Deleted != 1";
                    dbConnection.Open();
                    List<Pbi> listOfPbi = dbConnection.Query<Pbi>(sQuery, new { Id=user_id, Sprint_Id=sprint_id }).ToList();
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
