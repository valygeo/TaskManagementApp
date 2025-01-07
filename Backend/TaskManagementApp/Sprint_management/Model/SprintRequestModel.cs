using ProjectServices.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SprintAPI.Model
{
    public class SprintRequestModel
    {
        public string Sprint_Name { get; set; } 
        public string Sprint_Description { get; set; }
        public DateTime Start_Date_Sprint { get; set; }
        public DateTime End_Date_Sprint { get; set; }
        public int Project_Id { get; set; }
    }
}
