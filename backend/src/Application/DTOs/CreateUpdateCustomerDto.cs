// Para Create/Update de Cliente
namespace Application.DTOs;
public class CreateUpdateCustomerDto {
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
}