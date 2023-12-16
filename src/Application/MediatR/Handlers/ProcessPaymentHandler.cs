using Application.MediatR.Commands;
using Domain.DTO;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.MediatR.Handlers
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, ProcessPaymentResponseDTO>
    {
        private readonly ICheckoutRepository checkout;
        private readonly IGetUserFromToken getUserFromToken;

        public ProcessPaymentHandler(ICheckoutRepository checkout, IGetUserFromToken getUserFromToken)
        {
            this.checkout = checkout;
            this.getUserFromToken = getUserFromToken;
        }

        public async Task<ProcessPaymentResponseDTO> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var user = await getUserFromToken.GetUserIdFromToken();

            //var checkout = new Checkout() 
            //{
            //    new Customer()
            //    {
            //        name = user.Name,
            //        email = user.Email,

            //    }
            //};


            //var repository = await checkoutRepository.ProcessPayment(checkout); 
            return new ProcessPaymentResponseDTO();
        }
    }
}
