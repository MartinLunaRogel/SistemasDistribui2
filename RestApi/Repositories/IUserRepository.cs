using RespApi.Models;
namespace RespApi.Repositories;

public interface IUserRepository{
    Task<UserModel> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
}