using RestApi.Models;

namespace RestApi.Repositories;

public interface IGroupRepository{
    Task <GroupModel> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task <GroupModel> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task <IList<GroupModel>> GetAllByNameAsync(string name,int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken);
    Task DeleteByIdAsync(string id, CancellationToken cancellationToken);
    Task <GroupModel> CreateAsync(string name, Guid [] Users, CancellationToken cancellationToken);
    Task UpdateGroupAsync(string id, string name, Guid[] users, CancellationToken cancellationToken);

}