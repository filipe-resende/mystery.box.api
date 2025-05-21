namespace Application.Mappings;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<PaymentItem, SteamCardCategory>();

        CreateMap<PurchaseHistory, PurchaseHistoryResponseDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.SteamCardCategory.Title))
            .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.Payment.CreatedAt))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Payment.Status))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.HasValue ? (int)src.Quantity : 1))
            .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice ?? 0));

        CreateMap<PaymentItem, PurchaseHistory>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => SteamCardStatus.Pending))
            .ForMember(dest => dest.SteamCardCategoryId, opt => opt.MapFrom(src => Guid.Parse(src.Id))) 
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice));

        CreateMap<MercadoPago.Resource.Payment.Payment, Payment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.MercadoPagoPaymentId, opt => opt.MapFrom(src => src.Id ?? 0))
                .ForMember(dest => dest.ExternalReference, opt => opt.MapFrom(src => src.ExternalReference))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.StatusDetail, opt => opt.MapFrom(src => src.StatusDetail))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.ApprovedAt, opt => opt.MapFrom(src => src.DateApproved))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.MoneyReleaseDate))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.TransactionAmount ?? 0))
                .ForMember(dest => dest.NetAmount, opt => opt.MapFrom(src => src.TransactionDetails.NetReceivedAmount ?? 0))
                .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.Installments ?? 0))
                .ForMember(dest => dest.PaymentMethodId, opt => opt.MapFrom(src => src.PaymentMethodId))
                .ForMember(dest => dest.PaymentTypeId, opt => opt.MapFrom(src => src.PaymentTypeId))
                .ForMember(dest => dest.CardLastFour, opt => opt.MapFrom(src => src.Card.LastFourDigits))
                .ForMember(dest => dest.CardFirstSix, opt => opt.MapFrom(src => src.Card.FirstSixDigits))
                .ForMember(dest => dest.CardHolderName, opt => opt.MapFrom(src => src.Card.Cardholder.Name))
                .ForMember(dest => dest.PayerEmail, opt => opt.MapFrom(src => src.Payer.Email))
                .ForMember(dest => dest.PayerDocumentType, opt => opt.MapFrom(src => src.Payer.Identification.Type))
                .ForMember(dest => dest.PayerDocumentNumber, opt => opt.MapFrom(src => src.Payer.Identification.Number))
                .ForMember(dest => dest.PurchaseHistories, opt => opt.MapFrom(src => src.AdditionalInfo.Items))
                .ForMember(dest => dest.FullResponseJson, opt => opt.MapFrom(src =>
                    System.Text.Json.JsonSerializer.Serialize(src, new JsonSerializerOptions
                    {
                        Converters = { new JsonStringEnumConverter() },
                        WriteIndented = false,
                        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals 
                    })
                ));

    }
}

