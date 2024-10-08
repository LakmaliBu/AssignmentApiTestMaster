using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApiTestMaster.Models.Request
{
    public class UpdateProduct
    {
        public string Name { get; set; }
        public ProductDataUpdate Data { get; set; }
    }

    public class ProductDataUpdate
    {
        public int Year { get; set; }
        public double Price { get; set; }
        public string CPUModel { get; set; }
        public string HardDiskSize { get; set; }
        public string Color { get; set; }
    }


}
