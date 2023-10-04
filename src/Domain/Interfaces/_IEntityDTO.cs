namespace Domain.Interfaces
{
    public interface IEntityDTO
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        bool Active { get; set; }
    }
}
