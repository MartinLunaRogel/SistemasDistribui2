using System.Runtime.Serialization;

namespace SoapApi.Dtos;

[DataContract]
public class BookCreateRequestDto
{
    [DataMember]
    public string Title { get; set; } = null!;

    [DataMember]
    public string Author { get; set; } = null!;

    [DataMember]
    public DateTime PublishedDate { get; set; }
}
