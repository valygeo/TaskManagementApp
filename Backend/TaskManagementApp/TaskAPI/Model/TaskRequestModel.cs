namespace TaskAPI.Model
{
    public class TaskRequestModel
    {
        public string Task_Name { get; set; }
        public string Task_Description { get; set; }
        public int Id { get; set; }
        public int Pbi_Id { get; set; }
    }
}
