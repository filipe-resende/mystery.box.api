
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; }
        public string ReferenceId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PaidAt { get; set; }
        public string Description { get; set; }
        public int AmountValue { get; set; }
        public string AmountCurrency { get; set; }
        public PaymentResponse PaymentResponse { get; set; }
    }
}
