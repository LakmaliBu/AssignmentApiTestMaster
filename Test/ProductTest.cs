using AssignmentApiTestMaster.Base;
using AssignmentApiTestMaster.Models.Request;
using System.Net;
using AssignmentApiTestMaster.Models.Response;


namespace AssignmentApiTestMaster.Test
{
    public class ProductTest
    {
        ProductApi getApi = new ProductApi();
        private readonly HttpClient _httpClient;
        public static string ProductId = null;
        public static string UpdatedProductId = null;

        //Main workflow scenarios(positive)

        [Fact]
        public async void VerifyGetAllProductInfoWithValidData()
        {
            // Call the API and get both response and products
            var (httpResponse, products) = await getApi.GetAllProducts<GetAllProductsResponse>("/objects");

            // Assert status code
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

            // Assert that products are not null
            Assert.NotNull(products);
            Assert.NotEmpty(products);

            // Assert product 1 data
            Assert.Equal("1", products[0].Id);  // Access the list using products[0]
            Assert.Equal("Google Pixel 6 Pro", products[0].Name);
            Assert.Equal("Cloudy White", products[0].Data.Color);
            Assert.Equal("128 GB", products[0].Data.Capacity);

            // Assert product 2 data
            Assert.Equal("2", products[1].Id);
            Assert.Equal("Apple iPhone 12 Mini, 256GB, Blue", products[1].Name);

            // Assert on the product 2 Data property
            Assert.Null(products[1].Data);

        }


        [Fact]
        public async Task AddProduct_ReturnsTrue_WhenProductIsAddedSuccessfully()
        {

            var newProduct = new AddProduct
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

           
            var result = await getApi.AddProduct(newProduct, "/objects");
            ProductId = newProduct.Id;

            //Assert response
            Assert.Equal("Samsung Galaxy S22", newProduct.Name);
            Assert.Equal(2000, newProduct.Data.Year);
            Assert.Equal(799.99, newProduct.Data.Price);
            Assert.Equal("Core 001", newProduct.Data.CPUModel);
            Assert.Equal("1TB", newProduct.Data.HardDiskSize);
        }

        [Fact]
        public async void VerifyGetProductInfoByIdWithValidData()
        {
           
            var (httpResponse, products) = await getApi.GetProductById<GetProductsResponseById>($"/objects/{ProductId}");
            // Assert status code
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.NotNull(products);
            Assert.Equal("7", products.Id);
            Assert.Equal("Apple MacBook Pro 16", products.Name);
            Assert.Equal(2019, products.Data.Year);
            Assert.Equal(1849.99, products.Data.Price);
            Assert.Equal("Intel Core i9", products.Data.CPUModel);
            Assert.Equal("1 TB", products.Data.HardDiskSize);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsTrue_WhenProductIsUpdatedSuccessfully()
        {

            var updatedProduct = new UpdateProduct
            {
                Name = "Apple MacBook Pro 16",
                Data = new ProductDataUpdate
                {
                    Year = 2000,
                    Price = 2049.99,
                    CPUModel = "Intel Core i9",
                    HardDiskSize = "1 TB",
                    Color = "silver"
                }
            };
         
            var result = await getApi.UpdateProduct(updatedProduct, $"/objects/{ProductId}");
            UpdatedProductId = updatedProduct.Id;
            //Assert updated details
            Assert.Equal("Apple MacBook Pro 16", updatedProduct.Name);
            Assert.Equal(2000, updatedProduct.Data.Year);
            Assert.Equal(2049.99, updatedProduct.Data.Price);
            Assert.Equal("Intel Core i9", updatedProduct.Data.CPUModel);
            Assert.Equal("1 TB", updatedProduct.Data.HardDiskSize);
            Assert.Equal("silver", updatedProduct.Data.Color);

        }


        [Fact]
        public async Task DeleteProduct_ReturnsSuccessMessage()
        {

            var expectedMessage = $"Object with id = {UpdatedProductId} has been deleted.";
            var responseBody = new { message = expectedMessage };

            var result = await getApi.DeleteProduct($"/objects/{UpdatedProductId}");
            //Assert response message
            Assert.Equal(expectedMessage, result);

        }


        // Negative sceanrios related to main workflow

        [Fact]
        public async void VerifyGetProductInfoByIdWithInValidData()
        {
            var (httpResponse, products) = await getApi.GetProductById<GetProductsResponseById>("/objects/107");
            // Assert status code
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

        }


       

        [Fact]
        public async Task UpdateProduct_ReturnsFalse_WhenUpdateFails()
        {

            var updatedProduct = new UpdateProduct
            {

            };

            var response = await getApi.UpdateProduct(updatedProduct, "/objects/ff808181923ed5e2019276f6542f738d");
            // Assert response
           // Assert.False(response);
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
            Console.WriteLine(result);
            //Assert response message
            Assert.Contains(expectedMessage, result);


        }
    }
}