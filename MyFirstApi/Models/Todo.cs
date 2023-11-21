namespace MyFirstApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool isDone { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
