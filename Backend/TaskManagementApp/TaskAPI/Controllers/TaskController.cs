using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PbiServices.Model;
using PbiServices.Service;
using TaskAPI.Model;
using TasksServices.Model;
using TasksServices.Service;
using UserServices.Service;
using Task = TasksServices.Model.Task;


namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IPbiService _pbiService;
        public TaskController(ITaskService taskService,IUserService userService,IPbiService pbiService)
        {
            _taskService = taskService;
            _userService = userService;
            _pbiService = pbiService;
        }

        /// <summary>
        /// Get All Tasks
        /// </summary>
        /// <remarks>
        /// Get a list of all Tasks
        /// </remarks>
        /// <returns>List of Tasks</returns>
        /// <response code="200">Task</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllTasks")]
        public IEnumerable<Task> GetAll() => GetAllTasks();

        /// <summary>
        /// Get All Tasks with status='To do'
        /// </summary>
        /// <remarks>
        /// Get a list of all Tasks with status='To do'
        /// </remarks>
        /// <returns>List of Tasks</returns>
        /// <response code="200">Task</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllToDoTasks")]
        public IEnumerable<Task> GetAllToDo() => GetAllToDoTasks();

        /// <summary>
        /// Get All Tasks with status='In progress'
        /// </summary>
        /// <remarks>
        /// Get a list of all Tasks with status='In progress'
        /// </remarks>
        /// <returns>List of Tasks</returns>
        /// <response code="200">Task</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllInProgress")]
        public IEnumerable<Task> GetAllInProgress() => GetAllInProgressTasks();


        /// <summary>
        /// Get All Tasks with status='Done'
        /// </summary>
        /// <remarks>
        /// Get a list of all Tasks with status='Done'
        /// </remarks>
        /// <returns>List of Tasks</returns>
        /// <response code="200">Task</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetAllDone")]
        public IEnumerable<Task> GetAllDone() => GetAllDoneTasks();

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <remarks>
        /// Get task by id
        /// </remarks>
        /// <param name="id">task id</param>
        /// <returns>Task</returns>
        /// <response code="200">Task</response>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetTaskById/{id}")]
        public ActionResult<Task> GetTaskById([FromRoute(Name = "id")] int id) => GetDbTaskById(id);


        /// <summary>
        /// Get task by name
        /// </summary>
        /// <remarks>
        /// Get task by name
        /// </remarks>
        /// <param name="name">task name</param>
        /// <returns>Task</returns>
        /// <response code ="200">Task</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetTaskByName/{name}")]
        public ActionResult<Task> GetTaskByName([FromRoute(Name = "name")] string name) => GetDbTaskByName(name);


        /// <summary>
        /// Get task by user id and pbi id
        /// </summary>
        /// <remarks>
        /// Get task by user id and pbi id
        /// </remarks>
        /// <param name="id">user id</param>
        /// /// <param name="pbi_id">pbi id</param>
        /// <returns>Task</returns>
        /// <response code ="200">Task</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetTaskByUserIdPbiId/{user_id}/{pbi_id}")]
        public ActionResult<Task> GetTaskByUserIdPbiId([FromRoute(Name = "user_id")] int user_id, [FromRoute(Name = "pbi_id")] int pbi_id) => GetDbTaskByUserIdAndPbiId(user_id, pbi_id);


        /// <summary>
        /// Add task
        /// </summary>
        /// <remarks>
        /// Add a task in database
        /// </remarks>
        /// <param name="task"></param>
        /// <response code ="200">Task</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddTask")]
        public ActionResult<TaskRequestModel> PostTask([FromBody] TaskRequestModel task) => AddDbTask(task);


        /// <summary>
        /// Delete task
        /// </summary>
        /// <remarks>Delete a task from database</remarks>
        /// <param name="id">task id</param>
        /// <response code = "200">Task</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteTask/{id}")]
        public dynamic DeleteTask([FromRoute(Name = "id")] int id) => DeleteDbTaskById(id);


        /// <summary>
        /// Delete task by name
        /// </summary>
        /// <remarks>Delete a task from database by name</remarks>
        /// <param name="id">task id</param>
        /// <response code = "200">Task</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteTaskByName/{name}")]
        public dynamic DeleteTaskByName([FromRoute(Name = "name")] string name) => DeleteDbTaskByName(name);


        /// <summary>
        /// Update task
        /// </summary>
        /// <remarks>Update a task from database</remarks>
        /// <param name="id">task id</param>
        /// <param name="task"></param>
        /// <returns>Task</returns>
        /// <response code = "200">Task</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("UpdateTask/{id}")]
        public ActionResult<TaskRequest> UpdateTask([FromRoute(Name = "id")] int id, [FromBody] TaskRequest task) => UpdateDbTask(id, task);



        private IEnumerable<Task> GetAllTasks()
        {
            return _taskService.GetAll();
        }
        private IEnumerable<Task> GetAllToDoTasks()
        {
            return _taskService.GetAllToDo();

        }
        private IEnumerable<Task> GetAllInProgressTasks()
        {
            return _taskService.GetAllInProgress();
        }
        private IEnumerable<Task> GetAllDoneTasks()
        {
            return _taskService.GetAllDone();
        }

        private ActionResult  GetDbTaskById(int id)
        {   
            var foundTask = _taskService.GetById(id);
            if (foundTask != null)
            {
                return Ok(foundTask);
            }
            return BadRequest("Task was not found!");

        }
        private ActionResult GetDbTaskByUserIdAndPbiId(int user_id,int pbi_id)
        {
            List<Task> foundTask = _taskService.GetByUserIdAndPbiId(user_id, pbi_id);
            if (foundTask.Count>=1)
            {
      
                return Ok(foundTask);
            }
            return BadRequest("Task was not found!");
            
        }
        private ActionResult GetDbTaskByName(string name)
        {
            var foundTask = _taskService.GetByName(name);
            if(foundTask!=null)
            {
                return Ok(foundTask);
            }
            return BadRequest("Task was not found!");
        }
        private ActionResult AddDbTask(TaskRequestModel task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
            
            {
                if(_taskService.GetByName(task.Task_Name)!=null)
                {
                    return BadRequest("This task already exist!");
                }
               var taskToAdd = new Task
                {
                   Task_Name = task.Task_Name,
                   Task_Description = task.Task_Description,
                   Id=task.Id,
                   Pbi_Id = task.Pbi_Id,
                   
                };
                if (_userService.GetUserById(taskToAdd.Id) != null && _pbiService.GetById(taskToAdd.Pbi_Id)!=null)
                {
                    _taskService.AddTask(taskToAdd);
                    return Ok("Task was added!");
                }
                return BadRequest("User or Pbi doesn't exist!");
                
               
            }
        }
        private ActionResult <TaskRequest> UpdateDbTask(int id, TaskRequest task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var foundTask = _taskService.GetById(id);
                if (foundTask != null)
                {
                    var taskToUpdate = new TaskRequest
                    {
                        Task_Name = task.Task_Name,
                        Task_Description = task.Task_Description,
                        Id = task.Id,
                        Pbi_Id = task.Pbi_Id,
                        Task_Status = task.Task_Status,
                        
                    };
                    if (_userService.GetUserById(taskToUpdate.Id) != null && _pbiService.GetById(taskToUpdate.Pbi_Id) != null)
                    {
                        _taskService.Update(id,taskToUpdate);
                        return Ok("Task was updated!");
                    }
                    return BadRequest("User or Pbi doesn't exist!");
                }
                return BadRequest("Task was not found!");
            }
        }
        private dynamic DeleteDbTaskById(int id)
        {
            var foundTask = _taskService.GetById(id);
            if(foundTask!=null)
            {
                _taskService.Delete(id);
                return Ok("Task was deleted!");
            }
            return BadRequest("This task doesn't exist!");
    }
        private dynamic DeleteDbTaskByName(string name)
        {
            var foundTask = _taskService.GetByName(name);
            if (_taskService != null)
            {
                _taskService.Delete(foundTask.Task_Id);
                return Ok("Task was deleted!");
            }
            return BadRequest("Task was not found!");
        }
    }
    
}
