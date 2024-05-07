using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NutriGendaApi.Source.Models
{
    [Table("FoodItems")]
    public class FoodItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Meal")]
        public Guid MealId { get; set; }
        public Meal? Meal { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Description { get; set; } // Descrição do item alimentar
    }

}
