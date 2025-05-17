namespace Application.Services;

public interface IPaymentGatewayService
{
    Task<ProcessPaymentResponseDTO> ProcessAsync(ProcessPaymentCommand request);

    Task<ProcessPaymentResponseDTO> GetPaymentAsync(long paymentId);

    Task<PaymentsMethodsDTO> GetPaymentMethodsByBinAsync(string bin);
}

