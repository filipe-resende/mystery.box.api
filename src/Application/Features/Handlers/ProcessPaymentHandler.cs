namespace Application.Features.Handlers;

public class ProcessPaymentHandler(ICheckoutRepository checkout, IGetUserFromToken getUserFromToken) : IRequestHandler<ProcessPaymentCommand, ProcessPaymentResponseDTO>
{
    private readonly ICheckoutRepository checkout = checkout;
    private readonly IGetUserFromToken getUserFromToken = getUserFromToken;

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

