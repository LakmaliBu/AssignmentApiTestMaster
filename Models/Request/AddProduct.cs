using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApiTestMaster.Models.Request
{

    public class AddProduct
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProductDataAdd Data { get; set; }
    }

    public class ProductDataAdd
    {
        public int Year { get; set; }
        public double Price { get; set; }
        public string CPUModel { get; set; }
        public string HardDiskSize { get; set; }
    }



}
