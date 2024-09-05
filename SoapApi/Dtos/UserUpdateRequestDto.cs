using System.Data;
using System.Runtime.Serialization;

namespace SoapApi.Dtos;

[DataContract]

public class UserUpdateRequestDto{
    [DataMember]
    public String FirstName {get; set; } = null!;
    [DataMember]
    public String LastName {get; set; } = null!;
    [DataMember]
    public DateTime Birthday { get; set; }
}

