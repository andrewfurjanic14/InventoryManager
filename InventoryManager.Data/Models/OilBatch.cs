using System;
using System.Collections.Generic;
using System.Text;
using InventoryManager.Shared;

namespace InventoryManager.Data.Models
{
    public class OilBatch
    {
        public int BatchId { get; set; }

        public int OilId { get; set; }
        public Oil Oil { get; set; } = null!;

        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;

        public string CountryOfOrigin { get; set; } = null!;

        // Supplier's own lot/batch code; for traceability
        public string? LotNumber { get; set; }

        public DateTime ArrivalDate { get; set; }

        public decimal ArrivalWeight_Oz { get; set; }

        public decimal CurrentWeight_Oz { get; set; }

        public decimal? Cost_USD { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public string? StorageLocation { get; set; }

        public BatchStatus Status { get; set; } = BatchStatus.Active;

        // Link to Certificate of Analysis / GC-MS report
        public string? COAFilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
