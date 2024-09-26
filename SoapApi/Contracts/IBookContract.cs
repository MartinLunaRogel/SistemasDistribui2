using System.ServiceModel;
using SoapApi.Dtos;

namespace SoapApi.Contracts;

[ServiceContract]
public interface IBookContract
{
    [OperationContract]
    Task<BookResponseDto> GetBookById(Guid bookId, CancellationToken cancellationToken);
}
