namespace SDsystem.Entities

{
    public class TicketEntity
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string? Description { get; set; }   
        public string Name { get; set; }    
        public string Surname { get; set; }
        public string Department { get; set; }
        public string Status { get; set; } = "Aktywne";
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
