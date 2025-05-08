using Domain.Entities;

namespace Application.Services;

public interface ICheckoutRepository
{
    public Task<CheckoutResponsePayload> ProcessPayment(CheckoutPayload checkout);
}

