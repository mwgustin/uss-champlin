using System;
using Newtonsoft.Json;

namespace Champlin.Common
{
    [Serializable]
    public class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PartitionKey {get => "UssChamplin:" + this.GetType().Name;} 
        public string Type { get => "UssChamplin.Models." + this.GetType().Name; }
    }
}