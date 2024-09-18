using RestApi.Models;

namespace RestApi.Services;

public interface IGroupService{
    Task <GroupUserModel> GetGroupByIdAsync(string id, CancellationToken cancellationToken);
    Task<IList<GroupUserModel>> GetGroupsByNameAsync(string name, CancellationToken cancellationToken);
}

