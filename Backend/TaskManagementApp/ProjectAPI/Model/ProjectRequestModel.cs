using SprintServices.Model;
using System.ComponentModel.DataAnnotations;

namespace ProjectAPI.Model
{
    public class ProjectRequestModel
    {
        public string Project_Name { get; set; }

        public string Project_Description { get; set; }

        public DateTime Start_Date_Project { get; set; }

        public DateTime End_Date_Project { get; set; }

    }
}
