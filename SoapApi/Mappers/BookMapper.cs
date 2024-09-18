using SoapApi.Dtos;
using SoapApi.Infrastructure.Entities;
using SoapApi.Models;

namespace SoapApi.Mappers;

public static class BookMapper
{
    public static BookModel ToModel(this BookEntity book)
    {
        if (book is null)
        {
            return null;
        }
        return new BookModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublishedDate = book.PublishedDate
        };
    }

    public static BookResponseDto ToDto(this BookModel book)
    {
        return new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublishedDate = book.PublishedDate
        };
    }

    public static BookEntity ToEntity(this BookModel book)
    {
        return new BookEntity
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            PublishedDate = book.PublishedDate
        };
    }

    public static BookModel ToModel(this BookCreateRequestDto book)
    {
        return new BookModel
        {
            Title = book.Title,
            Author = book.Author,
            PublishedDate = book.PublishedDate
        };
    }
}
