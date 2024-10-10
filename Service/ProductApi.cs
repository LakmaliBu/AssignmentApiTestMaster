using AssignmentApiTestMaster.Models.Request;
using AssignmentApiTestMaster.Models.Response;
using AssignmentApiTestMaster.Utilities;
using Newtonsoft.Json;
using System.Text;


namespace AssignmentApiTestMaster.Base
{
    public class ProductApi
    {
        
        private HttpClient restClient = new HttpClient();
        ApiHandler apiHandler = new ApiHandler();
       
        public async Task<(HttpResponseMessage, List<T>)> GetAllProducts<T>(string endpoint)
        {
            var response = await restClient.GetAsync(apiHandler.BuildUrl(endpoint));
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<T> products;

            try
            {
               products = JsonConvert.DeserializeObject<List<T>>(jsonResponse);
              

            }
            catch (Exception ex)
            {
                throw ex;
                Logger.LogError("Response is empty" + ex);
              
            }

            return (response, products);
        }




        public async Task<(HttpResponseMessage, T)> GetProductById<T>(string endpoint)
        {
            var response = await restClient.GetAsync(apiHandler.BuildUrl(endpoint));
            string jsonResponse = await response.Content.ReadAsStringAsync();
            try
            {
                var product = JsonConvert.DeserializeObject<T>(jsonResponse);
                return (response, product);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error while getting product data by Id" + ex);
                return default;
            }
        }



        public async Task<bool> AddProduct(AddProduct newProduct, string endpoint)
        {
            var requestUrl = apiHandler.BuildUrl(endpoint);

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

        public async Task<bool> UpdateProduct(UpdateProduct updatedProduct, string endpoint)
        {

            var requestUrl = apiHandler.BuildUrl(endpoint);

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


        public async Task<string> DeleteProduct(string endpoint)
        {
            var requestUrl = apiHandler.BuildUrl(endpoint);

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
                    return $"Failed to delete product. Status code: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                Logger.LogError("Error while deleting prodcut" + ex);
                return $"{ex.Message}";
            }
        }
    }



}
