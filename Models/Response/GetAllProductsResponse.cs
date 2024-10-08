
namespace AssignmentApiTestMaster.Models.Response
{

    using System.Collections.Generic;

    public class GetAllProductsResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProductData Data { get; set; }
    }

    public class ProductData
    {
        public string Color { get; set; }
        public string Capacity { get; set; }
        public int? CapacityGB { get; set; }
        public double? Price { get; set; }
        public string Generation { get; set; }
        public int? Year { get; set; }
        public string CPUModel { get; set; }
        public string HardDiskSize { get; set; }
        public string StrapColour { get; set; }
        public string CaseSize { get; set; }
        public string Description { get; set; }
        public double? ScreenSize { get; set; }
    }

}
