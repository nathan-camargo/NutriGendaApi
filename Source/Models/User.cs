using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Users")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Password { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public Guid NutritionistToken { get; set; }


    public virtual Nutritionist Nutritionist { get; set; }
    public virtual HealthProfile HealthProfile { get; set; }
    public virtual ICollection<Diet> Diets { get; set; } = new List<Diet>();
}
