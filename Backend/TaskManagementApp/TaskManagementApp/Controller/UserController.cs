using AssignmentServices.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectServices.Service;
using SprintServices.Model;
using System.Collections.Generic;
using System.Security.Claims;
using TaskManagementApp.Dto;
using TaskManagementApp.Model;
using UserServices.Model;
using UserServices.Service;
using User = UserServices.User;

namespace TaskManagementApp.Controller
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserAuthorizationService _authorization;
        private readonly IProjectService _projectService;
        //private readonly IHttpContextAccessor _contextAccesor;

        public UserController(IUserService userService, IUserAuthorizationService authorization,IProjectService projectService)
        {
            _userService = userService;
            _authorization = authorization;
            //_contextAccesor = httpContextAccessor;
            _projectService = projectService;
           
        }
        

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult Register([FromBody] UserRequestModel user) => AddDbUser(user);


        //va urma sa returneze un token jwt
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] UserRequestModel user) => LoginUser(user);


        /// <summary>
        /// Get All Users
        /// </summary>
        /// <remarks>
        /// Get a list of all Users
        /// </remarks>
        /// <returns>List of Users</returns>
        /// <response code="200">User</response>   
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        [HttpGet("GetAllUsers")]
        public IEnumerable<User> GetAll() => GetAllDbUsers();


        /// <summary>
        /// Get user by id
        /// </summary>
        /// <remarks>
        /// Get user by id
        /// </remarks>
        /// <param name="id">user id</param>
        /// <returns>User</returns>
        /// <response code="200">User</response>  
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetUserById/{id}")]
        public ActionResult<User> GetById([FromRoute(Name = "id")] int id) => GetDbUserById(id);

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <remarks>
        /// Get user by username
        /// </remarks>
        /// <param name="username">user username</param>
        /// <returns>User</returns>
        /// <response code ="200">User</response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetUserByUsername/{username}")]
        public ActionResult<User> GetUserByUsername([FromRoute(Name = "username")] string username) => GetDbUserByUsername(username);

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <remarks>
        /// Get user by email
        /// </remarks>
        /// <param name="email">user email</param>
        /// <returns>User</returns>
        /// <response code ="200">User</response>        
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetUserByEmail/{email}")]
        public ActionResult<User> GetUserByEmail([FromRoute(Name = "email")] string email) => GetDbUserByEmail(email);


        /// <summary>
        /// Get assignment by user_id and project_id
        /// </summary>
        /// <remarks>
        /// Get assignment by user_id and project_id
        /// </remarks>
        /// <param name="user_id">user id</param>
        ///  /// <param name="project_id">project id</param>
        /// <returns>Assignment</returns>
        /// <response code ="200">Assignment</response>        
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("GetEnrollment/{user_id}/{project_id}")]
        public ActionResult<Assignment> GetEnrollment([FromRoute] int user_id, [FromRoute] int project_id) => GetDbEnrollment(user_id, project_id);


        /// <summary>
        /// Get all users by project id
        /// </summary>
        /// <remarks>Returns all users that work on a specific id</remarks>
        /// <param name="id">project id</param>
        /// <response code = "200">IEnumerable<User></User></response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("UsersByProject/{id}")]
        public IEnumerable<User> GetAllUsersByProjectId([FromRoute] int id) => GetAllUsersByProject(id);

        /// <summary>
        /// Add user
        /// </summary>
        /// <remarks>
        /// Add a user in database
        /// </remarks>
        /// <param name="user"></param>
        /// <response code ="200">User</response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("AddUser")]
        public ActionResult <User> PostUser([FromBody] UserRequestModel user) => AddDbUser(user);

        /// <summary>
        /// Enrolls user to specific project
        /// </summary>
        /// <remarks>
        /// Add user, project relation to database
        /// </remarks>
        /// <param name="user_id"></param>
        /// /// <param name="project_id"></param>
        /// <response code ="200">User</response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("EnrollUser/{user_id}/{project_id}")]
        public dynamic EnrollUserToProject([FromRoute] int user_id, [FromRoute] int project_id) => EnrollUser(user_id, project_id);

        /// <summary>
        /// Update user
        /// </summary>
        /// <remarks>Update a user from database</remarks>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>User</returns>
        /// <response code = "200">User</response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("UpdateUser/{id}")]
        public ActionResult<User> UpdateUser([FromRoute] int id, [FromBody] UserRequestModel user) => UpdateDbUser(id, user);

        /// <summary>
        /// Delete user
        /// </summary>
        /// <remarks>Delete a user from database</remarks>
        /// <param name="id">user id</param>
        /// <response code = "200">User</response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteUser/{id}")]
        public dynamic DeleteUser([FromRoute] int id) => DeleteDbUser(id);

        /// <summary>
        /// Delete assignment
        /// </summary>
        /// <remarks>Delete an assignment from database</remarks>
        /// <param name="user_id">user id</param>
        /// <param name="project_id">project id</param>
        /// <response code = "200">User</response>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("DeleteAssignment/{user_id}/{project_id}")]
        public dynamic DeleteAssignment([FromRoute] int user_id, [FromRoute] int project_id) => DisenrollUser(user_id,project_id);

        private IEnumerable<UserServices.User> GetAllDbUsers()
        {
            return _userService.GetAllUsers();
        }

        private ActionResult GetDbUserById(int id)
        {
            //TODO: mesaj daca id-ul nu exista in baza de date - returnati 400 bad request cu mesaj
            User foundUser = _userService.GetUserById(id);
            if (foundUser != null)
            {
                foundUser.Projects = _userService.GetProjectByUserId(id);
                foundUser.Tasks = _userService.GetTaskByUserId(id);
                foundUser.Pbi = _userService.GetPbiByUserId(id);

                return Ok(foundUser);
                
            }
            return BadRequest("User was not found!");
        }

        private dynamic EnrollUser(int user_id, int project_id)
        {
            var foundUser = _userService.GetUserById(user_id);
            var foundProject = _projectService.GetById(project_id);
            if(foundUser != null && foundProject!=null)
            {
                if (_userService.GetEnrollment(user_id, project_id) == null)
                {  //proiectul trebuie verificat din frontend
                    _userService.EnrollUser(user_id, project_id);
                    return Ok("User was enrolled!");
                }
                return BadRequest("This user is already enrolled to this project! ");
               
            }
            return BadRequest("User or project was not found!");
        }
        private ActionResult GetDbEnrollment(int user_id,int project_id)
        {
            var foundEnrollment = _userService.GetEnrollment(user_id, project_id);
            if(foundEnrollment!=null)
            {
                return Ok(foundEnrollment);
            }
            return BadRequest("This enrollment doesn't exist!");
        }

        private ActionResult GetDbUserByUsername(string username)
        {
            //TODO: mesaj daca username-ul nu exista in baza de date, returnati 400 bad request
            User foundUser = _userService.GetUserByUsername(username);
            if (foundUser != null)
            {
                foundUser.Projects = _userService.GetProjectByUserId(foundUser.Id);
                foundUser.Tasks = _userService.GetTaskByUserId(foundUser.Id);
                foundUser.Pbi = _userService.GetPbiByUserId(foundUser.Id);

                return Ok(foundUser);
            }
            return BadRequest("User was not found!");
        }

        private ActionResult GetDbUserByEmail(string email)
        {
            User foundUser = _userService.GetUserByEmail(email);
            if (foundUser != null)
            {
                foundUser.Projects = _userService.GetProjectByUserId(foundUser.Id);
                foundUser.Tasks = _userService.GetTaskByUserId(foundUser.Id);
                foundUser.Pbi = _userService.GetPbiByUserId(foundUser.Id);

                return Ok(foundUser);
            }
            return BadRequest("User with this email adress was not found!");
        }

        private IEnumerable<User> GetAllUsersByProject(int id)
        {
            return _userService.GetUsersByProject(id);
        }

        //TODO: parametrul este userrequestmodel - l-am pus eu corect
        private ActionResult AddDbUser(UserRequestModel user)
        {
            //TODO: validare pentru proprietatile obiectului
            //TODO: mesaj si return 400 daca nu e valid, cu mesaj
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                if (_userService.GetUserByUsername(user.Username) != null)
                {
                    return BadRequest("Username already exists!");
                }

                var userToAdd = new UserServices.User
                {
                    Password = _authorization.HashPassword(user.Password),
                    Username = user.Username,
                    Email = user.Email
                };

                _userService.AddUser(userToAdd);
                return Ok("User was added!");
            }

        }

        //TODO: La fel ca la add, parametrul trebuie sa fie de tipul UserRequestModel! --> UpdateDbUser(int id, UserRequestModel user)
        private ActionResult UpdateDbUser(int id, UserRequestModel user)
        {
            //TODO: validate field-uri user
            //TODO: return 400 daca nu e valid, cu mesaj
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var userToUpdate = new UserRequest
                {
                    Password = _authorization.HashPassword(user.Password),
                    Username = user.Username,
                    Email = user.Email
                };
                var foundUser = _userService.GetUserById(id);
                if (foundUser != null)
                {
                    _userService.UpdateUser(id, userToUpdate);
                    return Ok("User was updated!");
                }

                return BadRequest("User doesn't exist");
            }
        }

        //TODO: nu trebuie sa returneze void. Trebuie sa returneze ceva in asa fel incat in partea de UI sa avem ca informatie daca a fost sau nu sters user-ul. las in comentariu cum ar trebui sa arate
        // 
        private dynamic DeleteDbUser(int id)
        {
            var foundUser = _userService.GetUserById(id);
            if(foundUser!=null)
            {
                _userService.DeleteUser(id);
                return Ok("User deleted!");
            }
            return BadRequest("Failed!");
        }
        //private dynamic DeleteDbAssignment(int user_id,int project_id)
        //{
        //    var foundAssignment = _userService.GetEnrollment(user_id, project_id);
        //    if (foundAssignment != null)
        //    {
        //        _userService.DisenrollUser(user_id, project_id);

        //        return Ok("Assignment was deleted!");
        //    }
        //    return BadRequest("Failed!");
        //}


        private dynamic DisenrollUser(int user_id, int project_id)
        {
            var foundAssignment = _userService.GetEnrollment(user_id, project_id);
            if (foundAssignment != null)
            {
                List <Sprint> foundSprints = _projectService.GetSprintsByProjectId(project_id);
                foreach (Sprint foundSprint in foundSprints)
                {
                    _userService.DeleteRelatedPbiAndTasks(foundSprint.Sprint_Id);
                }
                _userService.DisenrollUser(user_id, project_id);

                return Ok("Current user was disenrolled from this project!");
            }
            return BadRequest("Failed!");
        }


       


        private ActionResult LoginUser(UserRequestModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var foundUser = _userService.GetUserByUsername(user.Username);
            if (foundUser == null) return BadRequest("User not found!");

            var samePassword = _authorization.VerifyHashedPassword(foundUser.Password, user.Password);
            if (!samePassword) return BadRequest("Invalid password!");

            var user_jsonWebToken = _authorization.GetToken(foundUser);

            //if(!_authorization.ValidateToken(user_jsonWebToken)) return Unauthorized();

            return Ok(new ResponseLogin
            {
                Token = user_jsonWebToken
            });
        }
       
    }
}