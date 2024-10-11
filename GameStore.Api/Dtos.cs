using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
);

public record CreateGameDto(
    [Required][StringLength(100)] string Name,
    [Required][StringLength(50)] string Genre,
    [Range(0, 150)] decimal Price,
    DateTime ReleaseDate,
    [Url][StringLength(100)] string ImageUri
);

public record UpdateGameDto(
    [Required][StringLength(100)] string Name,
    [Required][StringLength(50)] string Genre,
    [Range(0, 150)] decimal Price,
    DateTime ReleaseDate,
    [Url][StringLength(100)] string ImageUri
);