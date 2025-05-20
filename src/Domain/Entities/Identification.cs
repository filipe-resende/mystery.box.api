namespace Domain.Entities;

[ComplexType]
public class Identification
{
    [Required(ErrorMessage = "Identification type is required")]
    [StringLength(20, ErrorMessage = "Type can't be longer than 20 characters")]
    public string Type { get; set; } // Ex: "CPF", "CNPJ", etc.

    [Required(ErrorMessage = "Identification number is required")]
    [StringLength(20, ErrorMessage = "Number can't be longer than 20 characters")]
    public string Number { get; set; } // Ex: "123.456.789-00"
}
