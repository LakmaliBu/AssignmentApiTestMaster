using AssignmentApiTestMaster.Base;
using AssignmentApiTestMaster.Models.Request;
using Moq;
using Newtonsoft.Json;
using System.Net;
using Moq.Protected;
using AssignmentApiTestMaster.Models.Response;


namespace AssignmentApiTestMaster.Test
{
    public class ProductTest
    {
        ProductApi getApi = new ProductApi();
        private readonly HttpClient _httpClient;
        public readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;


        [Fact]
        public async void VerifyGetAllProductInfoWithValidData()
        {
            var response = await getApi.GetAllProducts<GetAllProductsResponse>();
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                   
                })
                .Verifiable();

            Assert.NotNull(response);
            //Assert product1 data
            Assert.Equal("1", response[0].Id);
            Assert.Equal("Google Pixel 6 Pro", response[0].Name);
            Assert.Equal("Cloudy White", response[0].Data.Color);
            Assert.Equal("128 GB", response[0].Data.Capacity);

            //Assert product2 data
            Assert.Equal("2", response[0].Id);
            Assert.Equal("Apple iPhone 12 Mini", response[1].Name);
            Assert.Equal("Blue", response[1].Data.Color);
            Assert.Equal("256 GB", response[1].Data.Capacity);



        }

        [Fact]
        public async void VerifyGetAllProductInfoWithInvalidData()
        {
            var response = await getApi.GetAllProducts<GetAllProductsResponse>();
            _httpMessageHandlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                   "SendAsync",
                   It.IsAny<HttpRequestMessage>(),
                   It.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage
               {
                   StatusCode = HttpStatusCode.NotFound

               })
               .Verifiable();


        }

        [Fact]
        public async void VerifyGetProductInfoByIdWithValidData()
        {
            var response = await getApi.GetAllProducts<GetProductsResponseById>();
            _httpMessageHandlerMock
             .Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 It.IsAny<HttpRequestMessage>(),
                 It.IsAny<CancellationToken>()
             )
             .ReturnsAsync(new HttpResponseMessage
             {
                 StatusCode = HttpStatusCode.OK, 

             })
             .Verifiable();

            Assert.NotNull(response);
            Assert.Equal("7", response[0].Id);
            Assert.Equal("Apple MacBook Pro 16", response[0].Name);
            Assert.Equal(2019, response[0].Data.Year);
            Assert.Equal(1849.99, response[0].Data.Price);
            Assert.Equal("Intel Core i9", response[0].Data.CPUModel);
            Assert.Equal("1 TB", response[0].Data.HardDiskSize);
 }

        [Fact]
        public async void VerifyGetProductInfoByIdWithInValidData()
        {
            var response = await getApi.GetAllProducts<GetProductsResponseById>();
            _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound

            })
            .Verifiable();

        }

        [Fact]
        public async Task UpdateProduct_ReturnsTrue_WhenProductIsUpdatedSuccessfully()
        {
            var productId = 7;
            var updatedProduct = new UpdateProduct
            {
                Name = "Apple MacBook Pro 16",
                Data = new ProductDataUpdate
                {
                    Year = 2019,
                    Price = 2049.99,
                    CPUModel = "Intel Core i9",
                    HardDiskSize = "1 TB",
                    Color = "silver"
                }
            };

     
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK, 
                    Content = new StringContent(JsonConvert.SerializeObject(updatedProduct)),
                })
                .Verifiable();

            var result = await getApi.UpdateProduct(productId, updatedProduct);

            Assert.True(result); 
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), 
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task UpdateProduct_ReturnsFalse_WhenUpdateFails()
        {
         
            var productId = 0;
            var updatedProduct = new UpdateProduct
            {
                Name = "Apple MacBook Pro 16",
                Data = new ProductDataUpdate
                {
                    Year = 2019,
                    Price = 2049.99,
                    CPUModel = "Intel Core i9",
                    HardDiskSize = "1 TB",
                    Color = "silver"
                }
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest, 
                })
                .Verifiable();

          
            var result = await getApi.UpdateProduct(productId, updatedProduct);

            Assert.False(result); 
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), 
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()
            );
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

     
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Created, 
                    Content = new StringContent(JsonConvert.SerializeObject(newProduct)),
                })
                .Verifiable();

            var result = await getApi.AddProduct(newProduct);

         
            Assert.True(result); 
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), 
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task AddProduct_ReturnsFalse_WhenProductAdditionFails()
        {

            var newProduct = new AddProduct
            {};
           
           
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest, 
                })
                .Verifiable();

            var result = await getApi.AddProduct(newProduct);

            Assert.False(result); 
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()
            );
           
        }

        [Fact]
        public async Task DeleteProduct_ReturnsSuccessMessag()
        {
           
            var productId = 6;
            var expectedMessage = $"Object with id = {productId}, has been deleted.";
            var responseBody = new { message = expectedMessage };

           
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK, 
                    Content = new StringContent(JsonConvert.SerializeObject(responseBody)),
                })
                .Verifiable();

      
            var result = await getApi.DeleteProduct(productId);

            Assert.Equal(expectedMessage, result); 
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), 
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async Task DeleteProduct_ReturnsErrorMessage()
        {
          
            var productId = 6;

            
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound,
                })
                .Verifiable();

       
            var result = await getApi.DeleteProduct(productId);

         
            Assert.Contains($"Failed to delete product with ID {productId}", result);
            _httpMessageHandlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1), 
                It.IsAny<HttpRequestMessage>(),
                It.IsAny<CancellationToken>()
            );
        }
  
    }
}
