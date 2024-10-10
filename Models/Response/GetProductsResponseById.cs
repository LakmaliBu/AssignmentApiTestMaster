using Authlete.Dto;
using Newtonsoft.Json;

namespace AssignmentApiTestMaster.Models.Response
{
    public class GetProductsResponseById
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProductDataById Data { get; set; }
    }
    public class ProductDataById
    {
        public int Year { get; set; }
        public double Price { get; set; }
        [JsonProperty("CPU model")]
        public string CPUModel { get; set; }  // Maps "CPU model" in JSON
        [JsonProperty("Hard disk size")]
        public string HardDiskSize { get; set; }  // Maps "Hard disk size" in JSON
    }

}
