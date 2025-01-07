using AssignmentAPI.Model;
using AssignmentServices.Model;
using AssignmentServices.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectServices.Service;
using UserServices.Service;

namespace AssignmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        public AssignmentController(IAssignmentService assignmentService,IUserService userService,IProjectService projectService)
        {
            _assignmentService = assignmentService;
            _userService = userService;
            _projectService = projectService;
        }

        /// <summary>
        /// Get All Assignment
        /// </summary>
        /// <remarks>
        /// Get a list of all Assignment
        /// </remarks>
        /// <returns>List of all Assignment</returns>
        /// <response code="200">Assignment</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllAssignment")]
        public IEnumerable<Assignment> GetAll() => GetAllAssignment();


        /// <summary>
        /// Get assignment by id
        /// </summary>
        /// <remarks>
        /// Get assignment by id
        /// </remarks>
        /// <param name="id">assignment id</param>
        /// <returns>Assignment</returns>
        /// <response code="200">Assignment</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAssignmentById/{id}")]
        public ActionResult<Assignment> GetAssignmentById([FromRoute(Name = "id")] int id) => GetDbAssignmentById(id);

        /// <summary>
        /// Get assignment by UserId
        /// </summary>
        /// <remarks>
        /// Get assignment by UserId
        /// </remarks>
        /// <param name="id">user id</param>
        /// <returns>Assignment</returns>
        /// <response code="200">Assignment</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAssignmentByUserId/{id}")]
        public ActionResult<Assignment> GetAssignmentByUserId([FromRoute(Name = "id")] int id) => GetDbAssignmentByUserId(id);


        /// <summary>
        /// Get assignment by ProjectId
        /// </summary>
        /// <remarks>
        /// Get assignment by ProjectId
        /// </remarks>
        /// <param name="id">project id</param>
        /// <returns>Assignment</returns>
        /// <response code="200">Assignment</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAssignmentByProjectId/{id}")]
        public ActionResult<Assignment> GetAssignmentByProjectId([FromRoute(Name = "id")] int id) => GetDbAssignmentByProjectId(id);


        /// <summary>
        /// Add assignment
        /// </summary>
        /// <remarks>
        /// Add a assignment in database
        /// </remarks>
        /// <param name="assignment"></param>
        /// <response code ="200">Assignment</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddAssignment")]
        public ActionResult<AssignmentRequestModel> AddAssignment([FromBody] AssignmentRequestModel assignment) => AddDbAssignment(assignment);


        /// <summary>
        /// Delete assignment
        /// </summary>
        /// <remarks>Delete an assignment from database</remarks>
        /// <param name="id">assignment id</param>
        /// <response code = "200">Assignment</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteAssignment/{id}")]
        public dynamic DeleteProject([FromRoute(Name = "id")] int id) => DeleteDbAssignment(id);


        /// <summary>
        /// Update assignment
        /// </summary>
        /// <remarks>Update an assignment from database</remarks>
        /// <param name="id">assignment id</param>
        /// <param name="assignment"></param>
        /// <returns>Assignment</returns>
        /// <response code = "200">Assignment</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("UpdateAssignment/{id}")]
        public ActionResult<AssignmentRequest> PutProject([FromRoute(Name = "id")] int id, [FromBody] AssignmentRequest assignment) => UpdateDbAssignment(id, assignment);
        private IEnumerable<Assignment> GetAllAssignment()
        {
            return _assignmentService.GetAll();
        }

        private ActionResult GetDbAssignmentById(int id)
        {
            var foundAssignment =_assignmentService.GetById(id);
            if (foundAssignment != null)
            {
                return Ok(foundAssignment);
            }
            return BadRequest("Assignment was not found!");
        }
        private ActionResult GetDbAssignmentByUserId(int id)
        {
            var foundAssignment = _assignmentService.GetByUserId(id);
            if (foundAssignment != null)
            {
                return Ok(foundAssignment);
            }
            return BadRequest("Assignment was not found!");
        }
        private ActionResult GetDbAssignmentByProjectId(int id)
        {
            var foundAssignment = _assignmentService.GetByProjectId(id);
            if (foundAssignment != null)
            {
                return Ok(foundAssignment);
            }
            return BadRequest("Assignment was not found!");
        }

        private ActionResult AddDbAssignment(AssignmentRequestModel assignment)

        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var assignmentToAdd = new Assignment
                {
                    Id = assignment.Id,
                    Project_Id = assignment.Project_Id,

                };
                if (_projectService.GetById(assignmentToAdd.Project_Id) != null && _userService.GetUserById(assignmentToAdd.Id) !=null)
                {
                    _assignmentService.Add(assignmentToAdd);
                    return Ok("Assignment was added!");
                }
                return BadRequest("Project or User doesn't exist!");
            }
        }
        private dynamic DeleteDbAssignment(int id)
        {
            var foundAssignment = _assignmentService.GetById(id);

            if (foundAssignment != null)
            {
                _assignmentService.Delete(id);
                return Ok("Assignment was deleted!");
            }
            return BadRequest("Assignment was not found!");
        }


        private ActionResult UpdateDbAssignment(int id, AssignmentRequest assignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var foundAssignment = _assignmentService.GetById(id);
                if (foundAssignment != null)
                {
                    var assignmentToUpdate = new AssignmentRequest
                    {
                        Id=assignment.Id,
                        Project_Id=assignment.Project_Id,
                    };
                    if (_projectService.GetById(assignmentToUpdate.Project_Id) != null && _userService.GetUserById(assignmentToUpdate.Id) != null)
                    {
                        _assignmentService.Update(id,assignmentToUpdate);
                        return Ok("Assignment was added!");
                    }
                    return BadRequest("Project or User doesn't exist!");
                }
                return BadRequest("Assignment was not found!");
            }
        }
    }
}
