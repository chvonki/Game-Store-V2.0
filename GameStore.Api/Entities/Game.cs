using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Название не должно быть пустым")]
    [StringLength(100, ErrorMessage = "Название должно содержать от 1 до 100 символов")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Жанр не должен быть пустым")]
    [StringLength(50)]
    public required string Genre { get; set; }

    [Range(0, 150, ErrorMessage = "Цена должна быть в диапазоне от 0 до 150 $")]
    public decimal Price { get; set; }

    public DateTime ReleaseDate { get; set; }

    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }
}