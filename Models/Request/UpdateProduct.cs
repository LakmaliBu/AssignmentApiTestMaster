using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApiTestMaster.Models.Request
{
    public class UpdateProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProductDataUpdate Data { get; set; }
    }

    public class ProductDataUpdate
    {
        public int Year { get; set; }
        public double Price { get; set; }

        [JsonProperty("CPU model")] // Mapping to the property with a space in JSON
        public string CPUModel { get; set; }

        [JsonProperty("Hard disk size")] // Mapping to the property with a space in JSON
        public string HardDiskSize { get; set; }

        public string Color { get; set; }
    }
}