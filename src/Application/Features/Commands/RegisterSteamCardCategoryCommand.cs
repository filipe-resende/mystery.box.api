namespace Application.Features.Commands;

public class RegisterSteamCardCategoryCommand : IRequest<Guid>
{
    public string Name { get; set; }
    [Required]
    public float Price { get; set; }
    [Required]
    public string Thumb { get; set; }
    [Required]
    public string Description { get; set; }
}
