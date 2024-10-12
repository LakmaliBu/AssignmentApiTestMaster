
namespace AssignmentApiTestMaster.Models.Response
{
  using Newtonsoft.Json;
  

    public class GetAllProductsResponse
{
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public ProductData Data { get; set; }
}

public class ProductData
{
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("capacity")]
        public string Capacity { get; set; }

    [JsonProperty("capacity GB")]
    public int? CapacityGB { get; set; }  // Maps "capacity GB" in JSON

        [JsonProperty("price")]
        public double? Price { get; set; }
        [JsonProperty("generation")]
        public string Generation { get; set; }
        [JsonProperty("year")]
        public int? Year { get; set; }

    [JsonProperty("CPU model")]
    public string CPUModel { get; set; }  // Maps "CPU model" in JSON

    [JsonProperty("Hard disk size")]
    public string HardDiskSize { get; set; }  // Maps "Hard disk size" in JSON

    [JsonProperty("Strap Colour")]
    public string StrapColour { get; set; }  // Maps "Strap Colour" in JSON

    [JsonProperty("Case Size")]
    public string CaseSize { get; set; }  // Maps "Case Size" in JSON
        [JsonProperty("description")]
        public string Description { get; set; }

    [JsonProperty("Screen size")]
    public double? ScreenSize { get; set; }  // Maps "Screen size" in JSON
}
}
