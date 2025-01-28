using System.ComponentModel.DataAnnotations;
namespace SDsystem.Entities

{
    public class TicketEntity
    {
        public int Id { get; set; }
        [Display(Name = "Tytuł")]
        public string Subject { get; set; }
        [Display(Name = "Opis")]
        public string? Description { get; set; }
        [Display(Name = "Imię")]
        public string Name { get; set; }
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }
        [Display(Name = "Dział")]
        public string Department { get; set; }
        public string Status { get; set; } = "Aktywne";
        [Display(Name = "Data")]
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
