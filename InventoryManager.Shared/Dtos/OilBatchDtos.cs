using System;
using System.ComponentModel.DataAnnotations;
using InventoryManager.Shared;

namespace InventoryManager.Shared.Dtos
{
    // Returned to clients
    public class OilBatchDto
    {
        public int BatchId { get; set; }
        public int OilId { get; set; }
        public string OilName { get; set; } = null!;
        public int ProviderId { get; set; }
        public string ProviderName { get; set; } = null!;
        public string CountryOfOrigin { get; set; } = null!;
        public string? LotNumber { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal ArrivalWeight_Oz { get; set; }
        public decimal CurrentWeight_Oz { get; set; }
        public decimal? Cost_USD { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? StorageLocation { get; set; }
        public BatchStatus Status { get; set; }
        public string? COAFilePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    // Accepted when a new shipment arrives. CurrentWeight_Oz starts equal to ArrivalWeight_Oz.
    public class CreateOilBatchDto
    {
        [Range(1, int.MaxValue)]
        public int OilId { get; set; }

        [Range(1, int.MaxValue)]
        public int ProviderId { get; set; }

        [Required, MaxLength(100)]
        public string CountryOfOrigin { get; set; } = null!;

        [MaxLength(100)]
        public string? LotNumber { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Range(typeof(decimal), "0.001", "9999999.999")]
        public decimal ArrivalWeight_Oz { get; set; }

        [Range(typeof(decimal), "0", "99999999.99")]
        public decimal? Cost_USD { get; set; }

        public DateTime? ExpirationDate { get; set; }

        [MaxLength(100)]
        public string? StorageLocation { get; set; }

        [MaxLength(300)]
        public string? COAFilePath { get; set; }
    }

    // PUT: replaces the mutable fields of an existing batch (weight, status, storage, etc.)
    public class UpdateOilBatchDto
    {
        [Range(typeof(decimal), "0", "9999999.999")]
        public decimal CurrentWeight_Oz { get; set; }

        public BatchStatus Status { get; set; } = BatchStatus.Active;

        public DateTime? ExpirationDate { get; set; }

        [MaxLength(100)]
        public string? StorageLocation { get; set; }

        [MaxLength(300)]
        public string? COAFilePath { get; set; }
    }
}
