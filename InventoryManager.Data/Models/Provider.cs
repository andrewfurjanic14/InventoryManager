using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Data.Models
{
    public class Provider
    {
        public int ProviderId { get; set; }

        public string Name { get; set; } = null!;

        public string? ContactEmail { get; set; }

        public string? ContactPhone { get; set; }

        public string? Website { get; set; }

        // Navigation
        public ICollection<OilBatch> Batches { get; set; } = new List<OilBatch>();
    }
}
