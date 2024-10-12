using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApiTestMaster.Models.Request
{

    public class AddProduct
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("data")]
        public ProductDataAdd Data { get; set; }
    }

    public class ProductDataAdd
    {
        [JsonProperty("year")]
        public int Year { get; set; }
        [JsonProperty("price")]
        public double Price { get; set; }
        [JsonProperty("CPU model")]
        public string CPUModel { get; set; }
        [JsonProperty("Hard disk size")]
        public string HardDiskSize { get; set; }
    }



}
