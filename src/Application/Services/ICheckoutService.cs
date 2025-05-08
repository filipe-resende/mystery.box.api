namespace Application.Services;

public interface ICheckoutService
{
    public Task<CheckoutResponsePayload> ProcessPayment(CheckoutPayload checkout);
}

