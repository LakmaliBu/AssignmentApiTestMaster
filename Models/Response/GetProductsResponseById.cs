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
        public string CPUModel { get; set; }
        public string HardDiskSize { get; set; }
    }




}
