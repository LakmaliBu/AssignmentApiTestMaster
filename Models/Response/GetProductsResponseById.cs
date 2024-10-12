using Authlete.Dto;
using Newtonsoft.Json;

namespace AssignmentApiTestMaster.Models.Response
{
    public class GetProductsResponseById
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("data")]
        public ProductDataById Data { get; set; }
    }
    public class ProductDataById
    {
        [JsonProperty("year")]
        public int Year { get; set; }
        public double Price { get; set; }
        [JsonProperty("CPU model")]
        public string CPUModel { get; set; }  
        [JsonProperty("Hard disk size")]
        public string HardDiskSize { get; set; }  
    }

}
