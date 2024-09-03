using SoapApi.Models;
namespace SoapApi.Repositories;

public interface IUserRepository{
    Task<IList<UserModel>> GetAll(CancellationToken cancellationToken);
    Task<IList<UserModel>> GetAllByEmail(string email, CancellationToken cancellationToken);
    public Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken) ;

}