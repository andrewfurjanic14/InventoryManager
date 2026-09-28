using System.ComponentModel.DataAnnotations;

namespace InventoryManager.Shared.Dtos
{
    // Returned to clients
    public class ProviderDto
    {
        public int ProviderId { get; set; }
        public string Name { get; set; } = null!;
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Website { get; set; }
    }

    // Used for both POST (create) and PUT (replace)
    public class SaveProviderDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        [EmailAddress, MaxLength(200)]
        public string? ContactEmail { get; set; }

        [MaxLength(50)]
        public string? ContactPhone { get; set; }

        [MaxLength(200)]
        public string? Website { get; set; }
    }
}
