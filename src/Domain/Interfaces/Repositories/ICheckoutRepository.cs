
using Domain.Entities.CheckoutPayload;

namespace Domain.Interfaces.Repositories
{
    public interface ICheckoutRepository
    {
        public Task<CheckoutResponsePayload> ProcessPayment(CheckoutPayload checkout);
    }
}
