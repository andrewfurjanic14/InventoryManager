using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InventoryManager.Api.DTOs
{
    // Returned to clients
    public class OilDto
    {
        public int OilId { get; set; }
        public string Name { get; set; } = null!;
        public string? BotanicalName { get; set; }
        public string? ExtractionMethod { get; set; }
        public string? PlantPart { get; set; }
        public string? Notes { get; set; }

        // Sum of CurrentWeight_Oz across this oil's Active batches
        public decimal TotalCurrentWeight_Oz { get; set; }
    }

    // Used for both POST (create) and PUT (replace)
    public class SaveOilDto
    {
        [Required, MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(150)]
        public string? BotanicalName { get; set; }

        [MaxLength(100)]
        public string? ExtractionMethod { get; set; }

        [MaxLength(100)]
        public string? PlantPart { get; set; }

        public string? Notes { get; set; }
    }
}
