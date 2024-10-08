using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApiTestMaster.Models.Response
{
    public class DeleteResponse
    {
        [JsonProperty("message")]
        public required string Message { get; set; }
    }
}
