using RespApi.Dtos;
using RespApi.Models;
using RestApi.Infrasctructure.Soap.SoapContracts;
using RestApi.Infrastructure.Soap;

namespace RespApi.Mappers;

public static class UserMapper{
    public static UserModel ToDomain(this UserResponseDto user){
        if (user is null)
        {
           return null; 
        }
        return new UserModel{
            Id = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDay = user.BirthDate,
            Email = user.Email
        };
    }

    public static UserResponse ToDto(this UserModel user){
        if(user == null){
            return null;
        }
        return new UserResponse{
            Id = user.Id,
            Email = user.Email,
            Name = user.FirstName + " " + user.LastName,
            BirthDate = user.BirthDay
        };
    }

    public static List<UserResponse>ToDto(this IList<UserModel> users){
        if(users == null){
            return null;
        }
        return users.Select(user => user.ToDto()).ToList();
    }
}