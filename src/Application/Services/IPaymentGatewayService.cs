namespace Application.Services;

public interface IPaymentGatewayService
{
    Task<MercadoPago.Resource.Payment.Payment> PostCreditCardAsync(PostCreditCardPaymentCommand request);
    Task<MercadoPago.Resource.Payment.Payment> PostPixAsync(PostPixPaymentCommand request);
    Task<MercadoPago.Resource.Payment.Payment> GetPaymentAsync(long paymentId);
    Task<PaymentsMethodsDTO> GetPaymentMethodsByBinAsync(string bin);
}

