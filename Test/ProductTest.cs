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
        public async void VerifyGetProductInfoByIdWithValidData()
        {
            var (httpResponse, products) = await getApi.GetProductById<GetProductsResponseById>("/objects/7");
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
        public async void VerifyGetProductInfoByIdWithInValidData()
        {
            var (httpResponse, products) = await getApi.GetProductById<GetProductsResponseById>("/objects/107");
            // Assert status code
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);

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

            var result = await getApi.UpdateProduct(updatedProduct, "/objects/ff808181923ed5e2019276f6542f738d");
            Assert.True(result);
            Assert.Equal("Apple MacBook Pro 16", updatedProduct.Name);
            Assert.Equal(2000, updatedProduct.Data.Year);
            Assert.Equal(2049.99, updatedProduct.Data.Price);
            Assert.Equal("Intel Core i9", updatedProduct.Data.CPUModel);
            Assert.Equal("1 TB", updatedProduct.Data.HardDiskSize);
            Assert.Equal("silver", updatedProduct.Data.Color);


        }

        [Fact]
        public async Task UpdateProduct_ReturnsFalse_WhenUpdateFails()
        {

            var updatedProduct = new UpdateProduct
            {

            };

            var response = await getApi.UpdateProduct(updatedProduct, "/objects/ff808181923ed5e2019276f6542f738d");
            // Assert status code
            Assert.True(response);
            Assert.Equal(null, updatedProduct.Name);

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

            Assert.True(result);

            Assert.Equal("Google Pixel 6 Pro", newProduct.Name);
            Assert.Equal(2000, newProduct.Data.Year);
            Assert.Equal(799.99, newProduct.Data.Price);
            Assert.Equal("Core 001", newProduct.Data.CPUModel);
            Assert.Equal("1TB", newProduct.Data.HardDiskSize);
        }

        [Fact]
        public async Task AddProduct_ReturnsTrue_WhenProductAddWithEmptyRequestBody()
        {

            var newProduct = new AddProduct
            {

            };


            var result = await getApi.AddProduct(newProduct, "/objects");

            Assert.True(result);
            Assert.Equal(null, newProduct.Data);

        }


        [Fact]
        public async Task DeleteProduct_ReturnsSuccessMessage()
        {

            var productId = "ff808181923ed5e2019277213d9073f8";
            var expectedMessage = $"Object with id = {productId} has been deleted.";
            var responseBody = new { message = expectedMessage };

            var result = await getApi.DeleteProduct("/objects/ff808181923ed5e2019277213d9073f8");
          
            Assert.Equal(expectedMessage, result);

        }

        [Fact]
        public async Task DeleteProduct_NonExistproduct()
        {

            var productId = "ff808181923ed5e2019277213d9073f8";
            var expectedMessage = $"Object with id = {productId} doesn't exist";
    
            var responseBody = new { message = expectedMessage };
            var result = await getApi.DeleteProduct("/objects/ff808181923ed5e2019277213d9073f");

            Assert.Equal(expectedMessage, result);


        }
    }
}