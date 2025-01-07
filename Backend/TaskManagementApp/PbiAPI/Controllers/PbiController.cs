using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PbiAPI.Model;
using PbiServices.Model;
using PbiServices.Service;
using SprintServices.Service;
using UserServices.Service;
using TasksServices.Model;
using Task = TasksServices.Model.Task;
using ProjectServices.Model;
using System.Linq;

namespace PbiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PbiController : ControllerBase
    {
        private readonly IPbiService _pbiService;
        private readonly IUserService _userService;
        private readonly ISprintService _sprintService;
        public PbiController(IPbiService pbiService, IUserService userService, ISprintService sprintService)
        {
            _pbiService = pbiService;
            _userService = userService;
            _sprintService = sprintService;
        }

        /// <summary>
        /// Adds new Pbi
        /// </summary>
        /// <response code="200">Pbi</response>   
        [AllowAnonymous]
        [HttpPost]
        [Route("addPbi")]
        public ActionResult AddPbi([FromBody] PbiRequestModel pbi) => AddDbPbi(pbi);

        /// <summary>
        /// Get All Pbi
        /// </summary>
        /// <remarks>
        /// Get a list of all Pbi
        /// </remarks>
        /// <returns>List of all Pbi</returns>
        /// <response code="200">Pbi</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllPbi")]
        public IEnumerable<Pbi> GetAll() => GetAllPbi();

        /// <summary>
        /// Get pbi by id
        /// </summary>
        /// <remarks>
        /// Get pbi by id
        /// </remarks>
        /// <param name="id">pbi id</param>
        /// <returns>Pbi</returns>
        /// <response code="200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPbiById/{id}")]
        public ActionResult<Pbi> GetById([FromRoute(Name = "id")] int id) => GetPbiById(id);

        /// <summary>
        /// Get pbi by user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>Pbi</returns>
        /// <response code="200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPbiByUserId/{id}")]
        public ActionResult<Pbi> GetByUserId([FromRoute(Name = "id")] int id) => GetPbiByUserId(id);

        /// <summary>
        /// Get pbi by sprint id
        /// </summary>
        /// <param name="id">sprint id</param>
        /// <returns>Pbi</returns>
        /// <response code="200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPbiBySprintId/{id}")]
        public ActionResult<Pbi> GetBySprintId([FromRoute(Name = "id")] int id) => GetPbiBySprintId(id);

        /// <summary>
        /// Get pbi by userid and sprint id
        /// </summary>
        /// <param name="id">user  id</param>
        /// /// <param name="sprint_id">sprint id </param>
        /// <returns>Pbi</returns>
        /// <response code="200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPbiByUserIdAndSprintId/{userId}/{sprintId}")]
        public ActionResult<Pbi> GetByUserIdSprintId([FromRoute(Name = "userId")] int user_Id, [FromRoute(Name = "sprintId")] int sprint_Id) => GetDbPbiBySprintIdAndUserId(user_Id, sprint_Id);

        /// <summary>
        /// Get pbi by name
        /// </summary>
        /// <remarks>
        /// Get pbi by name
        /// </remarks>
        /// <param name="name">pbi name</param>
        /// <returns>Pbi</returns>
        /// <response code ="200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPbiByName/{name}")]
        public ActionResult<Pbi> GetByName([FromRoute(Name = "name")] string name) => GetPbiByName(name);

        /// <summary>
        /// Get a list of tasks by pbiId
        /// </summary>
        /// <remarks>
        ///  Get a list of tasks by pbiId
        /// </remarks>
        /// <param name="id">pbi id</param>
        /// <returns>Task</returns>
        /// <response code="200">Task</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetPbiByPbiId/{pbiId}")]
        public ActionResult<Task> GetTasks([FromRoute(Name = "pbiId")] int pbiId) => GetDbTasksByPbiId(pbiId);

        /// <summary>
        /// Update pbi
        /// </summary>
        /// <remarks>Update a pbi from database</remarks>
        /// <param name="id">pbi id</param>
        /// <param name="pbi"></param>
        /// <returns>Pbi</returns>
        /// <response code = "200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("UpdatePbi/{id}")]
        public ActionResult<PbiRequest> UpdatePbi([FromRoute(Name = "id")] int id, [FromBody] PbiRequest pbi) => UpdateDbPbi(id, pbi);

        /// <summary>
        /// Delete pbi
        /// </summary>
        /// <remarks>Delete a pbi from database</remarks>
        /// <param name="id">pbi id</param>
        /// <response code = "200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeletePbi/{id}")]
        public dynamic DeletePbi([FromRoute(Name = "id")] int id) => DeletePbiById(id);

        /// <summary>
        /// Delete pbi by name
        /// </summary>
        /// <remarks>Delete a pbi from database by name</remarks>
        /// <param name="id">pbi id</param>
        /// <response code = "200">Pbi</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeletePbiByName/{name}")]
        public dynamic DeletePbiByName([FromRoute(Name = "name")] string name) => DeleteDbPbiByName(name);

        private IEnumerable<Pbi> GetAllPbi()
        {
            return _pbiService.GetAll();
        }

        private ActionResult GetPbiById(int id)
        {
            //TODO: mesaj daca id-ul nu exista in baza de date - returnati 400 bad request cu mesaj
            var foundPbi = _pbiService.GetById(id);
            if (foundPbi != null)
            {
                var foundTasks = _pbiService.GetTasksByPbiId(foundPbi.Pbi_Id);
                foundPbi.Tasks = foundTasks;
                return Ok(foundPbi);
            }
            return BadRequest("Pbi was not found!");
        }

        private ActionResult GetPbiByName(string name)
        {
            var foundPbi = _pbiService.GetByName(name);
            if (foundPbi != null)
            {
                var foundTasks = _pbiService.GetTasksByPbiId(foundPbi.Pbi_Id);
                foundPbi.Tasks = foundTasks;
                return Ok(foundPbi);
            }
            return BadRequest("Pbi was not found!");
        }

        private ActionResult GetPbiByUserId(int id)
        {
            var foundPbi = _pbiService.GetByUserId(id);
            if (foundPbi != null)
            {
                var foundTasks = _pbiService.GetTasksByPbiId(foundPbi.Pbi_Id);
                foundPbi.Tasks = foundTasks;
                return Ok(foundPbi);
            }
            return BadRequest("Pbi was not found!");

        }

        private ActionResult GetPbiBySprintId(int id)
        {
            //var foundSprint = _sprintService.GetByProjectId(projectId);
            //if (foundSprint.Count >= 1)
            //{
            //    return Ok(foundSprint);
            //}
            //return BadRequest("Sprint was not found!");
            List<Pbi> foundPbi = _pbiService.GetBySprintId(id);
            if (foundPbi.Count >= 1)
            {
                foreach (Pbi pbi in foundPbi)
                {
                    var foundTasks = _pbiService.GetTasksByPbiId(pbi.Pbi_Id);
                    pbi.Tasks = foundTasks;
                }

                return Ok(foundPbi);
            }
            return BadRequest("Pbi was not found!");

        }
        private ActionResult GetDbPbiBySprintIdAndUserId(int user_id, int sprint_id)
        {
            List<Pbi> foundPbi = _pbiService.GetPbiByUserIdAndSprintId(user_id, sprint_id);
            if (foundPbi.Count >= 1)
            {
                foreach (Pbi pbi in foundPbi)
                {
                    var foundTasks = _pbiService.GetTasksByPbiId(pbi.Pbi_Id);
                    pbi.Tasks = foundTasks;
                }

                return Ok(foundPbi);
            }
            return BadRequest("Pbi was not found!");

        }

        private ActionResult AddDbPbi(PbiRequestModel pbi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (_pbiService.GetByName(pbi.Pbi_Name) != null)
                {
                    return BadRequest("Name already exists!");
                }

                var pbiToAdd = new PbiServices.Model.Pbi
                {
                    Pbi_Name = pbi.Pbi_Name,
                    Pbi_Description = pbi.Pbi_Description,
                    Start_Date_Pbi = pbi.Start_Date_Pbi,
                    End_Date_Pbi = pbi.End_Date_Pbi,
                    Is_Deleted = 0,
                    Id = pbi.Id,
                    Sprint_Id = pbi.Sprint_Id,
                    pbi_type = pbi.pbi_type,
                };
                if (_userService.GetUserById(pbiToAdd.Id) != null && _sprintService.GetById(pbiToAdd.Sprint_Id) != null)
                {


                    if (pbiToAdd.Start_Date_Pbi < pbiToAdd.End_Date_Pbi)
                    {
                        if (_sprintService.GetById(pbiToAdd.Sprint_Id).Start_Date_Sprint <= pbiToAdd.Start_Date_Pbi && _sprintService.GetById(pbiToAdd.Sprint_Id).End_Date_Sprint >= pbiToAdd.End_Date_Pbi)
                        {
                            _pbiService.AddPbi(pbiToAdd);
                            return Ok("Pbi was added!");
                        }
                        return BadRequest("Pbi start date should be > sprint start date and Pbi end date should be < Sprint end date");
                    }
                    return BadRequest("Start date should be < End date");
                }

            }
            return BadRequest("User or Sprint doesn't exist!");
        }
    
        
        private ActionResult GetDbTasksByPbiId(int pbiId)
        {
            var foundPbi = _pbiService.GetTasksByPbiId(pbiId);
            if (foundPbi.Count >= 1)
            {
                return Ok(foundPbi);
            }
            return BadRequest("Task was not found!");
        }

        private ActionResult<PbiRequest> UpdateDbPbi(int id, PbiRequest pbi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var foundPbi = _pbiService.GetById(id);
                if (foundPbi != null)
                {
                    var pbiUpdate = new PbiRequest
                    {
                        Pbi_Name = pbi.Pbi_Name,
                        Pbi_Description = pbi.Pbi_Description,
                        Start_Date_Pbi = pbi.Start_Date_Pbi,
                        End_Date_Pbi = pbi.End_Date_Pbi,
                        Id = pbi.Id,
                        Sprint_Id = pbi.Sprint_Id,
                        pbi_type = pbi.pbi_type
                    };

                    if (_userService.GetUserById(pbiUpdate.Id) != null && _sprintService.GetById(pbiUpdate.Sprint_Id) != null)
                    {

                        if (pbiUpdate.Start_Date_Pbi < pbiUpdate.End_Date_Pbi)
                        {
                            if (_sprintService.GetById(pbiUpdate.Sprint_Id).Start_Date_Sprint <= pbiUpdate.Start_Date_Pbi && _sprintService.GetById(pbiUpdate.Sprint_Id).End_Date_Sprint >= pbiUpdate.End_Date_Pbi)
                            {
                                _pbiService.Update(id, pbiUpdate);
                                return Ok("Pbi was updated!");
                            }
                            return BadRequest("Pbi start date should be > sprint start date and Pbi end date should be < Sprint end date");
                        }
                        return BadRequest("Start date should be < End date");
                        
                    }

                    return BadRequest("Pbi wasn't updated");
                }
                return BadRequest("Pbi doesn't exist");
            }
        }

        private ActionResult DeletePbiById(int id)
        {
            var foundPbi = _pbiService.GetById(id);
            if (foundPbi != null)
            {
                _pbiService.Delete(id);
                return Ok("Pbi deleted!");
            }
            return BadRequest("Failed to delete pbi!");
        }

        private dynamic DeleteDbPbiByName(string name)
        {
            var foundPbi = _pbiService.GetByName(name);
            if (foundPbi != null)
            {
                _pbiService.DeleteByName(name);
                return Ok("Pbi deleted!");
            }
            return BadRequest("Failed to delete pbi!");
        }
    }
    
}
