using PbiServices.Model;
using ProjectServices.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TasksServices.Model;

namespace UserServices
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsDeleted { get; set; }

        public IEnumerable<Project> Projects { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
        public IEnumerable<Pbi> Pbi { get; set; }
    }
}
