namespace RestApi.Dtos;

public class CreateGroupRequest{
    public string Name{get; set;} = null!;
    public Guid[] User {get; set;} = null!;
}