namespace Application.Services;

public interface IPaymentGatewayService
{
    Task<ProcessPaymentResponseDTO> ProcessAsync(ProcessPaymentCommand request);

    Task<PaymentsMethodsDTO> GetPaymentMethodsByBinAsync(string bin);
}

