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
       
        public async Task<List<GetAllProductsResponse>> GetAllProducts(string endpoint)
        {
            var response = await restClient.GetAsync(apiHandler.BuildUrl(endpoint));
            string jsonResponse = await response.Content.ReadAsStringAsync();
            try
            {
                return JsonConvert.DeserializeObject<List<GetAllProductsResponse>>(jsonResponse);
              
            }
            catch (Exception ex)
            {
                throw ex;
                Logger.LogError("Response is empty" + ex);
              
            }

            return null;
        }

        public async Task<GetProductsResponseById> GetProductById(string endpoint)
              {
                  var response = await restClient.GetAsync(apiHandler.BuildUrl(endpoint));
                  string jsonResponse = await response.Content.ReadAsStringAsync();
                  try
                  {
                    return JsonConvert.DeserializeObject<GetProductsResponseById>(jsonResponse);
            }
                  catch (Exception ex)
                  {
                      Logger.LogError("Error while getting product data by Id" + ex);
                      return null;
                  }
              }
        

        public async Task<string> DeleteProduct(string endpoint)
        {
            var requestUrl = apiHandler.BuildUrl(endpoint);

            try
            {
                var response = await restClient.DeleteAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
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

        public async Task<UpdateProduct> UpdateProduct(UpdateProduct updatedProduct, string endpoint)
        {
            var requestUrl = apiHandler.BuildUrl(endpoint);

            try
            {
                var jsonProduct = JsonConvert.SerializeObject(updatedProduct);
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

              
                var response = await restClient.PutAsync(requestUrl, content);

              
                if (response.IsSuccessStatusCode)
                {
                   
                    var responseData = await response.Content.ReadAsStringAsync();
                    var updatedProductResult = JsonConvert.DeserializeObject<UpdateProduct>(responseData);

                    return updatedProductResult; 
                }

               
                Logger.LogError($"Failed to update product. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                return null;
            }
            catch (HttpRequestException ex)
            {
               
                Logger.LogError("Error while updating product: " + ex);
                return null; 
            }
        }

        public async Task<AddProduct> AddProduct(AddProduct newProduct, string endpoint)
        {
            var requestUrl = apiHandler.BuildUrl(endpoint);

            try
            {
                
                var jsonProduct = JsonConvert.SerializeObject(newProduct);

               
                var content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

               
                var response = await restClient.PostAsync(requestUrl, content);

              
                if (response.IsSuccessStatusCode)
                {
                    
                    var responseData = await response.Content.ReadAsStringAsync();

                    Logger.LogError("API Response: " + responseData);

                    var result = JsonConvert.DeserializeObject<AddProduct>(responseData);

                    if (result != null)
                    {
                        return result;
                    }
                    else
                    {
                        Logger.LogError("Deserialization failed, result is null");
                    }
                }
                else
                {
                    Logger.LogError("API call failed with status code: " + response.StatusCode);
                }

                return null;
            }
            catch (HttpRequestException ex)
            {

                   Logger.LogError("Error while adding product: " + ex);
                return null;
            }
        }



    }




}


