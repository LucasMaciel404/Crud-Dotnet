using System.ComponentModel.DataAnnotations;

namespace Crud.Model;
public class UserModel
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public bool Ativo { get; set; }
}

