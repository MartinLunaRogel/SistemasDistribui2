using RespApi.Dtos;
using RespApi.Repositories;

namespace RestApi.Dtos;

public class GroupResponse{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public IList<UserResponse> Users {get; set;}

}