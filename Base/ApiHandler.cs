using AssignmentApiTestMaster.Config;

namespace AssignmentApiTestMaster.Base
{
    public class ApiHandler
    {
     
        private string baseUrl = PropertyFileReader.properties["baseApiurl"];


        // Common method to build the URL
        public string BuildUrl(string endpoint)
        {
            // Construct the base URL
            UriBuilder builder = new UriBuilder($"{baseUrl}{endpoint}");
            return builder.Uri.ToString();
        }


    }
}
