namespace RestApi.Dtos;

public class CreateGroupRequest{
    public string Name{get; set;}
    public Guid[] User {get; set;}
}