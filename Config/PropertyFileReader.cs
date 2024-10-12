using AssignmentApiTestMaster.Utilities;


namespace AssignmentApiTestMaster.Config
{
    public class PropertyFileReader
    {
     
        private Dictionary<string, string> _properties;

        public PropertyFileReader(string filePath)
        {
            _properties = new Dictionary<string, string>();
            foreach (var row in File.ReadAllLines(filePath))
            {
                if (!string.IsNullOrWhiteSpace(row) && row.Contains('='))
                {
                    var keyValue = row.Split('=');
                    _properties.Add(keyValue[0].Trim(), keyValue[1].Trim());
                }
            }
        }

        public string GetProperty(string key)
        {
            return _properties.ContainsKey(key) ? _properties[key] : null;
        }


        public void WriteToConfigFile(string key, string value, string pathToWrite)
        {

            try
            {

                string[] lines = File.ReadAllLines(pathToWrite);
                bool keyExists = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith(key + "=", StringComparison.OrdinalIgnoreCase))
                    {
                      
                        lines[i] = $"{key}={value}";
                        keyExists = true;
                        break;
                    }
                }

                if (!keyExists)
                {
                    Array.Resize(ref lines, lines.Length + 1);
                    lines[^1] = $"{key}={value}"; 
                }

                File.WriteAllLines(pathToWrite, lines);
            }
            catch (Exception ex)
            {
                Logger.LogError($"An error occurred while writing to the config file: {ex.Message}");
            }
        }
    }
}
