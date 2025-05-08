namespace Domain.Entities;

public class Phone
{
    [Key]
    public Guid Id { get; set; }
    public string Country { get; set; }
    public string Area { get; set; }
    public string Number { get; set; }
}
