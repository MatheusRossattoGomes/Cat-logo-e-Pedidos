// Para os Detalhes do Cliente
namespace Application.DTOs;
public class CustomerDetailsDto {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}