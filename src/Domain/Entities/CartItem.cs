namespace Domain.Entities;

public record CartItem(
    Guid Id, 
    int Quantity, 
    decimal Price, 
    string Title, 
    string Description,
    string PictureUrl,
    string CategoryId,
    float UnitPrice);