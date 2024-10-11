using Newtonsoft.Json;


namespace AssignmentApiTestMaster.Models.Response
{
    public class DeleteResponse
    {
        [JsonProperty("message")]
        public required string Message { get; set; }
    }
}
