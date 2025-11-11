using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class PaymentStatus
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Key]
    public int PaymentStatusId { get; set; }
    public string PaymentStatusName { get; set; } = string.Empty;

    public virtual List<Ticket> Tickets { get; set; } = new();
}
