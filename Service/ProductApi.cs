using AssignmentApiTestMaster.Config;
using AssignmentApiTestMaster.Models.Request;
using AssignmentApiTestMaster.Models.Response;
using AssignmentApiTestMaster.Utilities;
using log4net;
using Newtonsoft.Json;
using System.Text;


namespace AssignmentApiTestMaster.Base
{
    public class ProductApi
    {
        
        private HttpClient restClient = new HttpClient();
        ApiHandler apiHandler = new ApiHandler();
       
        public async Task<List<T>> GetAllProducts<T>()
        {
            var response = await restClient.GetAsync(apiHandler.BuildUrl("/objects"));
            var context = await response.Content.ReadAsStringAsync();

            try
            {
                var responseModel = JsonConvert.DeserializeObject<List<T>>(context);
                return responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
                Logger.LogError("Error while getting all product data" + ex);

            }

        }

        public async Task<T> GetProductById<T>()
        {

            var response = await restClient.GetAsync(apiHandler.BuildUrl("/objects/7"));
            var context = await response.Content.ReadAsStringAsync();

            try
            {
                var responseModel = JsonConvert.DeserializeObject<T>(context);
                return responseModel;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error while getting product data by Id"+ ex);
                return default(T);
            }

        }

        public async Task<bool> AddProduct(AddProduct newProduct)
        {
            var requestUrl = apiHandler.BuildUrl("/objects");

            try
            {
             
                var jsonProduct = JsonConvert.SerializeObject(newProduct);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

                var response = await restClient.PostAsync(requestUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError("Error while adding product" + ex);
                return false; 
            }
        }

        public async Task<bool> UpdateProduct(int productId, UpdateProduct updatedProduct)
        {
            var requestUrl = apiHandler.BuildUrl("/objects/7");

            try
            {
                var jsonProduct = JsonConvert.SerializeObject(updatedProduct);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

               
                var response = await restClient.PutAsync(requestUrl, content);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError("Error while updating product" + ex);
                return false; 
            }

        }


        public async Task<string> DeleteProduct(int productId)
        {
            var requestUrl = apiHandler.BuildUrl("/objects/${productId}");

            try
            {
                var response = await restClient.DeleteAsync(requestUrl);

                // Check if the response was successful (status code)
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseMessage = JsonConvert.DeserializeObject<DeleteResponse>(responseContent);
                    return responseMessage.Message;
                }
                else
                {
                    return $"Failed to delete product with ID {productId}. Status code: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError("Error while deleting prodcut" + ex);
                return $"{productId}: {ex.Message}";
            }
        }
    }



}
