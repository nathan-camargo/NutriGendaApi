using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Nutritionists")]
public class Nutritionist
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string Email { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string Password { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string Crn { get; set; }
}
