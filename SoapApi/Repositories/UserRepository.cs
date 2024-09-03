using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SoapApi.Infrastructure;
using SoapApi.Mappers;
using SoapApi.Models;

namespace SoapApi.Repositories;

public class UserRespository : IUserRepository{

    private readonly RelationalDbContext _dbContext;
    public UserRespository(RelationalDbContext dbContext){


        _dbContext = dbContext;

    }


    public async Task<UserModel> GetByIdAsync(Guid id, CancellationToken cancellationToken) {


        var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        
        return user.ToModel();
    }
    public async Task<IList<UserModel>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.AsNoTracking().ToListAsync(cancellationToken);
        return users.Select(user => user.ToModel()).ToList();
    }
    public async Task<IList<UserModel>> GetAllByEmail(string email, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.AsNoTracking()
                    .Where(user => user.Email.Contains(email))
                    .ToListAsync(cancellationToken);
        
        return users.Select(user => user.ToModel()).ToList();
    }

}