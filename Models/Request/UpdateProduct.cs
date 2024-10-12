using Newtonsoft.Json;


namespace AssignmentApiTestMaster.Models.Request
{
    public class UpdateProduct
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("data")]
        public ProductDataUpdate Data { get; set; }
    }

    public class ProductDataUpdate
    {
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }

        [JsonProperty("CPU model")] // Mapping to the property with a space in JSON
        public string CPUModel { get; set; }

        [JsonProperty("Hard disk size")] // Mapping to the property with a space in JSON
        public string HardDiskSize { get; set; }
        [JsonProperty("color")]
        public string Color { get; set; }
    }
}