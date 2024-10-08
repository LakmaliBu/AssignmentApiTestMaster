using AssignmentApiTestMaster.Utilities;
using Authlete.Util;


namespace AssignmentApiTestMaster.Config
{
    public class PropertyFileReader
    {
        public static IDictionary<string, string> properties;

        public void Read()
        {
            string file = "config.properties";
            try
            {
                using (TextReader reader = new StreamReader(file))
                {
                    properties = PropertiesLoader.Load(reader);

                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error while reading property file" + ex);


            }
        }
    }
}
