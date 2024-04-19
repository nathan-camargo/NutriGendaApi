using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("HealthProfiles")]
public class HealthProfile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public decimal Height { get; set; }

    [Required]
    public decimal Weight { get; set; }

    [Column(TypeName = "text")]
    public string? Comments { get; set; }
}
