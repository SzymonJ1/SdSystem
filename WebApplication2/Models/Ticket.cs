using System;
using System.ComponentModel.DataAnnotations;

namespace SdSystem.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public TicketStatus Status { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public int? AssignedToId { get; set; }
        public User AssignedTo { get; set; }
    }

    public enum TicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
}
