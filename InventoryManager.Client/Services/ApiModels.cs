using System;
using System.Collections.Generic;

namespace InventoryManager.Client.Services
{
    public enum BatchStatus { Active, Inactive }

    public class OilDto
    {
        public int OilId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class SaveOilDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class ProviderDto
    {
        public int ProviderId { get; set; }
        public string? Name { get; set; }
    }

    public class SaveProviderDto
    {
        public string? Name { get; set; }
    }

    public class OilBatchDto
    {
        public int BatchId { get; set; }
        public int OilId { get; set; }
        public string? BatchNumber { get; set; }
        public BatchStatus Status { get; set; }
    }

    public class CreateOilBatchDto
    {
        public int OilId { get; set; }
        public string? BatchNumber { get; set; }
    }

    public class UpdateOilBatchDto
    {
        public int OilId { get; set; }
        public string? BatchNumber { get; set; }
        public BatchStatus Status { get; set; }
    }
}
