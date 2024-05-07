using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NutriGendaApi.Source.Models
{
    [Table("Meals")]
    public class Meal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Week")]
        public Guid WeekId { get; set; }
        public Week? Week { get; set; }

        [Required]
        public string Name { get; set; } // Ex: "Café da Manhã", "Almoço", etc.

        public virtual ICollection<FoodItem> FoodItems { get; set; } = new List<FoodItem>();
    }
}

