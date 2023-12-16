using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class PaymentResponse
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Reference { get; set; }
        public string AuthorizationCode { get; set; }
        public string Nsu { get; set; }
        public string ReasonCode { get; set; }
    }
}