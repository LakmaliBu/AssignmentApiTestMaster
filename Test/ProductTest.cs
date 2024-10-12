using AssignmentApiTestMaster.Base;
using AssignmentApiTestMaster.Config;
using AssignmentApiTestMaster.Models.Request;


namespace AssignmentApiTestMaster.Test
{
    public class ProductTest
    {
        ProductApi getApi = new ProductApi();
        private readonly HttpClient _httpClient;
        public static string filePath = "C:\\Users\\Admin\\source\\repos\\AssignmentApiTestMaster\\config.properties";
        PropertyFileReader propertyFileReader = new PropertyFileReader(filePath);
       
        //Main workflow scenarios(positive)
        
        [Fact]
        public async void VerifyGetAllProductInfoWithValidData()
        {
            
          
            var result = await getApi.GetAllProducts($"/objects");

            // Assert that products are not null
            Assert.NotNull(result);
            // Assert product 1 data
            Assert.Equal("Google Pixel 6 Pro", result[0].Name);
            Assert.Equal("Cloudy White", result[0].Data.Color);
            Assert.Equal("128 GB", result[0].Data.Capacity);

            // Assert product 2 data
            Assert.Equal("Apple iPhone 12 Mini, 256GB, Blue", result[1].Name);

            //Assert product 3 data
            Assert.Equal("Apple iPhone 12 Pro Max", result[2].Name);
            Assert.Equal("Cloudy White", result[2].Data.Color);
            await Task.Delay(1000);
        }
        
         [Fact]
         public async Task AddProduct_ReturnsUpdatedProduct_WhenProductIsUpdatedSuccessfully()
         {

             var addedProduct = new AddProduct
             {
                 Name = "Samsung Galaxy S22",
                 Data = new ProductDataAdd
                 {
                     Year = 2000,
                     Price = 799.99,
                     CPUModel = "Core 001",
                     HardDiskSize = "1TB"

                 }
             };

             var result = await getApi.AddProduct(addedProduct, "/objects");

             Assert.NotNull(result);

             Assert.Equal("Samsung Galaxy S22", result.Name);
             Assert.Equal(2000, result.Data.Year);
             Assert.Equal(799.99, result.Data.Price);
             Assert.Equal("Core 001", result.Data.CPUModel);
             Assert.Equal("1TB", result.Data.HardDiskSize);
             propertyFileReader.WriteToConfigFile("ProductId", result.Id, filePath);
             await Task.Delay(2000);
        }
       
         [Fact]
         public async void VerifyGetProductInfoByIdWithValidData()
         {
             String productIdGet = propertyFileReader.GetProperty("ProductId");
             var result = await getApi.GetProductById($"/objects/{productIdGet}");

             // Assert status code
             Assert.NotNull(result);
             Assert.Equal("Samsung Galaxy S22", result.Name);
             Assert.Equal(2000, result.Data.Year);
             Assert.Equal(799.99, result.Data.Price);
             Assert.Equal("Core 001", result.Data.CPUModel);
             Assert.Equal("1TB", result.Data.HardDiskSize);
         }

        
        [Fact]
        public async Task UpdateProduct_ReturnsUpdatedProduct_WhenProductIsUpdatedSuccessfully()
        {
           
            var updatedProduct = new UpdateProduct
            {
                Name = "Apple MacBook Pro 16",
                Data = new ProductDataUpdate
                {
                    Year = 2024,
                    Price = 2049.99,
                    CPUModel = "Intel Core i9",
                    HardDiskSize = "1 TB",
                    Color = "Black"
                }
            };


            String productIdUpdate = propertyFileReader.GetProperty("ProductId");
        
            var result = await getApi.UpdateProduct(updatedProduct, $"/objects/{productIdUpdate}");
         
            Assert.NotNull(result);
            Assert.Equal("Apple MacBook Pro 16", result.Name);
            Assert.Equal(2024, result.Data.Year);
            Assert.Equal(2049.99, result.Data.Price);
            Assert.Equal("Intel Core i9", result.Data.CPUModel);
            Assert.Equal("1 TB", result.Data.HardDiskSize);
            Assert.Equal("Black", result.Data.Color);
            propertyFileReader.WriteToConfigFile("UpdatedProductId", result.Id, filePath);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsSuccessMessage()
        {
            String productIdDelete = propertyFileReader.GetProperty("UpdatedProductId");
            var expectedMessage = $"Object with id = {productIdDelete} has been deleted.";
            var responseBody = new { message = expectedMessage };

            var result = await getApi.DeleteProduct($"/objects/{productIdDelete}");
            //Assert response message
            Assert.Contains(expectedMessage, result);

        }

        
        
        // Negative sceanrios related to main workflow

        [Fact]
        public async void VerifyGetProductInfoByIdWithInValidData()
        {
            var result = await getApi.GetProductById("/objects/107");
            // Assert response      
            Assert.Equal(null, result.Name);
            Assert.Equal(null, result.Data);
   
        }



        [Fact]
        public async Task UpdateProduct_ReturnsFalse_WhenUpdateFails()
        {

            var updatedProduct = new UpdateProduct
            {

            };

            var response = await getApi.UpdateProduct(updatedProduct, "/objects/ff808181923ed5e2019276f6542f738d");
            // Assert response
            Assert.Equal(null, updatedProduct.Name);

        }


        [Fact]
        public async Task AddProduct_ReturnsTrue_WhenProductAddWithEmptyRequestBody()
        {

            var newProduct = new AddProduct
            {

            };


            var result = await getApi.AddProduct(newProduct, "/objects");
            //Assert response
            Assert.Equal(null, newProduct.Data);

        }
       
        

        [Fact]
        public async Task DeleteProduct_NonExistProduct()
        {

            var expectedMessage = "Failed to delete product. Status code:";
          
            var responseBody = new { message = expectedMessage };
            var result = await getApi.DeleteProduct("/objects/abc");
            //Assert response message
            Assert.Contains(expectedMessage, result);

        } 

        
    }
}
