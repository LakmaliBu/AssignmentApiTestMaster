using AssignmentApiTestMaster.Config;
using AssignmentApiTestMaster.Utilities;

namespace AssignmentApiTestMaster.Base
{
    public class ApiHandler
{

    private PropertyFileReader reader;
    private string baseUrl;

      
    public ApiHandler()
    {

            reader = new PropertyFileReader(@"config.properties");  
            baseUrl = reader.GetProperty("ApiBaseUrl");
            baseUrl = baseUrl.Replace("\"", "").Trim();
            Logger.LogInfo("Base Url :" + baseUrl);
        }


        // Common method to build the URL
        public string BuildUrl(string endpoint)
        {
            // Construct the base URL
            return new Uri(new Uri(baseUrl), endpoint).ToString();;
        }


    }
}
