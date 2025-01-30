using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDsystem.Entities
{
    public class TicketEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Subject { get; set; }

        [Display(Name = "Opis")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Dział")]
        public string Department { get; set; }

        [Required]
        public string Status { get; set; } = "Aktywne";

        [Required]
        [Display(Name = "Data")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}