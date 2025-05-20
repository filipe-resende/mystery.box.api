namespace Domain.Interfaces.Repositories;

public interface IPaymentRepository : IRepository<Payment>
{
    public Task<Payment?> GetByMercadoPagoIdAsync(long? mercadoPagoId);
}