namespace Infraestructure.Repository;

public class PaymentRepository(Context dbContext) : Repository<Payment>(dbContext), IPaymentRepository
{
    public async Task<Payment?> GetByMercadoPagoIdAsync(long? mercadoPagoId)
    {
        return await dbContext.Set<Payment>()
            .FirstOrDefaultAsync(p => p.MercadoPagoPaymentId == mercadoPagoId);
    }
}