using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using RestApi.Repositories;
using RestApi.Models;
using RestApi.Exceptions;

namespace RestApi.Services;

public class GroupService : IGroupService {

    private readonly IGroupRepository _groupRepository;
    private readonly IUserRepository  _userRepository;
    private readonly IUserRepository userRepository;

    public GroupService(IGroupRepository groupRepository, IUserRepository userRepository)
    {
        _groupRepository = groupRepository;
        _userRepository = userRepository;
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
                group.Users.Select(userId => _userRepository.GetByIdAsync(
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
            Users = (await Task.WhenAll(group.Users.Select(async user => await _userRepository.GetByIdAsync(user, cancellationToken)))).ToList()
        }));

        return groupUserModels.ToList();
    }


    public async Task DeleteGroupByIdAsync(string id, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdAsync(id, cancellationToken);
        if(group is null){
            throw new GroupNotFoundException();
        }

        await _groupRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<GroupUserModel> CreateGroupAsync(string name, Guid[] users, CancellationToken cancellationToken )
    {
       if(users.Length==0){
        throw new InvalidGroupRequestFormatException();
       }

       var groups = await _groupRepository.GetGroupsByNameAsync(name, 1, 1, "Name", cancellationToken);
       if(groups.Any()){
        throw new GroupAlreadyExistsException();
       }

       var group= await _groupRepository.CreateAsync(name, users, cancellationToken);

       return new GroupUserModel {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(
                group.Users.Select(userId => _userRepository.GetByIdAsync(
                    userId, cancellationToken)))).Where(user => user != null)
                    .ToList()
        };
    }

    public async Task<IList<GroupUserModel>> GetGroupByExNameAsync(string name, int page, int pageS, string orderBy, CancellationToken cancellationToken)
    {
        var groups = await _groupRepository.GetByExNameAsync(name, page, pageS, orderBy,  cancellationToken);

        if (groups == null || !groups.Any())
        {
            return new List<GroupUserModel>(); 
        }

        return await Task.WhenAll(groups.Select(async group => new GroupUserModel
        {
            Id = group.Id,
            Name = group.Name,
            CreationDate = group.CreationDate,
            Users = (await Task.WhenAll(
                group.Users.Select(userId => _userRepository.GetByIdAsync(userId, cancellationToken))))
                .Where(user => user != null)
                .ToList()
        }));
    }
}