using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Diets")]
public class Diet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User? User { get; set; }

    [Required]
    public int Week { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string? Information { get; set; }
}
