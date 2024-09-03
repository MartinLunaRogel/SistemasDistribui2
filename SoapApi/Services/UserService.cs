using System.ServiceModel;
using SoapApi.Contracts;
using SoapApi.Dtos;
using SoapApi.Mappers;
using SoapApi.Repositories;

namespace SoapApi.Services;

public class UserService : IUserContract{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository){
        _userRepository = userRepository;
    }

    public async Task<IList<UserResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAll(cancellationToken);
        return users.Select(user => user.ToDto()).ToList();
    }

    public async Task<List<UserResponseDto>> GetAllByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAllByEmail(email, cancellationToken);
        return user.Select(user => user.ToDto()).ToList();

    }

    public async Task<UserResponseDto> GetUserById(Guid userId, CancellationToken cancellationToken){
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        if(user is not null){
            return user.ToDto();
        }

        throw new FaultException("user not found");
    }
}