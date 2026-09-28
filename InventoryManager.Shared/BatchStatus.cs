using System.Text.Json.Serialization;

namespace InventoryManager.Shared
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BatchStatus
    {
        Active,
        Depleted,
        Quarantined
    }
}
