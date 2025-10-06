// Para a Grid de Clientes
namespace Application.DTOs;
public class CustomerGridDto {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}