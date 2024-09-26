using Amazon.SecurityToken.Model;

namespace RespApi.Models;

public class UserModel{
    public Guid Id {get; set;}
    public string FirstName {get; set;}
    public string LastName{get; set;}
    public string Email{get; set;}
    public DateTime BirthDay{get; set;}
}