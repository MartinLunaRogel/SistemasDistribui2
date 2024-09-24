using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using RespApi.Repositories;
using RestApi.Models;
using RestApi.Repositories;

namespace RestApi.Services;

public class GroupService : IGroupService {

    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository  _userRepositiry;
    private readonly IUserRepository userRepository;

    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepositiry = userRepository;
    }
    

    public async Task<GroupUserModel> GetGroupByIdAsync (string id, CancellationToken cancellationToken){

        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);
        if (group is null){
            return null;
        }

        return new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(
                group.Users.Select(userId => _userRepositiry.GetByIdAsync(
                    userId, cancellationToken)))).Where(UserClaimsPrincipalFactory => UserClaimsPrincipalFactory != null)
                    .ToList()
            };
    }

    public async Task<IList<GroupUserModel>> GetGroupsByNameAsync(string name, int pageNumber, int pageSize, string orderBy, CancellationToken cancellationToken){
        var groups = await _groupRepository.GetGroupsByNameAsync(name, pageNumber, pageSize, orderBy, cancellationToken);

        if(groups is null){
            return new List<GroupUserModel>();
        }

        var groupUserModels = await Task.WhenAll(groups.Select(async group => new GroupUserModel{
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(group.Users.Select(async user => await _userRepositiry.GetByIdAsync(user, cancellationToken)))).ToList()
        }));

        return groupUserModels.ToList();
    }
}