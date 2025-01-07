using PbiServices.Model;

namespace PbiAPI.Model
{
    public class PbiRequestModel
    {
        public string Pbi_Name { get; set; }
        public string Pbi_Description { get; set; }
        public DateTime Start_Date_Pbi { get; set; }
        public DateTime End_Date_Pbi { get; set; }
        public PbiType pbi_type { get; set; }
        public int Id { get; set; }
        public int Sprint_Id { get; set; }
    }
}
