using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Model;
using ProjectServices.Model;
using ProjectServices.Service;
using SprintServices.Model;

namespace ProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Get All Projects
        /// </summary>
        /// <remarks>
        /// Get a list of all Projects
        /// </remarks>
        /// <returns>List of Projects</returns>
        /// <response code="200">Sprint</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllProjects")]
        public IEnumerable<Project> GetAll() => GetAllProjects();


        /// <summary>
        /// Get project by name
        /// </summary>
        /// <remarks>
        /// Get project by name
        /// </remarks>
        /// <param name="name">project name</param>
        /// <returns>Project</returns>
        /// <response code ="200">Sprint</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetProjectByName/{name}")]
        public ActionResult<ProjectRequestModel> GetProjectByName([FromRoute(Name = "name")] string name) => GetDbProjectByName(name);


        /// <summary>
        /// Add project
        /// </summary>
        /// <remarks>
        /// Add a project in database
        /// </remarks>
        /// <param name="project"></param>
        /// <response code ="200">Sprint</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddProject")]
        public ActionResult<ProjectRequestModel> AddProject([FromBody] ProjectRequestModel project) => AddDbProject(project);

        /// <summary>
        /// Add project
        /// </summary>
        /// <remarks>
        /// Add a project in database
        /// </remarks>
        /// <param name="project"></param>
        /// <response code ="200">Project</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddProjectWithAssignment/{user_id}")]
        public ActionResult<ProjectRequestModel> AddProjectWithUser([FromRoute] int user_id,[FromBody] ProjectRequestModel project) => AddDbProjectWithAssignment(user_id,project);


        /// <summary>
        /// Get project by id
        /// </summary>
        /// <remarks>
        /// Get project by id
        /// </remarks>
        /// <param name="id">project id</param>
        /// <returns>Project</returns>
        /// <response code="200">Sprint</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetProjectById/{id}")]
        public ActionResult<ProjectRequestModel> GetProjectById([FromRoute(Name = "id")] int id) => GetDbProjectById(id);

        /// <summary>
        /// Get a list of sprints by projectId
        /// </summary>
        /// <remarks>
        /// Get a list of sprints by projectId
        /// </remarks>
        /// <param name="id">project id</param>
        /// <returns>Sprint</returns>
        /// <response code="200">Sprint</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetSprintsByProjectId/{projectId}")]
        public ActionResult<Sprint> GetSprints([FromRoute(Name = "projectId")] int projectId) => GetDbSprintsByProjectId(projectId);

        
        /// <summary>
        /// Update project
        /// </summary>
        /// <remarks>Update a project from database</remarks>
        /// <param name="id">project id</param>
        /// <param name="sprint"></param>
        /// <returns>Project</returns>
        /// <response code = "200">Project</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("UpdateProject/{id}")]
        public ActionResult<ProjectRequest> PutProject([FromRoute(Name = "id")] int id, [FromBody] ProjectRequest project) => UpdateDbProject(id, project);

        /// <summary>
        /// Delete project
        /// </summary>
        /// <remarks>Delete a project from database</remarks>
        /// <param name="id">project id</param>
        /// <response code = "200">Project</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteProject/{id}")]
        public dynamic DeleteProject([FromRoute(Name = "id")] int id) => DeleteDbProject(id);

        /// <summary>
        /// Delete project by name
        /// </summary>
        /// <remarks>Delete a project from database by name</remarks>
        /// <param name="id">project id</param>
        /// <response code = "200">Project</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteProjectByName/{name}")]
        public dynamic DeleteProjectByName([FromRoute(Name = "name")] string name) => DeleteDbProjectByName(name);


        
        
        private IEnumerable<Project> GetAllProjects()
        {
            return _projectService.GetAll();
        }

        private ActionResult GetDbProjectByName(string name)
        {
            var foundProject = _projectService.GetByName(name);
            if (foundProject != null)
            {
                var foundSprints = _projectService.GetSprintsByProjectId(foundProject.Project_Id);
                foundProject.Sprints = foundSprints; 
                return Ok(foundProject);
            }
            return BadRequest("Project was not found!");
        }

        private ActionResult AddDbProject(ProjectRequestModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (_projectService.GetByName(project.Project_Name) != null)
                {
                    return BadRequest("Project name already exists!");
                }
                var projectToAdd = new Project
                {   
                    Project_Name = project.Project_Name,
                    Project_Description = project.Project_Description,
                    Start_Date_Project = project.Start_Date_Project,
                    End_Date_Project = project.End_Date_Project
                    
                    
                };
                
                if(projectToAdd.Start_Date_Project < projectToAdd.End_Date_Project)

                {
                    _projectService.Add(projectToAdd);
                    
                    return Ok("Project was added!");
                }
                return BadRequest("Start Date should be < End Date");
            }
        }

        private ActionResult AddDbProjectWithAssignment(int user_id,ProjectRequestModel project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (_projectService.GetByName(project.Project_Name) != null)
                {
                    return BadRequest("Project name already exists!");
                }
                var projectToAdd = new Project
                {
                    Project_Name = project.Project_Name,
                    Project_Description = project.Project_Description,
                    Start_Date_Project = project.Start_Date_Project,
                    End_Date_Project = project.End_Date_Project


                };
                Project projectToEnroll;
                if (projectToAdd.Start_Date_Project < projectToAdd.End_Date_Project)

                {
                    _projectService.Add(projectToAdd);
                    projectToEnroll = _projectService.GetByName(projectToAdd.Project_Name);
                    _projectService.EnrollUserToProject(user_id, projectToEnroll.Project_Id);
                    return Ok("Project was added!");
                }
                return BadRequest("Start Date should be < End Date");
            }
        }

        private ActionResult GetDbProjectById(int id)
        {
            var foundProject = _projectService.GetById(id);
            if (foundProject != null)
            {
                var foundSprint = _projectService.GetSprintsByProjectId(id);
                foundProject.Sprints = foundSprint;
                return Ok(foundProject);
            }
            return BadRequest("Project was not found!");
        }

        private ActionResult UpdateDbProject(int id, ProjectRequest project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var foundProject = _projectService.GetById(id);
                if (foundProject != null)
                {
                    var projectToUpdate = new ProjectRequest
                    {
                        Project_Name = project.Project_Name,
                        Project_Description = project.Project_Description,
                        Start_Date_Project = project.Start_Date_Project,
                        End_Date_Project = project.End_Date_Project
                    };
                    if (projectToUpdate.Start_Date_Project < projectToUpdate.End_Date_Project)
                    {
                        _projectService.Update(id, projectToUpdate);
                        return Ok("Project was updated!");
                    }
                    return BadRequest("StartDate should be < EndDate");
                }
                return BadRequest("Project was not found!");
            }
        }

        private dynamic DeleteDbProject(int id)
        {
            var foundProject = _projectService.GetById(id);
           
            if (foundProject != null)
            {   
                _projectService.Delete(id);
                return Ok("Project was deleted!");
            }
            return BadRequest("Project was not found!");
        }

        private dynamic DeleteDbProjectByName(string name)
        {
            var foundProject = _projectService.GetByName(name);
            if (foundProject != null)
            {
                _projectService.Delete(foundProject.Project_Id);
                return Ok("Project was deleted!");
            }
            return BadRequest("Project was not found!");
        }
        private ActionResult GetDbSprintsByProjectId(int projectId)
        {
            var foundSprint = _projectService.GetSprintsByProjectId(projectId);
            if (foundSprint.Count >= 1)
            {
                return Ok(foundSprint);
            }
            return BadRequest("Sprint was not found!");
        }
       
    }
}
