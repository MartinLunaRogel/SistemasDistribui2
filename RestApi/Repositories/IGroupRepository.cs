using RestApi.Models;

namespace RestApi.Repositories;

public interface IGroupRepository{
    Task <GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken);  
    Task <IList<GroupModel>> GetGroupsByNameAsync(string name,int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken);
    Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
    Task <GroupModel> CreateAsync(string name, Guid [] Users, CancellationToken cancellationToken);
}