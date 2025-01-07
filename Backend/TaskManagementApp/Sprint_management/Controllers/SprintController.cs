using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PbiServices.Model;
using ProjectServices.Service;
using SprintAPI.Model;
using SprintServices.Model;
using SprintServices.Service;

namespace SprintAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _sprintService;
        private readonly IProjectService _projectService;
        public SprintController(ISprintService sprintService, IProjectService projectService)
        {
            _sprintService = sprintService;
            _projectService = projectService;
        }

        /// <summary>
        /// Add sprint
        /// </summary>
        /// <remarks>
        /// Add a sprint in database
        /// </remarks>
        /// <param name="sprint"></param>
        /// <response code ="200">Sprint</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddSprint")]
        public ActionResult<Sprint> PostSprint([FromBody] SprintRequestModel sprint) => AddDbSprint(sprint);

        /// <summary>
        /// Get All Sprints
        /// </summary>
        /// <remarks>
        /// Get a list of all Sprints
        /// </remarks>
        /// <returns>List of Sprints</returns>
        /// <response code="200">Sprint</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllSprints")]
        public IEnumerable<Sprint> GetAll() => GetAllSprints();

        /// <summary>
        /// Get sprint by name
        /// </summary>
        /// <remarks>
        /// Get sprint by name
        /// </remarks>
        /// <param name="name">sprint name</param>
        /// <returns>Sprint</returns>
        /// <response code ="200">Sprint</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetSprintByName/{name}")]
        public ActionResult<Sprint> GetSprintByName([FromRoute(Name = "name")] string name) => GetDbSprintByName(name);

        /// <summary>
        /// Get sprint by id
        /// </summary>
        /// <remarks>
        /// Get sprint by id
        /// </remarks>
        /// <param name="id">sprint id</param>
        /// <returns>Sprint</returns>
        /// <response code="200">Sprint</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetSprintById/{id}")]
        public ActionResult<Sprint> GetSprintById([FromRoute(Name = "id")] int id) => GetDbSprintById(id);

        /// <summary>
        /// Delete sprint
        /// </summary>
        /// <remarks>Delete a sprint from database</remarks>
        /// <param name="id">sprint id</param>
        /// <response code = "200">Sprint</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteSprint/{id}")]
        public dynamic DeleteSprint([FromRoute(Name = "id")] int id) => DeleteDbSprintById(id);

        /// <summary>
        /// Delete sprint by name
        /// </summary>
        /// <remarks>Delete a sprint from database by name</remarks>
        /// <param name="id">sprint id</param>
        /// <response code = "200">Sprint</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteSprintByName/{name}")]
        public dynamic DeleteSprintByName([FromRoute(Name = "name")] string name) => DeleteDbSprintByName(name);

        /// <summary>
        /// Update sprint
        /// </summary>
        /// <remarks>Update a sprint from database</remarks>
        /// <param name="id">sprint id</param>
        /// <param name="sprint"></param>
        /// <returns>Sprint</returns>
        /// <response code = "200">Sprint</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("UpdateSprint/{id}")]
        public ActionResult<Sprint> PutSprint([FromRoute(Name = "id")] int id, [FromBody] SprintRequestModel sprint) => UpdateDbSprint(id, sprint);

        /// <summary>
        /// Get sprint or sprints by projectId
        /// </summary>
        /// <remarks>
        /// Get sprint or sprints by projectId
        /// </remarks>
        /// <param name="projectId">sprint projectId</param>
        /// <returns>Sprint</returns>
        /// <response code="200">Sprint</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetByProjectId/{projectId}")]
        public ActionResult <Sprint> GetSprintByProjectId([FromRoute(Name = "projectId")] int projectId) => GetDbSprintByProjectId(projectId);


        private IEnumerable<Sprint> GetAllSprints()
        {
            return _sprintService.GetAll();
        }

        private ActionResult GetDbSprintByName(string name)
        {
            var foundSprint = _sprintService.GetByName(name);
            if(foundSprint != null)
            {
                var relatedPbi = _sprintService.GetPbiById(foundSprint.Sprint_Id);
                foundSprint.Pbi = relatedPbi;
                return Ok(foundSprint);
            }
            return BadRequest("Sprint was not found!");
        }

        private ActionResult AddDbSprint(SprintRequestModel sprint)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if(_sprintService.GetByName(sprint.Sprint_Name)!=null)
                {
                    return BadRequest("Sprint already exists!");
                }
                var sprintToAdd = new Sprint
                {
                    Sprint_Name = sprint.Sprint_Name,
                    Sprint_Description=sprint.Sprint_Description,
                    Start_Date_Sprint=sprint.Start_Date_Sprint,
                    End_Date_Sprint=sprint.End_Date_Sprint,
                    Project_Id=sprint.Project_Id,
                };
                if (_projectService.GetById(sprintToAdd.Project_Id) != null)
                {   if (sprintToAdd.Start_Date_Sprint < sprintToAdd.End_Date_Sprint)
                    {
                        if (_projectService.GetById(sprintToAdd.Project_Id).Start_Date_Project <= sprintToAdd.Start_Date_Sprint && _projectService.GetById(sprintToAdd.Project_Id).End_Date_Project >= sprintToAdd.End_Date_Sprint)
                        {
                            _sprintService.Add(sprintToAdd);
                            return Ok("Sprint was added!");
                        }
                        return BadRequest("Sprint start date should be > project start date and Sprint end date should be < Project end date");
                    }
                    return BadRequest("Start date should be < End date");
                }
                return BadRequest("Project doesn't exist!");
            }
        }

        private ActionResult GetDbSprintById(int id)
        {
            var foundSprint= _sprintService.GetById(id);
            if(foundSprint!=null)
            {
                var relatedPbi = _sprintService.GetPbiById(foundSprint.Sprint_Id);
                foundSprint.Pbi = relatedPbi;
                return Ok(foundSprint);
            }
            return BadRequest("Sprint was not found!");
        }
        
        private dynamic DeleteDbSprintById(int id)
        {
            var foundSprint = _sprintService.GetById(id);
            if(foundSprint != null)
            {
                _sprintService.Delete(id);
                return Ok("Sprint was deleted!");
            }
            return BadRequest("Sprint was not found!");
        }
        private dynamic DeleteDbSprintByName(string name)
        {
            var foundSprint = _sprintService.GetByName(name);
            if (foundSprint != null)
            {
                _sprintService.Delete(foundSprint.Sprint_Id);
                return Ok("Sprint was deleted!");
            }
            return BadRequest("Sprint was not found!");
        }

        private ActionResult UpdateDbSprint(int id, SprintRequestModel sprint)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var foundSprint = _sprintService.GetById(id);
                if(foundSprint!=null)
                {
                    var sprintToUpdate = new SprintRequest
                    {
                        Sprint_Name = sprint.Sprint_Name,
                        Sprint_Description=sprint.Sprint_Description,
                        Start_Date_Sprint = sprint.Start_Date_Sprint,
                        End_Date_Sprint=sprint.End_Date_Sprint,
                        Project_Id=sprint.Project_Id,
                    };
                    if (_projectService.GetById(sprintToUpdate.Project_Id) != null)
                    {
                        if (sprintToUpdate.Start_Date_Sprint < sprintToUpdate.End_Date_Sprint)
                        {
                            if (_projectService.GetById(sprintToUpdate.Project_Id).Start_Date_Project <= sprintToUpdate.Start_Date_Sprint && _projectService.GetById(sprintToUpdate.Project_Id).End_Date_Project >= sprintToUpdate.End_Date_Sprint)
                            {
                                _sprintService.Update(id,sprintToUpdate);
                                return Ok("Sprint was added!");
                            }
                            return BadRequest("Sprint start date should be > project start date and Sprint end date should be < Project end date");
                        }
                        
                        return BadRequest("Start date should be < End date");
                    }
                    return BadRequest("Project doesn't exist!");
                }
                return BadRequest("Sprint was not found!");
            }
        }

        private ActionResult GetDbSprintByProjectId(int projectId)
        {

            //var foundSprint = _sprintService.GetByProjectId(projectId);
            //if(foundSprint.Count >= 1)
            //{
            //    return Ok(foundSprint);
            //}
            //return BadRequest("Sprint was not found!");
            List <Sprint> foundSprint = _sprintService.GetByProjectId(projectId);
            if (foundSprint.Count >= 1)
            {
                foreach(Sprint sprint in foundSprint)
                {
                    var foundPbi = _sprintService.GetPbiById(sprint.Sprint_Id);
                    sprint.Pbi = foundPbi;
                }
                return Ok(foundSprint);
            }
            return BadRequest("Sprint was not found!");
        }
    }
}
