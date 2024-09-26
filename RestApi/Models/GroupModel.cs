namespace RestApi.Models;

public class GroupModel{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set;}
    public Guid[] Users { get; set; }
}