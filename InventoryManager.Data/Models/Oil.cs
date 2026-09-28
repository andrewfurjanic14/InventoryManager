using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Data.Models
{
    public class Oil
    {
        public int OilId { get; set; }

        public string Name { get; set; } = null!;

        public string? BotanicalName { get; set; }

        public string? ExtractionMethod { get; set; }

        public string? PlantPart { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public ICollection<OilBatch> Batches { get; set; } = new List<OilBatch>();
    }
}
