using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NutriGendaApi.Source.Models
{
    [Table("Weeks")]
    public class Week
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Diet")]
        public Guid DietId { get; set; }
        public Diet? Diet { get; set; }

        [Required]
        public int WeekNumber { get; set; }

        public virtual ICollection<Meal> Meals { get; set; } = new List<Meal>();
    }

}
