using Domain.DTO;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.MediatR.Commands
{
    public class ProcessPaymentCommand : IRequest<ProcessPaymentResponseDTO>
    {
        [Required]
        public IEnumerable<ItemDTO> Item { get; set; }
        [Required]
        public CreditCardDTO Card { get; set; }
    }
}